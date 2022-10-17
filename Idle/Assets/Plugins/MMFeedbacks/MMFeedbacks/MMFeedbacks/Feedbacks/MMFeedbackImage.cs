﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MoreMountains.Feedbacks
{
    /// <summary>
    /// This feedback will let you change the color of a target sprite renderer over time, and flip it on X or Y. You can also use it to command one or many MMSpriteRendererShakers.
    /// </summary>
    [AddComponentMenu("")]
    [FeedbackHelp("This feedback will let you change the color of a target Image over time. You can also use it to command one or many MMImageShakers.")]
    [FeedbackPath("UI/Image")]
    public class MMFeedbackImage : MMFeedback
    {
        /// sets the inspector color for this feedback
#if UNITY_EDITOR
        public override Color FeedbackColor { get { return MMFeedbacksInspectorColors.UIColor; } }
#endif

        /// the possible modes for this feedback
        public enum Modes { OverTime, Instant, ShakerEvent }

        [Header("Sprite Renderer")]
        /// the Image to affect when playing the feedback
        public Image BoundImage;
        /// whether the feedback should affect the Image instantly or over a period of time
        public Modes Mode = Modes.OverTime;
        /// how long the Image should change over time
        [MMFEnumCondition("Mode", (int)Modes.OverTime, (int)Modes.ShakerEvent)]
        public float Duration = 0.2f;
        /// whether or not that Image should be turned off on start
        public bool StartsOff = false;
        /// the channel to broadcast on
        [MMFEnumCondition("Mode", (int)Modes.ShakerEvent)]
        public int Channel = 0;
        /// whether or not to reset shaker values after shake
        [MMFEnumCondition("Mode", (int)Modes.ShakerEvent)]
        public bool ResetShakerValuesAfterShake = true;
        /// whether or not to reset the target's values after shake
        [MMFEnumCondition("Mode", (int)Modes.ShakerEvent)]
        public bool ResetTargetValuesAfterShake = true;
        /// whether or not to broadcast a range to only affect certain shakers
        [MMFEnumCondition("Mode", (int)Modes.ShakerEvent)]
        public bool UseRange = false;
        /// the range of the event, in units
        [MMFEnumCondition("Mode", (int)Modes.ShakerEvent)]
        public float EventRange = 100f;
        /// the transform to use to broadcast the event as origin point
        [MMFEnumCondition("Mode", (int)Modes.ShakerEvent)]
        public Transform EventOriginTransform;
        
        [Header("Color")]
        public bool ModifyColor = true;
        /// the colors to apply to the Image over time
        [MMFEnumCondition("Mode", (int)Modes.OverTime, (int)Modes.ShakerEvent)]
        public Gradient ColorOverTime;
        /// the color to move to in instant mode
        [MMFEnumCondition("Mode", (int)Modes.Instant, (int)Modes.ShakerEvent)]
        public Color InstantColor;

        /// the duration of this feedback is the duration of the Image, or 0 if instant
        public override float FeedbackDuration { get { return (Mode == Modes.Instant) ? 0f : Duration; } }

        /// <summary>
        /// On init we turn the Image off if needed
        /// </summary>
        /// <param name="owner"></param>
        protected override void CustomInitialization(GameObject owner)
        {
            base.CustomInitialization(owner);

            if (EventOriginTransform == null)
            {
                EventOriginTransform = this.transform;
            }

            if (Active)
            {
                if (StartsOff)
                {
                    Turn(false);
                }
            }
        }

        /// <summary>
        /// On Play we turn our Image on and start an over time coroutine if needed
        /// </summary>
        /// <param name="position"></param>
        /// <param name="attenuation"></param>
        protected override void CustomPlayFeedback(Vector3 position, float attenuation = 1.0f)
        {
            if (Active)
            {
                Turn(true);
                switch (Mode)
                {
                    case Modes.Instant:
                        if (ModifyColor)
                        {
                            BoundImage.color = InstantColor;
                        }
                        break;
                    case Modes.OverTime:
                        StartCoroutine(ImageSequence());
                        break;
                    case Modes.ShakerEvent:
                        /*MMImageShakeEvent.Trigger(Duration, ModifyColor, ColorOverTime, 
                            attenuation,
                            Channel, ResetShakerValuesAfterShake, ResetTargetValuesAfterShake,
                            UseRange, EventRange, EventOriginTransform.position);*/
                        break;
                }
            }
        }

        /// <summary>
        /// This coroutine will modify the values on the Image
        /// </summary>
        /// <returns></returns>
        protected virtual IEnumerator ImageSequence()
        {
            float journey = 0f;

            while (journey < Duration)
            {
                float remappedTime = MMFeedbacksHelpers.Remap(journey, 0f, Duration, 0f, 1f);

                SetImageValues(remappedTime);

                journey += FeedbackDeltaTime;
                yield return null;
            }
            SetImageValues(1f);
            if (StartsOff)
            {
                Turn(false);
            }            
            yield return null;
        }

        /// <summary>
        /// Sets the various values on the sprite renderer on a specified time (between 0 and 1)
        /// </summary>
        /// <param name="time"></param>
        protected virtual void SetImageValues(float time)
        {
            if (ModifyColor)
            {
                BoundImage.color = ColorOverTime.Evaluate(time);
            }
        }

        /// <summary>
        /// Turns the sprite renderer off on stop
        /// </summary>
        /// <param name="position"></param>
        /// <param name="attenuation"></param>
        protected override void CustomStopFeedback(Vector3 position, float attenuation = 1)
        {
            base.CustomStopFeedback(position, attenuation);
            if (Active)
            {
                Turn(false);
            }
        }

        /// <summary>
        /// Turns the sprite renderer on or off
        /// </summary>
        /// <param name="status"></param>
        protected virtual void Turn(bool status)
        {
            BoundImage.gameObject.SetActive(status);
            BoundImage.enabled = status;
        }
    }
}
