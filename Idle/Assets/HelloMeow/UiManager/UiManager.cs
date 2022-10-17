/**
 * @author [jie.wen]
 * @email [example@mail.com]
 * @create date 2018-12-19 11:40:56
 * @modify date 2018-12-19 11:40:56
 * @desc [场景内UI弹框的管理器]
 * @modify
 * jie.wen 20181220 明确定义ShowModalOption的各枚举值从而FlowCanvas可以正常工作
 */

using System;
using System.Collections.Generic;
using DG.Tweening;
using HM.Extensions;
using MEC;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

namespace HM.GameBase
{
    /// <summary>
    /// 负责场景内UI弹框的管理
    /// </summary>
    public partial class UiManager : MonoBehaviour
    {
        #region FIELDS

        // 场景中的canvas
        [SerializeField]
        private RectTransform canvasRoot;

        // 如果dialog需要复用，关闭后会挂载到该节点之下
        [SerializeField]
        private RectTransform inactiveDialogsRoot;

        // 所有显示的弹框都暂存在_dialogRoot下
        [SerializeField]
        private RectTransform dialogRoot;

        [BoxGroup("Hide Animation Control")]
        [SerializeField]
        private Ease easeHide = Ease.OutQuad;

        [BoxGroup("Hide Animation Control")]
        [SerializeField]
        private float dtHide = 0.3f;

        // 当弹框创建后会调用该事件回调
        public delegate void OnDialogueAddedDelegate(UiDialogHandle handle);

        // 当弹窗隐藏后会调用该事件回调
        public delegate void OnDialogueHiddenDelegate(UiDialogHandle handle);

        public event OnDialogueAddedDelegate  OnDialogueAdded;
        public event OnDialogueHiddenDelegate OnDialogueHidden;

        [Serializable]
        public class ModalEntity : IHMPooledObject
        {
            public UiDialogHandle  handle;
            public bool            isTemporary;
            public ShowModalOption option;

            public void OnEnterPool()
            {
                if (handle != null)
                {
                    ObjectPool<UiDialogHandle>.Release(ref handle);
                    handle = null;
                }

                option = ShowModalOption.Invisible;
            }
        }

        // 保存所有正在显示的弹框
        [SerializeField]
        private List<ModalEntity> modals = new List<ModalEntity>();

        // 黑色半透背景对象(在fade-out过程中有效)
        private RectTransform fadingOutBlackCurtain;

        // 黑色半透背景，只会有一个
        [SerializeField]
        private RectTransform curtain;

        // 半透背景对象
        private UiManagerCurtain uiCurtain;

        [SerializeField, ReadOnly]
        private Color curtainColor = new Color(0, 0, 0, 0.7f);

        // 弹框的选项
        [Flags]
        public enum ShowModalOption
        {
            Invisible        = 0x01,   // 不显示黑色半透
            BlackCurtain     = 0x100,  // 显示黑色半透
            HideEnabled      = 0x10,   // 点击半透区域时关闭UI
            TransparentBlock = 0x1000, // block空白区域的点击
        }

        #endregion

        #region PROPERTIES

        public UiDialogHandle TopHandle
        {
            get
            {
                var entity = TopModalEntity();

                return entity?.handle;
            }
        }

        /// <summary>
        /// 当前最前面的dialog
        /// </summary>
        /// <returns></returns>
        public UiDialog TopDialog()
        {
            var entity = TopModalEntity();
            return entity?.handle.Dialog;
        }

        #endregion

        #region PUBLIC METHODS

        public RectTransform FindFromDialogRoot(string name)
        {
            var targetTransform = dialogRoot.Find(name);

            return targetTransform != null ? targetTransform.GetComponent<RectTransform>() : null;
        }

        /// <summary>
        /// 在正在打开的弹框里查找指定类型（父类也可以）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public UiDialogHandle FindHandle<T>()
        {
            foreach (var entity in modals)
            {
                if (typeof(T) == entity.handle.Dialog.GetType() ||
                    entity.handle.Dialog.GetType().IsSubclassOf(typeof(T)))
                {
                    return entity.handle;
                }
            }

            return null;
        }

