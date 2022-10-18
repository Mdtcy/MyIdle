/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2020-02-03 16:02:37
 * @modify date 2020-02-03 16:02:37
 * @desc [description]
 */

#pragma warning disable 0649

#if UNITY_EDITOR
using System.IO;
using HM;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEditor.AddressableAssets;
using System.Linq;
#endif
using UnityEngine;

namespace NewLife.EditorOnly
{
    public class AddressablesAssetAddressEditor : MonoBehaviour
    {
#if UNITY_EDITOR

        #region Label管理

        [BoxGroup("Label管理")]
        [SerializeField]
        private string label;

        [BoxGroup("Label管理")]
        [Button("添加Label", ButtonSizes.Medium)]
        private void AddLabel()
        {
            if (!label.IsNullOrWhitespace())
            {
                var settings = AddressableAssetSettingsDefaultObject.Settings;
                settings.AddLabel(label);
                HMLog.LogInfo($"添加label = {label}");
            }
        }

        [BoxGroup("批量设置Address")]
        [SerializeField]
        private string groupName;

        [BoxGroup("批量设置Address")]
        [Button("按Asset文件名设置Address", ButtonSizes.Medium)]
        private void SetFilenameAsAddressForGivenGroup()
        {
            if (groupName.IsNullOrWhitespace())
            {
                return;
            }

            var settings = AddressableAssetSettingsDefaultObject.Settings;
            var group    = settings.FindGroup(groupName);

            if (group == null)
            {
                HMLog.LogWarning($"Failed to find group [name={groupName}]");

                return;
            }

            var entries = group.entries.ToList();

            for (int i = 0; i < entries.Count; i++)
            {
                var entry   = entries[i];
                var address = Path.GetFileNameWithoutExtension(entry.AssetPath);

                // filename is like 'Achievement204001001'
                entry.SetAddress(address);
                HMLog.LogInfo($"Set entry[{i}/{entries.Count}]'s address to {address} | path = {entry.AssetPath}");
            }
        }

        #endregion

#endif
    }
}
#pragma warning restore 0649
