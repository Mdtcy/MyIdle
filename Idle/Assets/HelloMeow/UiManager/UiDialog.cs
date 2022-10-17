using System;
using UnityEngine;

namespace HM.GameBase
{
    public partial class UiDialog : MonoBehaviour
    {
        #region Will show
        public delegate void OnWillShowDelegate(object param);
        public event OnWillShowDelegate OnWillShowEvent;
        public virtual void OnWillShow(object param)
        {
            OnWillShowEvent?.Invoke(param);
        }
        #endregion

        #region Did show
        public delegate void OnDidShowDelegate(object param);
        public event OnDidShowDelegate OnDidShowEvent;
        public virtual void OnDidShow(object param)
        {
            OnDidShowEvent?.Invoke(param);
        }
        #endregion

        #region Will hide
        public delegate void OnWillHideDelegate();
        public event OnWillHideDelegate OnWillHideEvent;
        public virtual void OnWillHide()
        {
            OnWillHideEvent?.Invoke();
        }
        #endregion

        #region Did hide
        public delegate void OnDidHideDelegate();
        public event OnDidHideDelegate OnDidHideEvent;
        public virtual void OnDidHide()
        {
            OnDidHideEvent?.Invoke();
        }
        #endregion

        /// <summary>
        /// UiDialog在关闭自己的时候调用这个接口
        /// </summary>
        /// <param name="returnValue"></param>
        public virtual void Hide(object returnValue)
        {
            uiManager.HideModal(this, returnValue);
        }

        /// <summary>
        /// UiDialog在关闭自己的时候调用这个接口
        /// </summary>
        /// <param name="returnValue"></param>
        public void Hide()
        {
            Hide(null);
        }

        public void HideDirectly()
        {
            uiManager.HideModalDirectly(this, null);
        }

        /// <summary>
        /// 如果需要自定义显示，重写该方法并返回true
        /// </summary>
        /// <returns></returns>
        public virtual bool NeedCustomShow()
        {
            return false;
        }

        /// <summary>
        /// 自定义显示对话框，显示完之后要调用OnDidShow(param)；
        /// 只会在NeedCustomShow()返回true时被调用
        /// </summary>
        /// <param name="param"></param>
        public virtual void ShowCustom(object param)
        {
            // show dialogue

            // after showed, call OnDidShow
            OnDidShow(param);
        }
        /// <summary>
        /// 如果需要自定义销毁，重写该方法并返回true
        /// </summary>
        /// <returns></returns>
        public virtual bool NeedCustomHide()
        {
            return false;
        }

        /// <summary>
        /// 自定义销毁对话框，在NeedCustomHide()返回true时被调用；
        /// 销毁后要调用onDidHide
        /// </summary>
        /// <param name="onDidHide"></param>
        public virtual void HideCustom(Action onDidHide)
        {
            // hide&remove dialogue

            onDidHide();
        }

        [HideInInspector]
        public UiManager uiManager;
    }
}