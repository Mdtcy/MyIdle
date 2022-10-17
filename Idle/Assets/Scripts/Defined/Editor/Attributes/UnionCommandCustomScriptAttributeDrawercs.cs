/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2020-10-22 10:10:56
 * @modify date 2020-10-22 10:10:56
 * @desc [通过名字选择配置]
 */

using System;
using System.Collections.Generic;
using System.Reflection;
using HM.CustomAttributes.Editor;
using HM.Extensions;
using NewLife.Defined.Condition;
using NewLife.Defined.CustomAttributes;
using UnityEditor;
using UnityEngine;

namespace NewLife.CustomAttributes.Editor
{
    [CustomPropertyDrawer(typeof(UnionCommandCustomScriptAttribute))]
    public class UnionCommandCustomScriptAttributeDrawer : PropertyDrawer
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
            if (idHash == 0) idHash = "UnionCommandCustomScriptAttributeDrawer".GetHashCode();

            int id = GUIUtility.GetControlID(idHash, FocusType.Keyboard, position);

            label    = EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, id, label);

            FetchAllConfigs();
            GUIContent buttonText;

            // If the enum has changed, a blank entry
            if (!Names.Contains(property.stringValue))
            {
                buttonText = new GUIContent();
            }
            else
            {
                buttonText = new GUIContent(property.stringValue);
            }

            if (DropdownButton(id, position, buttonText))
            {
                // 找到所有的配置
                FetchAllConfigs(true);
                Action<int> onSelect = i =>
                {
                    property.stringValue = Names[i];
                    property.serializedObject.ApplyModifiedProperties();
                };

                SearchablePopup.Show(position, Names.ToArray(),
                                     Names.IndexOf(property.stringValue), onSelect);
            }

            EditorGUI.EndProperty();
        }

        private static readonly List<string> Names = new List<string>();

        private static void FetchAllConfigs(bool force = false)
        {
            if (!force && !Names.IsNullOrEmpty())
            {
                return;
            }

            Names.Clear();

            // fetch all names
            var fields = typeof(UnionCommandCustomScriptType).GetFields(BindingFlags.Static | BindingFlags.Public);

            foreach (var info in fields)
            {
                Names.Add(info.Name);
            }

            Names.Sort();
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
                    if (GUIUtility.keyboardControl == id && current.character == '\n')
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