/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2019-10-11 17:10:42
 * @modify date 2019-10-11 17:10:42
 * @desc [description]
 */

#pragma warning disable 0649

using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace HM.UI.Toggle
{
    public class ToggleBase : MonoBehaviour, IPointerClickHandler, ISubmitHandler
    {
	    [Serializable]
        // UnityEvent callback for when a toggle is toggled.
        public class ToggleEvent : UnityEvent<bool> {}
        #region FIELDS

		public ToggleEvent onValueChanged = new ToggleEvent();

        [OnValueChanged("AddToGroup")]
		[SerializeField]
		private ToggleBaseGroup m_group;

		[SerializeField]
		private bool m_isOn;

        [SerializeField]
        private bool m_interactable = true;

        [SerializeField]
        private bool m_triggerOnEnable = false;

        private bool m_hasInitializedOnStart = false;

        [SerializeField]
        private List<ToggleBase> m_toggles;

        // ---------- private fields ----------

		#endregion

		#region PROPERTIES

		public bool isOn
        {
            get => m_isOn;
            set => Set(value);
        }

        public bool Interactable
        {
            get => m_interactable;
            set => m_interactable = value;
        }

        /// <summary>
        /// Group the toggle belongs to.
        /// </summary>
        public ToggleBaseGroup group
        {
            get => m_group;
            set
            {
                SetToggleGroup(value, true);
            }
        }

		#endregion

		#region PUBLIC METHODS
		/// <summary>
        /// Returns true if the GameObject and the Component are active.
        /// </summary>
        public virtual bool IsActive()
        {
            return isActiveAndEnabled;
        }

		/// <summary>
        /// Set isOn without invoking onValueChanged callback.
        /// </summary>
        /// <param name="value">New Value for isOn.</param>
        public void SetIsOnWithoutNotify(bool value)
        {
            Set(value, false);
        }

        /// <summary>
        /// force to set value even if m_isOn equals to value.
        /// </summary>
        /// <param name="value"></param>
        public void ForceSetIsOn(bool value)
        {
            m_isOn = !value;
            isOn = value;
        }

        /// <summary>
        /// React to clicks.
        /// </summary>
        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (!Interactable)
                return;
            if (eventData.button != PointerEventData.InputButton.Left)
                return;

            InternalToggle();
        }

        public virtual void OnSubmit(BaseEventData eventData)
        {
            InternalToggle();
        }

        public void AddToggle(ToggleBase toggle)
        {
            RemoveToggle(toggle);
            m_toggles.Add(toggle);
        }

        public void RemoveToggle(ToggleBase toggle)
        {
            m_toggles.Remove(toggle);
        }

		#endregion

		#region PROTECTED METHODS

        protected virtual void OnToggleChanged()
        {
            if (m_toggles != null)
            {
                foreach (var toggle in m_toggles)
                {
                    toggle.isOn = isOn;
                    toggle.OnToggleChanged();
                }
            }
        }
		#endregion

		#region PRIVATE METHODS

        private void OnDestroy()
        {
            if (m_group != null)
                m_group.EnsureValidState();
        }

        private void OnEnable()
        {
            SetToggleGroup(m_group, false);

            if (m_triggerOnEnable && m_hasInitializedOnStart)
            {
                onValueChanged?.Invoke(isOn);
            }
        }

        private void OnDisable()
        {
            SetToggleGroup(null, false);
        }
		private void SetToggleGroup(ToggleBaseGroup newGroup, bool setMemberValue)
        {
            // Sometimes IsActive returns false in OnDisable so don't check for it.
            // Rather remove the toggle too often than too little.
            if (m_group != null)
                m_group.UnregisterToggle(this);

            // At runtime the group variable should be set but not when calling this method from OnEnable or OnDisable.
            // That's why we use the setMemberValue parameter.
            if (setMemberValue)
                m_group = newGroup;

            // Only register to the new group if this Toggle is active.
            if (newGroup != null && IsActive())
                newGroup.RegisterToggle(this);

            // If we are in a new group, and this toggle is on, notify group.
            // Note: Don't refer to m_group here as it's not guaranteed to have been set.
            if (newGroup != null && isOn && IsActive())
                newGroup.NotifyToggleOn(this);
        }

		private void InternalToggle()
        {
            if (!IsActive())
                return;

            // 20200810 disallow toggle off while is on.
            if (isOn && (m_group != null && !m_group.allowSwitchOff))
                return;

            isOn = !isOn;
        }

		private void Set(bool value, bool sendCallback = true)
        {
            if (m_isOn == value)
                return;

            // if we are in a group and set to true, do group logic
            m_isOn = value;
            if (group != null && IsActive())
            {
                if (m_isOn || (!group.AnyTogglesOn() && !group.allowSwitchOff))
                {
                    m_isOn = true;
                    group.NotifyToggleOn(this, sendCallback);
                }
            }

            OnToggleChanged();

            // Always send event when toggle is clicked, even if value didn't change
            // due to already active toggle in a toggle group being clicked.
            // Controls like Dropdown rely on this.
            // It's up to the user to ignore a selection being set to the same value it already was, if desired.
            if (sendCallback)
            {
                UISystemProfilerApi.AddMarker("Toggle.value", this);
                onValueChanged.Invoke(m_isOn);
            }
        }

        private void Start()
        {
            OnToggleChanged();
            m_hasInitializedOnStart = true;
        }

        // 将自己添加到group中
        private void AddToGroup()
        {
            if (group != null)
            {
                group.RegisterToggle(this);
            }
        }

        #endregion

		#region STATIC METHODS

		#endregion



    #if UNITY_EDITOR
        [Button("预览效果（自动切换）", ButtonSizes.Medium)]
        private void Preview()
        {
            var grp = group;
            group = null;
            {
                isOn = !isOn;
            }
            group = grp;
        }
    #endif
    }
}
#pragma warning restore 0649
