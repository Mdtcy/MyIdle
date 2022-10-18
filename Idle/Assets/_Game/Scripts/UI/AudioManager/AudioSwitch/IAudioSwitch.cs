/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date 11:45:32
 * @modify date 11:45:32
 * @desc [声音管理器接口]
 */

#pragma warning disable 0649
namespace NewLife.UI.AudioManager
{
    public interface IAudioSwitch
    {
        /// <summary>
        /// 静音音乐
        /// </summary>
        void MuteMusic();

        /// <summary>
        /// 取消静音音乐
        /// </summary>
        void UnmuteMusic();

        /// <summary>
        /// 是否静音
        /// </summary>
        /// <returns></returns>
        bool IsMusicMuted();

        /// <summary>
        /// 静音音效
        /// </summary>
        void MuteEffect();

        /// <summary>
        /// 取消静音音效
        /// </summary>
        void UnmuteEffect();

        /// <summary>
        /// 是否静音
        /// </summary>
        /// <returns></returns>
        bool IsEffectMuted();
    }
}
#pragma warning restore 0649