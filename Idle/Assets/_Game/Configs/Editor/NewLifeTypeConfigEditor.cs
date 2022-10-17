/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2020-05-14 09:05:02
 * @modify date 2020-05-14 09:05:02
 * @desc [description]
 */

using HM.ConfigTool;
using HM.EditorOnly;
using UnityEditor;

namespace NewLife.Config
{
    public class NewLifeTypeConfigEditor : TypeConfigEditor
    {
        protected override ConfigCollectionEditor GetDetailedEditor()
        {
            return GetOrCreateDetailedWindow<NewLifeConfigCollectionEditor>();
        }

        /// <inheritdoc />
        protected override ConfigToolkit GetOrCreateConfigToolkit()
        {
            return new NewLifeConfigToolkit(new NewLifeTypeSerializer());
        }

        /// <inheritdoc />
        protected override string GetConfigFullTypeName(string typename)
        {
            return $"NewLife.Config.{typename}, NewLife.Configs";
        }

        protected override ConfigJsonExporter GetJsonExporter()
        {
            return new NewLifeConfigJsonExporter();
        }

        // 菜单入口
        [MenuItem("HelloMeowLab/Config Editor")]
        public new static void OpenView()
        {
            GetWindow(typeof(NewLifeTypeConfigEditor), false, "NewLife Config Editor");
        }
    }
}