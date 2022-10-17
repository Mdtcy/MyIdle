/**
 * @author [Sligh]
 * @email [zsaveyou@163.com]
 * @create date 2021-01-07 18:27:41
 * @desc [用于播放音效]
 */

#pragma warning disable 0649

using UnityEngine;

namespace NewLife.UI.AudioManager
{
    /// <summary>
    /// 用于播放音效
    /// </summary>
    public class EffectPlayer : MonoBehaviour, IEffectAudioPlayer
    {
        #region FIELDS

        // 用于播放音频
        [SerializeField]
        private AudioSource audioSourceEffect;

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        /// <inheritdoc />
        public void Play(AudioClip clip)
        {
            audioSourceEffect.PlayOneShot(clip);
        }

        /// <inheritdoc />
        public void StopAll()
        {
            audioSourceEffect.Stop();
        }

        /// <inheritdoc />
        public void Mute()
        {
            audioSourceEffect.mute = true;
        }

        /// <inheritdoc />
        public void Unmute()
        {
            audioSourceEffect.mute = false;
        }

        /// <inheritdoc />
        public bool IsMuted()
        {
            return audioSourceEffect.mute;
        }

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        #endregion

        #region STATIC FIELDS

        #endregion

        #region STATIC METHODS

        #endregion

    }
}

#pragma warning restore 0649