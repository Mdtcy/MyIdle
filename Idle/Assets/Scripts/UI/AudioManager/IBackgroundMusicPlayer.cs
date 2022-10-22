/**
 * @author [Sligh]
 * @email [zsaveyou@163.com]
 * @create date 2020-12-22 18:14:01
 * @desc [播放背景音乐的接口]
 */

using UnityEngine;

namespace NewLife.UI.AudioManager
{
    /// <summary>
    /// 播放背景音乐的接口
    /// </summary>
    public interface IBackgroundMusicPlayer
    {
        /// <summary>
        /// 播放背景音乐
        /// </summary>
        /// <param name="audioClip"></param>
        void Play(AudioClip audioClip);

        /// <summary>
        /// 暂停背景音乐
        /// </summary>
        void Pause();

        /// <summary>
        /// 恢复背景音乐
        /// </summary>
        void Resume();

        /// <summary>
        /// 停止背景音乐
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