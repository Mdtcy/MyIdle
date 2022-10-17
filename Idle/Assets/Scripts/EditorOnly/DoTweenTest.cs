/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2021-11-26 10:38:46
 * @modify date 2021-11-26 10:38:46
 * @desc [方便在编辑器里测试DoTween效果]
 */

using DG.Tweening;
using HM.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

#pragma warning disable 0649
namespace NewLife.EditorOnly
{
    public class DoTweenTest : MonoBehaviour
    {
        [BoxGroup("通用设置")]
        [SerializeField]
        private Ease ease = Ease.Linear;

        [BoxGroup("通用设置")]
        [SerializeField]
        private float duration = 1;

        [BoxGroup("通用设置")]
        [SerializeField]
        private bool isRelative = true;

        #region DOFade

        [FoldoutGroup("DoFade")]
        [SerializeField]
        private Graphic imgDoFade;

        [FoldoutGroup("DoFade")]
        [SerializeField]
        private float fromAlpha;

        [FoldoutGroup("DoFade")]
        [SerializeField]
        private float toAlpha;

        [FoldoutGroup("DoFade")]
        [Button]
        private void DoFade()
        {
            imgDoFade.SetAlpha(fromAlpha);
            imgDoFade.DOFade(toAlpha, duration).SetEase(ease);
        }

        #endregion

        #region DoShake

        [FoldoutGroup("DoShake")]
        [SerializeField]
        private Transform transDoShake;

        [FoldoutGroup("DoShake")]
        [SerializeField]
        private Vector2 strength = new Vector2(10, 10);

        [FoldoutGroup("DoShake")]
        [SerializeField]
        private int vibrato = 10;

        [FoldoutGroup("DoShake")]
        [SerializeField]
        private float randomness = 90;

        [FoldoutGroup("DoShake")]
        [Button]
        private void DoShakePosition()
        {
            transDoShake.transform.DOShakePosition(duration, strength, vibrato, randomness)
                        .SetEase(ease)
                        .SetRelative(isRelative);
        }

        #endregion

    }
}
#pragma warning restore 0649