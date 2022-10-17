/**
 * @author [Sligh]
 * @email [zsaveyou@163.com]
 * @create date 2021-01-07 19:35:54
 * @desc [负责播放背景音乐]
 */

#pragma warning disable 0649

using UnityEngine;

namespace NewLife.UI.AudioManager
{
    /// <summary>
    /// 负责播放背景音乐
    /// </summary>
    public class BackgroundMusicPlayer : MonoBehaviour, IBackgroundMusicPlayer
    {
        #region FIELDS

        // 负责播放背景音乐
        [SerializeField]
        private AudioSource audioSourceBackground;

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

        #region IBackgroundMusicPlayer

        /// <inheritdoc />
        public void Play(AudioClip audioClip)
        {
            audioSourceBackground.clip = audioClip;
            audioSourceBackground.Play();
        }

        /// <inheritdoc />
        public void Pause()
        {
            audioSourceBackground.Pause();
        }

        /// <inheritdoc />
        public void Resume()
        {
            audioSourceBackground.Play();
        }

        /// <inheritdoc />
        public void Stop()
        {
            audioSourceBackground.Stop();
        }

        /// <inheritdoc />
        public void Mute()
        {
            audioSourceBackground.mute = true;
        }

        /// <inheritdoc />
        public void Unmute()
        {
            audioSourceBackground.mute = false;
        }

        /// <inheritdoc />
        public bool IsMuted()
        {
            return audioSourceBackground.mute;
        }

        #endregion
    }
}

#pragma warning restore 0649