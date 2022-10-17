using System.IO;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NewLife.Editor
{
    public class GenerateXyzConfigAttribute : MonoBehaviour
    {
        [Tooltip("示例：如果为GuestConfig生成，只需填写Guest")]
        [LabelText("配置名")]
        public string ConfigName;

        [FoldoutGroup("配置")]
        [FilePath]
        [SerializeField]
        [LabelText("Attribute模板路径")]
        private string attributeTemplatePath =
            "Assets/_Game/Scripts/EditorOnly/GenerateXyzConfigAttribute/XyzConfigAttributeTemplate.txt";

        [FoldoutGroup("配置")]
        [FilePath]
        [SerializeField]
        [LabelText("AttributeDrawer模板路径")]
        private string attributeDrawerTemplatePath = "Assets/_Game/Scripts/EditorOnly/GenerateXyzConfigAttribute/XyzConfigAttributeDrawerTemplate.txt";

        [FoldoutGroup("配置")]
        [FolderPath]
        [SerializeField]
        [LabelText("Attribute目标目录")]
        private string attributeDir = "Assets/HelloMeow/CustomAttributes";

        [FoldoutGroup("配置")]
        [FolderPath]
        [SerializeField]
        [LabelText("AttributeDrawer目标目录")]
        private string attributeDrawerDir = "Assets/HelloMeow/Editor/CustomAttributes";

        [Button("生成")]
        private void Generate()
        {
            GenerateAttribute();
            GenerateAttributeDrawer();
        }

        private void GenerateAttribute()
        {
            var configName = ConfigName.Replace("Config", "");
            var filename   = $"{configName}ConfigAttribute.cs";
            var filepath   = Path.Combine(attributeDir, filename);

            Debug.Log($"Attribute生成路径:{filepath}");

            // 删除已有的(如果有)
            if (File.Exists(filepath))
            {
                File.Delete(filepath);
            }

            // 生成
            var template = File.ReadAllText(attributeTemplatePath);
            template = template.Replace("$[ConfigName]", configName);

            // 保存
            File.WriteAllText(filepath, template, System.Text.Encoding.UTF8);
        }

        private void GenerateAttributeDrawer()
        {
            var configName = ConfigName.Replace("Config", "");
            var filename   = $"{configName}ConfigAttributeDrawer.cs";
            var filepath   = Path.Combine(attributeDrawerDir, filename);

            Debug.Log($"AttributeDrawer生成路径:{filepath}");

            // 删除已有的(如果有)
            if (File.Exists(filepath))
            {
                File.Delete(filepath);
            }

            // 生成
            var template = File.ReadAllText(attributeDrawerTemplatePath);
            template = template.Replace("$[ConfigName]", configName);

            // 保存
            File.WriteAllText(filepath, template, System.Text.Encoding.UTF8);
        }
    }
}