        /// <summary>
        /// 在正在打开的弹框里查找所有指定类型（父类也可以）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public void FindHandles<T>(ref List<UiDialogHandle> handles)
        {
            foreach (var entity in modals)
            {
                if (typeof(T) == entity.handle.Dialog.GetType() ||
                    entity.handle.Dialog.GetType().IsSubclassOf(typeof(T)))
                {
                    handles.Add(entity.handle);
                }
            }
        }

        /// <summary>
        /// 关闭当前最上方对话框
        /// </summary>
        public void HideCurrentModalDirectly()
        {
            var currentDialogue = CurrentDialog();
            if (currentDialogue != null)
                HideModalDirectly(currentDialogue, null);
        }

        /// <summary>
        /// 获取当前最上方对话框handle
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public UiDialogHandle GetModalHandle<T>() where T : UiDialog
        {
            var dialogGo = FindFromDialogRoot(typeof(T).Name);

            if (dialogGo == null)
                throw new Exception("ModalRoot not found: " + typeof(T).Name);

            var dialog = dialogGo.GetComponent<T>();

            if (dialog == null)
                throw new Exception("ModalRoot type mismatched: " + typeof(T).Name);

            foreach (var modalEntity in modals)
            {
                if (modalEntity.handle.Dialog == dialog)
                {
                    return modalEntity.handle;
                }
            }

            return null;
        }

        /// <summary>
        /// 返回当前正在显示的对话框数量
        /// </summary>
        /// <returns></returns>
        public int GetModalCount()
        {
            return modals.Count;
        }

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        // 根据名字查找可复用对话框对象
        private GameObject FindInactiveDialog(string name)
        {
            for (int i = 0; i < inactiveDialogsRoot.childCount; i++)
            {
                var child = inactiveDialogsRoot.GetChild(i);

                if (child.name.Equals(name, StringComparison.Ordinal))
                {
                    return child.gameObject;
                }
            }

            return null;
        }

        private void Awake()
        {
            curtain = GetOrCreateCurtain();
            curtain.gameObject.SetActive(false);
            uiCurtain = curtain.GetComponent<UiManagerCurtain>();
            uiCurtain.Setup(curtainColor, OnClickCurtain);
        }

        private ModalEntity TopModalEntity()
        {
            return GetModalCount() > 0 ? modals[modals.Count - 1] : null;
        }

        // 创建半透遮罩
        private RectTransform CreateCurtain(Color color, RectTransform parent)
        {
            if (Application.isPlaying)
            {
                HMLog.LogError($"Should not create curtain during runtime, please setup in editor");
            }

            var go = new GameObject("Curtain");
            var rt = go.AddComponent<RectTransform>();
            go.AddComponent<UiManagerCurtain>();

            // image
            var image = go.GetOrAddComponent<Image>();
            image.color = color;

            // button
            go.GetComponent<Button>().transition = Selectable.Transition.None;

            // rect transform
            rt.SetParent(parent, false);
            rt.anchorMin = Vector2.zero;
            rt.anchorMax = Vector2.one;
            rt.offsetMin = Vector2.zero;
            rt.offsetMax = Vector2.zero;

            return rt;
        }

        private UiDialog GetOrCreateDialogue(GameObject prefab, bool recycleEnabled)
        {
            if (recycleEnabled)
            {
                // 先从active里寻找
                var dialog = FindFromDialogRoot(prefab.name);

                if (dialog != null)
                {
                    dialog.SetAsLastSibling();

                    return dialog.GetComponent<UiDialog>();
                }

                // 再从inactive里寻找
                var ret = FindInactiveDialog(prefab.name);

                if (ret != null)
                {
                    ret.SetActive(true);
                    ret.transform.SetParent(dialogRoot, false);

                    return ret.GetComponent<UiDialog>();
                }
            }

            // 都没有，创建新的
            var uiDialog = Factory.Create(prefab);
            uiDialog.transform.SetParent(dialogRoot, false);
            uiDialog.name = prefab.name;

            return uiDialog;
        }

