/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2019-10-27 17:10:00
 * @modify date 2019-10-27 17:10:00
 * @desc [description]
 */

using System;
using System.IO;
using System.Linq;
using System.Reflection;
using HM.ConfigTool;
using UnityEditor;
using UnityEngine;

namespace HM.GameBase
{
    public class EditorWindowBase : EditorWindow
    {
        #region FIELDS

        /// <summary>
        /// 全局配置
        /// </summary>
        private ConfigSettings setting;

        /// <summary>
        /// 全局配置所在路径
        /// </summary>
        private string settingPath;

		#endregion

		#region PROPERTIES

		protected ConfigSettings Settings => setting;
		#endregion

		#region PUBLIC METHODS
		#endregion

		#region PROTECTED METHODS
		// 显示弹框提示
		protected void Popup(string content, params object[] objects)
		{
			ShowNotification(objects.Length > 0
				                 ? new GUIContent(string.Format(content, objects))
				                 : new GUIContent(content));
		}

		protected virtual void OnGUI()
		{
			if (setting == null)
			{
				AutoFindSettings();
			}
		}

		protected T CreateNewConfig<T>(TypeConfig typeConfig) where T : BaseConfig
		{
			var dir = Path.Combine(setting.ConfigAssetRoot, typeConfig.ConfigName);
            Directory.CreateDirectory(dir);

            int id = NextId(typeConfig);

            var asset = ScriptableObject.CreateInstance(GetTypeFromConfigName(typeConfig.ConfigName));
            ((BaseConfig)asset).Id = id;
            AssetDatabase.CreateAsset(asset, Path.Combine(dir, ConfigFileName(typeConfig, id) + ".asset"));
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            return (T)asset;
		}

		protected Type GetTypeFromConfigName(string configName)
		{
			return Type.GetType($"FTCF.{configName}Config, Assembly-CSharp");
		}

		protected FieldInfo[] GetInheritedFields(Type classType)
		{
			return classType.BaseType.GetFields(BindingFlags.Public | BindingFlags.Instance);
		}

		private int NextId(TypeConfig typeConfig)
		{
			string configName = typeConfig.ConfigName;
            var dir = Path.Combine(setting.ConfigAssetRoot, configName);

            var filenames = AssetDatabase.FindAssets($"t:{configName}Config", new[] {dir}).Select(guid => Path.GetFileNameWithoutExtension(AssetDatabase.GUIDToAssetPath(guid))).ToList();

            int i = 1;
            for (; i <= filenames.Count; i++)
            {
                int id = ConfigId(typeConfig, i);
                if (!filenames.Contains(ConfigFileName(typeConfig, id)))
                {
                    return id;
                }
            }
            return ConfigId(typeConfig, i);
        }

		#endregion

		#region PRIVATE METHODS

		private int ConfigId(TypeConfig typeConfig, int idx)
        {
            return Convert.ToInt32($"{typeConfig.MajorType:D3}{typeConfig.MinorType:D3}{idx:D3}");
        }

        private string ConfigFileName(TypeConfig typeConfig, int id)
        {
            return $"{typeConfig.ConfigName}{id}";
        }

		/// <summary>
        /// 刷新全局配置
        /// </summary>
        private void AutoFindSettings()
        {
            settingPath = string.Empty;
            AssetDatabase.Refresh();
            var guids = AssetDatabase.FindAssets("t:ConfigSettings");
            if (guids.Length > 0)
            {
                settingPath = AssetDatabase.GUIDToAssetPath(guids[0]);
                setting = AssetDatabase.LoadAssetAtPath<ConfigSettings>(settingPath);
            }
        }
		#endregion

		#region STATIC METHODS
		#endregion
    }
}