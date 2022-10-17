using System;
using UnityEngine.Events;
using UnityEngine.UI;

namespace HM.Extensions
{
    public static class ButtonExtensions
    {
        /// <summary>
        /// 关注按钮点击事件，只会触发一次
        /// </summary>
        /// <param name="btn"></param>
        /// <param name="onClick"></param>
        public static UnityAction AddOneTimeClickListener(this Button btn, Action onClick)
        {
            UnityAction _onClick = null;
            _onClick = () =>
            {
                btn.GetComponent<Button>().onClick.RemoveListener(_onClick);
                onClick?.Invoke();
            };
            btn.GetComponent<Button>().onClick.AddListener(_onClick);
            return _onClick;
        }
    }
}