        private RectTransform GetOrCreateCurtain()
        {
            // When there is a curtain fading out, reuse it to reduce flickering
            if (fadingOutBlackCurtain != null)
            {
                var curtain = fadingOutBlackCurtain; // the same as _curtain
                DOTween.Kill(curtain.GetComponentInChildren<Image>());
                fadingOutBlackCurtain = null;
                HMLog.LogDebug("[GetOrCreateCurtain] return fading out curtain");

                return curtain;
            }

            // 保证curtain不为null
            if (curtain == null)
            {
                curtain = CreateCurtain(new Color(0, 0, 0, 0), dialogRoot);
                HMLog.LogDebug("[GetOrCreateCurtain] create new curtain");
            }

            return curtain;
        }

        private void OnClickCurtain()
        {
            var entity = TopModalEntity();

            if (entity == null)
            {
                HMLog.LogDebug("No top handle is found, ignore");

                return;
            }

            if ((entity.option & ShowModalOption.HideEnabled) == 0)
            {
                HMLog.LogDebug($"entity.HideEnabled = false, ignore | {entity.handle.Dialog.name} option = {entity.option}");

                return;
            }

            entity.handle.Dialog.Hide();
        }

        private void ShowCurtain(ShowModalOption option)
        {
            if (!curtain.gameObject.activeSelf)
                curtain.gameObject.SetActive(true);

            curtain.SetAsLastSibling();

            if (OptionEnabled(option, ShowModalOption.BlackCurtain))
            {
                // fade-in动画
                uiCurtain.FadeIn();
            }
            else
            {
                uiCurtain.SetVisible(false);
            }

            if (OptionEnabled(option, ShowModalOption.HideEnabled))
            {
                uiCurtain.SetBlock(true);
                uiCurtain.SetClickable(true);
            }
            else if (OptionEnabled(option, ShowModalOption.TransparentBlock))
            {
                uiCurtain.SetBlock(true);
                uiCurtain.SetClickable(false);
            }
            else
            {
                uiCurtain.SetBlock(false);
                uiCurtain.SetClickable(false);
            }
        }

        /// <summary>
        /// 当前正在显示的最顶层对话框
        /// </summary>
        /// <returns></returns>
        private UiDialog CurrentDialog()
        {
            if (GetModalCount() > 0)
            {
                return modals[modals.Count - 1].handle.Dialog;
            }

            return null;
        }

        /// <summary>
        /// 显示流程：
        /// dialog.OnWillShow(param);
        /// 如需要，则显示黑色半透背景
        /// if (dialog.NeedCustomShow()) {
        ///     dialog.ShowCustom(param); // 自定义显示需自行调用dialog.OnDidShow()
        /// }
        /// else {
        ///     默认方式显示后，调用dialog.OnDidShow()
        /// }
        /// </summary>
        /// <param name="dialog"></param>
        /// <param name="isTemporary"></param>
        /// <param name="param"></param>
        /// <param name="option"></param>
        /// <returns>返回UiDialogHandle对象</returns>
        private UiDialogHandle ShowModalInternal(UiDialog        dialog,
                                                 bool            isTemporary,
                                                 object          param,
                                                 ShowModalOption option)
        {
            HMLog.LogInfo("[UiManager]打开对话框(prefab={0}, option={1}, isTemporary={2})", dialog.name, option,
                          isTemporary);

            // 设置层级
            curtain.SetSiblingIndex(dialog.GetComponent<RectTransform>().GetSiblingIndex());

            // unnecessary but it works as workaround for setting sibling index
            dialog.GetComponent<RectTransform>().SetSiblingIndex(curtain.GetSiblingIndex() + 1);

            dialog.uiManager = this;

            // create handle and register to modals
            var handle = ObjectPool<UiDialogHandle>.Claim();
            handle.Dialog  = dialog;
            handle.Visible = true;

            var modal = ObjectPool<ModalEntity>.Claim();
            modal.handle      = handle;
            modal.isTemporary = isTemporary;
            modal.option      = option;

            // 如果要打开的UI已经打开着了，则将已经打开着的UI移除出modals，避免计数错误（因为在这里打开同样的UI实际上只是将已经存在的该UI移到最上层）
            // 这里的modals内是有顺序的，所以先移除再添加到最后
            for (int i = 0; i < modals.Count; i++)
            {
                var entity = modals[i];

                if (entity.handle.Dialog == modal.handle.Dialog)
                {
                    modals.Remove(entity);

                    break;
                }
            }

            modals.Add(modal);

            OnDialogueAdded?.Invoke(handle);

            dialog.OnWillShow(param);

            if (dialog.NeedCustomShow())
            {
                dialog.ShowCustom(param);
            }
            else
            {
                var dialogCg = dialog.GetOrAddComponent<CanvasGroup>();
                dialogCg.blocksRaycasts = true;
                dialogCg.alpha          = 0.2f;
                dialogCg.DOFade(1f, 0.2f)
                        .SetEase(Ease.OutCubic)
                        .SetUpdate(true)
                        .OnComplete(() => { dialog.OnDidShow(param); });
            }

            return handle;
        }

