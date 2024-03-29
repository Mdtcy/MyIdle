﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;

namespace MoreMountains.Feedbacks
{
    /// <summary>
    /// This feedback will trigger a one time play on a target FloatController
    /// </summary>
    [AddComponentMenu("")]
    [FeedbackHelp("This feedback lets you trigger a one time play on a target ShaderController.")]
    [FeedbackPath("Renderer/ShaderController")]
    public class MMFeedbackShaderController : MMFeedback
    {
        /// the different possible modes 
        public enum Modes { OneTime, ToDestination }
        /// sets the inspector color for this feedback
        #if UNITY_EDITOR
        public override Color FeedbackColor { get { return MMFeedbacksInspectorColors.RendererColor; } }
        #endif

        [Header("Float Controller")]
        /// the mode this controller is in
        public Modes Mode = Modes.OneTime;
        /// the float controller to trigger a one time play on
        public ShaderController TargetShaderController;
        /// whether this should revert to original at the end
        public bool RevertToInitialValueAfterEnd = false;
        /// the duration of the One Time shake
        [MMFEnumCondition("Mode", (int)Modes.OneTime)]
        public float OneTimeDuration = 1f;
        /// the amplitude of the One Time shake (this will be multiplied by the curve's height)
        [MMFEnumCondition("Mode", (int)Modes.OneTime)]
        public float OneTimeAmplitude = 1f;
        /// the low value to remap the normalized curve value to 
        [MMFEnumCondition("Mode", (int)Modes.OneTime)]
        public float OneTimeRemapMin = 0f;
        /// the high value to remap the normalized curve value to 
        [MMFEnumCondition("Mode", (int)Modes.OneTime)]
        public float OneTimeRemapMax = 1f;
        /// the curve to apply to the one time shake
        [MMFEnumCondition("Mode", (int)Modes.OneTime)]
        public AnimationCurve OneTimeCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(0.5f, 1), new Keyframe(1, 0));

        [MMFEnumCondition("Mode", (int)Modes.ToDestination)]
        public float ToDestinationValue = 1f;
        [MMFEnumCondition("Mode", (int)Modes.ToDestination)]
        public float ToDestinationDuration = 1f;
        [MMFEnumCondition("Mode", (int)Modes.ToDestination)]
        public Color ToDestinationColor = Color.red;
        [MMFEnumCondition("Mode", (int)Modes.ToDestination)]
        public AnimationCurve ToDestinationCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(0.5f, 1), new Keyframe(1, 0));

        /// the duration of this feedback is the duration of the one time hit
        public override float FeedbackDuration { get { return OneTimeDuration; } }

        protected float _oneTimeDurationStorage;
        protected float _oneTimeAmplitudeStorage;
        protected float _oneTimeRemapMinStorage;
        protected float _oneTimeRemapMaxStorage;
        protected AnimationCurve _oneTimeCurveStorage;
        protected float _toDestinationValueStorage;
        protected float _toDestinationDurationStorage;
        protected AnimationCurve _toDestinationCurveStorage;
        protected bool _revertToInitialValueAfterEndStorage;

        /// <summary>
        /// On init we grab our initial color and components
        /// </summary>
        /// <param name="owner"></param>
        protected override void CustomInitialization(GameObject owner)
        {
            if (Active && (TargetShaderController != null))
            {
                _oneTimeDurationStorage = TargetShaderController.OneTimeDuration;
                _oneTimeAmplitudeStorage = TargetShaderController.OneTimeAmplitude;
                _oneTimeCurveStorage = TargetShaderController.OneTimeCurve;
                _oneTimeRemapMinStorage = TargetShaderController.OneTimeRemapMin;
                _oneTimeRemapMaxStorage = TargetShaderController.OneTimeRemapMax;
                _toDestinationCurveStorage = TargetShaderController.ToDestinationCurve;
                _toDestinationDurationStorage = TargetShaderController.ToDestinationDuration;
                _toDestinationValueStorage = TargetShaderController.ToDestinationValue;
                _revertToInitialValueAfterEndStorage = TargetShaderController.RevertToInitialValueAfterEnd;
            }
        }

        /// <summary>
        /// On play we make our renderer flicker
        /// </summary>
        /// <param name="position"></param>
        /// <param name="attenuation"></param>
        protected override void CustomPlayFeedback(Vector3 position, float attenuation = 1.0f)
        {
            if (Active && (TargetShaderController != null))
            {
                TargetShaderController.RevertToInitialValueAfterEnd = RevertToInitialValueAfterEnd;
                if (Mode == Modes.OneTime)
                {
                    TargetShaderController.OneTimeDuration = OneTimeDuration;
                    TargetShaderController.OneTimeAmplitude = OneTimeAmplitude;
                    TargetShaderController.OneTimeCurve = OneTimeCurve;
                    TargetShaderController.OneTimeRemapMin = OneTimeRemapMin;
                    TargetShaderController.OneTimeRemapMax = OneTimeRemapMax;
                    TargetShaderController.OneTime();
                }
                if (Mode == Modes.ToDestination)
                {
                    TargetShaderController.ToColor = ToDestinationColor;
                    TargetShaderController.ToDestinationCurve = ToDestinationCurve;
                    TargetShaderController.ToDestinationDuration = ToDestinationDuration;
                    TargetShaderController.ToDestinationValue = ToDestinationValue;
                    TargetShaderController.ToDestination();
                }                
            }
        }

        /// <summary>
        /// On reset we make our renderer stop flickering
        /// </summary>
        protected override void CustomReset()
        {
            base.CustomReset();
            if (Active && (TargetShaderController != null))
            {
                TargetShaderController.OneTimeDuration = _oneTimeDurationStorage;
                TargetShaderController.OneTimeAmplitude = _oneTimeAmplitudeStorage;
                TargetShaderController.OneTimeCurve = _oneTimeCurveStorage;
                TargetShaderController.OneTimeRemapMin = _oneTimeRemapMinStorage;
                TargetShaderController.OneTimeRemapMax = _oneTimeRemapMaxStorage;
                TargetShaderController.ToDestinationCurve = _toDestinationCurveStorage;
                TargetShaderController.ToDestinationDuration = _toDestinationDurationStorage;
                TargetShaderController.ToDestinationValue = _toDestinationValueStorage;
                TargetShaderController.RevertToInitialValueAfterEnd = _revertToInitialValueAfterEndStorage;
            }
        }

    }
}
