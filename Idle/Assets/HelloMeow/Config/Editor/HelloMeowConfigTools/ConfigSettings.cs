using System.IO;
using UnityEditorInternal;
using UnityEngine;
#pragma warning disable 0649
namespace HM.ConfigTool
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "ConfigSettings", menuName = "Config/Settings")]
    public class ConfigSettings : ScriptableObject
    {
        [Comment("Root:配置工具相对于Assets/的路径")]
        public string root = "Assets/Configs";

        [Comment("ConfigAssetRoot:配置文件(.asset)所在目录")]
        [SerializeField]
        private string configAssetRoot = "{ROOT}/Assets/Resources";

        public string ConfigAssetRoot => configAssetRoot.Replace("{ROOT}", root);

        [Comment("TypeConfigAssetRoot:类型配置文件(.asset)所在目录")]
        [SerializeField]
        private string typeConfigAssetRoot = "{ROOT}/Assets/TypeConfigAssets";
        public string TypeConfigAssetRoot => typeConfigAssetRoot.Replace("{ROOT}", root);

        [Comment("ConfigClassOutputRoot:类型配置类文件(XyzConfig.cs)所在目录")]
        [SerializeField]
        private string configClassOutputRoot = "{ROOT}/Assets/Class";
        public string ConfigClassOutputRoot
        {
            get
            {
                var subpath = configClassOutputRoot.Replace("{ROOT}", root);
                if (subpath.StartsWith("Assets/"))
                {
                    subpath = subpath.Remove(0, 7);
                }
                return Path.Combine(Application.dataPath, subpath);
            }
        }

        #region TemplateRoot

        [Comment("TemplateRoot:XyzConfig.cs和XyzConfigEditor.cs模板所在目录")]
        [SerializeField]
        private string templateRoot = "Assets/HelloMeow/Config/Templates";
        public string TemplateRoot
        {
            get
            {
                var subpath = templateRoot.Replace("{ROOT}", root);
                if (subpath.StartsWith("Assets/"))
                {
                    subpath = subpath.Remove(0, 7);
                }
                return Path.Combine(Application.dataPath, subpath);
            }
        }

        #endregion

        #region Namespace

        [Comment("配置Config的namespace")]
        [SerializeField]
        private string configNamespace;
        public string ConfigNamespace => configNamespace;

        #endregion

        #region Assembly

        [Comment("配置所在Assembly")]
        [SerializeField]
        private AssemblyDefinitionAsset assemblyDefinition;
        public string AssemblyName => assemblyDefinition.name;

        #endregion

    }
}
#pragma warning restore 0649