        private void UpdateCurtainOnDialogueClosed(bool closeDirectly)
        {
            HMLog.LogVerbose($"[UiManager]::HideModal _modals.Count = {modals.Count}, _curtain = {curtain == null}");

            if (modals.Count > 0)
            {
                // 挪到下一个active modal后面
                var last       = modals.LastOne();
                var nextDialog = last.handle.Dialog;
                curtain.SetSiblingIndex(nextDialog.rectTransform().GetSiblingIndex());

                // unnecessary but it works as workaround for setting sibling index
                nextDialog.rectTransform().SetSiblingIndex(curtain.GetSiblingIndex() + 1);

                var option = last.option;

                if (OptionEnabled(option, ShowModalOption.BlackCurtain))
                {
                    // fade-in动画
                    uiCurtain.FadeIn();
                }
                else
                {
                    uiCurtain.SetVisible(false);
                }

                if (OptionEnabled(option, ShowModalOption.HideEnabled))
                {
                    uiCurtain.SetBlock(true);
                    uiCurtain.SetClickable(true);
                }
                else if (OptionEnabled(option, ShowModalOption.TransparentBlock))
                {
                    uiCurtain.SetBlock(true);
                    uiCurtain.SetClickable(false);
                }
                else
                {
                    uiCurtain.SetBlock(false);
                    uiCurtain.SetClickable(false);
                }
            }
            else
            {
                if (closeDirectly)
                {
                    CleanupCurtain();
                }
                else
                {
                    fadingOutBlackCurtain = curtain;
                    uiCurtain.FadeOut(CleanupCurtain);
                }
            }
        }

        private bool OptionEnabled(ShowModalOption option1, ShowModalOption option2)
        {
            return (option1 & option2) != 0;
        }

        private void CleanupCurtain()
        {
            curtain.gameObject.SetActive(false);
            fadingOutBlackCurtain = null;
        }

        private ModalEntity FindModal(UiDialog dialog)
        {
            foreach (var modalEntity in modals)
            {
                if (modalEntity.handle.Dialog == dialog)
                {
                    return modalEntity;
                }
            }

            return null;
        }

        /// <summary>
        /// UiDialog在销毁自己的时候调用这个接口；
        /// uiDialog.OnWillHide();
        /// if (uiDialog.NeedCustomHide())
        /// {
        ///    uiDialog.HideCustom(_onDidHide);
        /// }
        /// else uiDialog.OnDidHide();
        /// </summary>
        /// <param name="dialog"></param>
        /// <param name="returnValue">如果注册了handle.Hidden事件，会将returnValue回传给这个回调</param>
        /// <returns></returns>
        internal bool HideModal(UiDialog dialog, object returnValue)
        {
            HMLog.LogInfo("[UiManager]关闭对话框(name={0})", dialog.name);
            var entity = FindModal(dialog);

            if (entity == null)
            {
                HMLog.LogWarning($"[UiManager]关闭对话框(name={dialog.name})失败，未找到该对话框");

                return false;
            }

            entity.handle.ReturnValue = returnValue;

            // trigger on will hide event
            dialog.OnWillHide();

            OnDialogueHidden?.Invoke(entity.handle);

            if (dialog.NeedCustomHide())
            {
                dialog.HideCustom(onDidHide: () => OnDialogDidHide(dialog));
            }
            else
            {
                _HideDialogAnimated(dialog).RunCoroutine();
            }

            return true;
        }

