/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2022-09-13 10:43:04
 * @modify date 2022-09-13 10:43:04
 * @desc [description]
 */

using NewLife.UI.AudioManager;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

#pragma warning disable 0649
namespace NewLife.EditorOnly
{
    public class AudioSwitchDebugger : MonoBehaviour
    {
        [Inject]
        private IAudioSwitch audioSwitch;

        [FoldoutGroup("音乐")]
        [Button("静音", ButtonSizes.Medium)]
        private void MuteMusic()
        {
            audioSwitch.MuteMusic();
        }

        [FoldoutGroup("音乐")]
        [Button("取消静音", ButtonSizes.Medium)]
        private void UnmuteMusic()
        {
            audioSwitch.UnmuteMusic();
        }

        [FoldoutGroup("音效")]
        [Button("静音", ButtonSizes.Medium)]
        private void MuteEffect()
        {
            audioSwitch.MuteEffect();
        }

        [FoldoutGroup("音效")]
        [Button("取消静音", ButtonSizes.Medium)]
        private void UnmuteEffect()
        {
            audioSwitch.UnmuteEffect();
        }

    }
}
#pragma warning restore 0649