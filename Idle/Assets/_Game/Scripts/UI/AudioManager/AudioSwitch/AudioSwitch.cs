/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date 11:49:48
 * @modify date 11:49:48
 * @desc [声音开关]
 */

#pragma warning disable 0649
using Zenject;

namespace NewLife.UI.AudioManager
{
    /// <summary>
    /// 声音开关
    /// </summary>
    public class AudioSwitch : IAudioSwitch
    {
        #region FIELDS

        // * inject
        private IAmbientAudioPlayer    ambientAudioPlayer;
        private IBackgroundMusicPlayer backgroundMusicPlayer;
        private IEffectAudioPlayer     effectAudioPlayer;
        private INpcAudioPlayer        npcAudioPlayer;

        #endregion

        #region PUBLIC METHODS

        [Inject]
        public void Construct(IAmbientAudioPlayer    ambientAudioPlayer,
                              IBackgroundMusicPlayer backgroundMusicPlayer,
                              IEffectAudioPlayer     effectAudioPlayer,
                              INpcAudioPlayer        npcAudioPlayer)
        {
            this.ambientAudioPlayer    = ambientAudioPlayer;
            this.npcAudioPlayer        = npcAudioPlayer;
            this.backgroundMusicPlayer = backgroundMusicPlayer;
            this.effectAudioPlayer     = effectAudioPlayer;
        }

        public void MuteMusic()
        {
            backgroundMusicPlayer.Mute();
            ambientAudioPlayer.Mute();
            npcAudioPlayer.Mute();
        }

        public void UnmuteMusic()
        {
            backgroundMusicPlayer.Unmute();
            ambientAudioPlayer.Unmute();
            npcAudioPlayer.Unmute();
        }

        /// <inheritdoc />
        public bool IsMusicMuted()
        {
            return backgroundMusicPlayer.IsMuted() && ambientAudioPlayer.IsMuted() && npcAudioPlayer.IsMuted();
        }

        public void MuteEffect()
        {
            effectAudioPlayer.Mute();
        }

        public void UnmuteEffect()
        {
            effectAudioPlayer.Unmute();
        }

        /// <inheritdoc />
        public bool IsEffectMuted()
        {
            return effectAudioPlayer.IsMuted();
        }

        #endregion

        #region PRIVATE METHODS

        #endregion
    }
}

#pragma warning restore 0649