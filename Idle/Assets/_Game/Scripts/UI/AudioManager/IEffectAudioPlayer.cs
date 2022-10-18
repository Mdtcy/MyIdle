/**
 * @author [Sligh]
 * @email [zsaveyou@163.com]
 * @create date 2020-12-22 18:13:17
 * @desc [播放音效的接口]
 */

using UnityEngine;

namespace NewLife.UI.AudioManager
{
    /// <summary>
    /// 播放音效的接口
    /// </summary>
    public interface IEffectAudioPlayer
    {
        /// <summary>
        /// 播放音效
        /// </summary>
        /// <param name="clip"></param>
        void Play(AudioClip clip);

        /// <summary>
        /// 停止全部音效
        /// </summary>
        void StopAll();

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