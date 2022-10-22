using System;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NewLife.UI.Extensions
{
    public static class ToggleExtensions
    {
        /// <summary>
        /// 关注复选框点击事件，只会触发一次
        /// </summary>
        /// <param name="toggle"></param>
        /// <param name="onClick"></param>
        public static UnityAction<bool> AddOneTimeClickListener(this Toggle toggle, Action onClick)
        {
            UnityAction<bool> _onClick = null;
            _onClick = value =>
            {
                toggle.onValueChanged.RemoveListener(_onClick);
                onClick?.Invoke();
            };
            toggle.onValueChanged.AddListener(_onClick);
            return _onClick;
        }
    }
}