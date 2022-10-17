/**
 * @author [Sligh]
 * @email [zsaveyou@163.com]
 * @create date 2021-01-07 19:42:21
 * @desc [用于播放环境音效]
 */

#pragma warning disable 0649

using UnityEngine;

namespace NewLife.UI.AudioManager
{
    /// <summary>
    /// 用于播放环境音效
    /// </summary>
    public class AmbientAudioPlayer : MonoBehaviour, IAmbientAudioPlayer
    {
        #region FIELDS

        // 负责环境音效
        [SerializeField]
        private AudioSource audioSourceAmbient;

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        #endregion

        #region STATIC FIELDS

        #endregion

        #region STATIC METHODS

        #endregion


        #region IAmbientAudioPlayer

        /// <inheritdoc />
        public void Play(AudioClip audioClip)
        {
            audioSourceAmbient.clip = audioClip;
            audioSourceAmbient.Play();
        }

        /// <inheritdoc />
        public void Stop()
        {
            audioSourceAmbient.Stop();
        }

        /// <inheritdoc />
        public void Mute()
        {
            audioSourceAmbient.mute = true;
        }

        /// <inheritdoc />
         public void Unmute()
         {
             audioSourceAmbient.mute = false;
         }

        /// <inheritdoc />
        public bool IsMuted()
        {
            return audioSourceAmbient.mute;
        }

        #endregion

    }
}

#pragma warning restore 0649