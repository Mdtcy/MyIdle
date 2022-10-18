/**
 * @author [Boluo]
 * @email [tktetb@163.com]
 * @create date 10:12:12
 * @modify date 10:12:12
 * @desc [播放NPC音效]
 */

using UnityEngine;

#pragma warning disable 0649
namespace NewLife.UI.AudioManager
{
    /// <summary>
    /// 播放NPC音效
    /// </summary>
    public class NpcAudioPlayer : MonoBehaviour, INpcAudioPlayer
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
            if (audioSourceEffect.isPlaying)
            {
                Stop();
            }
            audioSourceEffect.PlayOneShot(clip);
        }

        /// <inheritdoc />
        public void Stop()
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

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649