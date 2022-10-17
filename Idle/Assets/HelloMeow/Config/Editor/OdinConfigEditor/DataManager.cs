/**
 * @author [Boluo]
 * @email [tktetb@163.com]
 * @create date 2022年10月17日 15:59:57
 * @modify date 2022年10月17日 15:59:57
 * @desc []
 */

using Sirenix.OdinInspector.Editor;
using System;
using System.Linq;
using HM.GameBase;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

#pragma warning disable 0649
namespace HelloMeow.Config.Editor.OdinConfigEditor
{
    public class DataManager : OdinMenuEditorWindow
    {
        private static Type[] typesToDisplay = TypeCache.GetTypesWithAttribute<ManageableDataAttribute>()
                                                        .OrderBy(m => m.Name)
                                                        .ToArray();

        private Type selectedType;

        [MenuItem("Tools/Data Manager")]
        private static void OpenEditor() => GetWindow<DataManager>();

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree();
            tree.DefaultMenuStyle.IconSize = 28.00f;
            tree.Config.DrawSearchToolbar  = true;

            foreach (var type in typesToDisplay)
            {
                tree.AddAllAssetsAtPath(type.Name, "Assets/", type, true, true)
                    .ForEach(AddDragHandles);
            }

            tree.EnumerateTree().Where(x => x.Value as BaseConfig).ForEach(AddDragHandles);
            tree.EnumerateTree().AddIcons<BaseConfig>(x => x.Image);

            foreach (var odinMenuItem in tree.EnumerateTree())
            {
                if (odinMenuItem.Value is BaseConfig config)
                {
                    odinMenuItem.Name         = $"{config.Name} {config.name}";

                    // 如果有需要可以加上更多信息以支持搜索
                    odinMenuItem.SearchString = $"{config.Name} {config.name}";
                }
            }

            return tree;
        }

        /// <inheritdoc />
        protected override void OnBeginDrawEditors()
        {
            // todo 不是很好的方式
            var selected      = this.MenuTree.Selection.FirstOrDefault();
            var toolbarHeight = this.MenuTree.Config.SearchToolbarHeight;

            // Draws a toolbar with the name of the currently selected menu item.
            SirenixEditorGUI.BeginHorizontalToolbar(toolbarHeight);
            {
                if (selected != null)
                {
                    GUILayout.Label(selected.Name);
                }

                if (SirenixEditorGUI.ToolbarButton(new GUIContent("Create Item")))
                {
                    ScriptableObjectCreator.ShowDialog<BaseConfig>("Assets/",
                                                             obj =>
                                                             {
                                                                 obj.Name = obj.name;
                                                                 base
                                                                    .TrySelectMenuItemWithObject(obj); // Selects the newly created item in the editor
                                                             });
                }
            }

            SirenixEditorGUI.EndHorizontalToolbar();

        }

        private void AddDragHandles(OdinMenuItem menuItem)
        {
            menuItem.OnDrawItem += x => DragAndDropUtilities.DragZone(menuItem.Rect, menuItem.Value, false, false);
        }

    }
}
#pragma warning restore 0649