        private IEnumerator<float> _HideDialogAnimated(UiDialog dialog)
        {
            var dialogCg = dialog.GetOrAddComponent<CanvasGroup>();

            //无法连续点击按钮
            dialogCg.blocksRaycasts = false;

            // remove dialog
            var anim = dialogCg.DOFade(0f, dtHide)
                               .SetEase(easeHide)
                               .SetUpdate(true);

            yield return Timing.WaitUntilDone(anim.WaitForCompletion(true));
            OnDialogDidHide(dialog);
        }

        /// <summary>
        /// 直接关闭对话框，不会有特效，不会调用NeedCustomHide()
        /// </summary>
        /// <param name="dialog"></param>
        /// <param name="returnValue"></param>
        /// <returns></returns>
        public bool HideModalDirectly(UiDialog dialog, object returnValue)
        {
            if (dialog == null)
            {
                return false;
            }

            HMLog.LogInfo("[UiManager]直接关闭对话框(name={0})", dialog.name);
            var entity = FindModal(dialog);

            if (entity == null)
            {
                HMLog.LogWarning($"[UiManager]直接关闭对话框(name={dialog.name})失败，未找到该对话框");

                return false;
            }

            entity.handle.ReturnValue = returnValue;

            // trigger UiDialog.OnHide
            dialog.OnWillHide();

            OnDialogueHidden?.Invoke(entity.handle);

            // remove curtain
            OnDialogDidHide(dialog);

            return true;
        }

        private void OnDialogDidHide(UiDialog dialog)
        {
            var entity = FindModal(dialog);

            if (entity == null)
            {
                HMLog.LogVerbose($"[UiManager]Failed to find entity of dialog = {dialog}, maybe multiple hide requests received.");

                return;
            }

            // trigger Hidden event
            var handle = entity.handle;
            handle.Visible = false;
            dialog.OnDidHide();

            if (entity.isTemporary)
            {
                var asyncHandle = handle.AsyncHandle;

                if (asyncHandle.IsValid() && asyncHandle.IsDone)
                {
                    Addressables.Release(asyncHandle);
                }

                Destroy(dialog.gameObject);
            }
            else
            {
                // move to inactive dialogues.
                dialog.transform.SetParent(inactiveDialogsRoot, false);
                dialog.gameObject.SetActive(false);
            }

            handle.Hidden?.Invoke(dialog, handle.ReturnValue);

            modals.Remove(entity);
            ObjectPool<ModalEntity>.Release(ref entity);

            // remove curtain
            UpdateCurtainOnDialogueClosed(false);
        }

        #if UNITY_EDITOR
        [Sirenix.OdinInspector.Button("自动设置", ButtonSizes.Medium)]
        private void AutoSetup()
        {
            var rt = transform.rectTransform();
            rt.RemoveAllChildren();
            rt.anchorMin = Vector2.zero;
            rt.anchorMax = Vector2.one;
            rt.offsetMin = Vector2.zero;
            rt.offsetMax = Vector2.zero;

            canvasRoot = rt;
            dialogRoot = canvasRoot;

            inactiveDialogsRoot = CreateFullScreenRectTransform();
            inactiveDialogsRoot.SetParent(rt, false);

            curtain = CreateCurtain(new Color(0, 0, 0, 0), dialogRoot);
            curtain.gameObject.SetActive(false);
        }

        private RectTransform CreateFullScreenRectTransform()
        {
            var go = new GameObject("InactiveDialogs");
            var rt = go.AddComponent<RectTransform>();
            rt.anchorMin = Vector2.zero;
            rt.anchorMax = Vector2.one;
            rt.offsetMin = Vector2.zero;
            rt.offsetMax = Vector2.zero;

            return rt;
        }
        #endif

        #endregion

        #region STATIC METHODS

        #endregion
    }
}