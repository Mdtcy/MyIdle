/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2020-10-22 10:10:56
 * @modify date 2020-10-22 10:10:56
 * @desc [通过名字选择配置]
 */

using System;
using System.Collections.Generic;
using System.Linq;
using HM.Extensions;
using HM.GameBase;
using UnityEditor;
using UnityEngine;

namespace HM.CustomAttributes.Editor
{
    [CustomPropertyDrawer(typeof(BaseConfigAttribute))]
    public class BaseConfigAttributeDrawer : PropertyDrawer
    {
        /// <summary>
        /// Cache of the hash to use to resolve the ID for the drawer.
        /// </summary>
        private int idHash;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // By manually creating the control ID, we can keep the ID for the
            // label and button the same. This lets them be selected together
            // with the keyboard in the inspector, much like a normal popup.
            if (idHash == 0) idHash = "BaseConfigDrawer".GetHashCode();
            int id = GUIUtility.GetControlID(idHash, FocusType.Keyboard, position);

            label = EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, id, label);

            FetchAllConfigs();
            GUIContent buttonText;
            // If the enum has changed, a blank entry
            if (!Configs.Contains(property.objectReferenceValue)) {
                buttonText = new GUIContent();
            }
            else {
                buttonText = new GUIContent(Names[Configs.IndexOf(property.objectReferenceValue as BaseConfig)]);
            }

            if (DropdownButton(id, position, buttonText))
            {
                // 找到所有的配置
                FetchAllConfigs(true);
                Action<int> onSelect = i =>
                {
                    property.objectReferenceValue = Configs[i] as BaseConfig;
                    property.serializedObject.ApplyModifiedProperties();
                };

                SearchablePopup.Show(position, Names.ToArray(),
                    Configs.IndexOf(property.objectReferenceValue as BaseConfig), onSelect);
            }
            EditorGUI.EndProperty();
        }

        private static readonly List<BaseConfig> Configs = new List<BaseConfig>();
        private static readonly List<string> Names = new List<string>();

        private static void FetchAllConfigs(bool force = false)
        {
            if (!force && !Configs.IsNullOrEmpty() && !Names.IsNullOrEmpty())
            {
                return;
            }

            Configs.Clear();
            Names.Clear();

            Configs.Add(null);
            Names.Add("--未设置--");

            foreach (var guid in AssetDatabase.FindAssets("t:BaseConfig"))
            {
                var config = AssetDatabase.LoadAssetAtPath<BaseConfig>(AssetDatabase.GUIDToAssetPath(guid));
                Configs.Add(config);
                Names.Add($"{config.Name}({config.Id})");
            }
        }

        private static bool DropdownButton(int id, Rect position, GUIContent content)
        {
            Event current = Event.current;
            switch (current.type)
            {
                case EventType.MouseDown:
                    if (position.Contains(current.mousePosition) && current.button == 0)
                    {
                        Event.current.Use();
                        return true;
                    }
                    break;
                case EventType.KeyDown:
                    if (GUIUtility.keyboardControl == id && current.character =='\n')
                    {
                        Event.current.Use();
                        return true;
                    }
                    break;
                case EventType.Repaint:
                    EditorStyles.popup.Draw(position, content, id, false);
                    break;
            }
            return false;
        }
    }
}