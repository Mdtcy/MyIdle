/**
 * @author [Sligh]
 * @email [zsaveyou@163.com]
 * @create date 2021-01-05 13:40:12
 * @desc [播放环境音]
 */

using UnityEngine;

namespace NewLife.UI.AudioManager
{
    /// <summary>
    /// 播放环境音
    /// </summary>
    public interface IAmbientAudioPlayer
    {
        /// <summary>
        /// 播放环境音
        /// </summary>
        /// <param name="audioClip"></param>
        void Play(AudioClip audioClip);

        /// <summary>
        /// 停止环境音
        /// </summary>
        void Stop();

        /// <summary>
        /// 静音
        /// </summary>
        void Mute();

        /// <summary>
        /// 取消静音
        /// </summary>
        void Unmute();

        /// <summary>
        /// 是否静音
        /// </summary>
        /// <returns></returns>
        bool IsMuted();
    }
}