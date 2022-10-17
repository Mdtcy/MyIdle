/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2020-10-22 10:10:56
 * @modify date 2020-10-22 10:10:56
 * @desc [description]
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HM.Extensions;
using HM.GameBase;
using HM.Notification;
using UnityEditor;
using UnityEngine;

namespace HM.CustomAttributes.Editor
{
    [CustomPropertyDrawer(typeof(NotifyKeysAttribute))]
    public class NotifyKeysAttributeDrawer : PropertyDrawer
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
            if (idHash == 0) idHash = "SearchableEnumDrawer".GetHashCode();
            int id = GUIUtility.GetControlID(idHash, FocusType.Keyboard, position);

            label = EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, id, label);

            FetchNotifyKeys();
            GUIContent buttonText;
            // If the enum has changed, a blank entry
            if (!notifyKeys.Contains(property.intValue)) {
                buttonText = new GUIContent();
            }
            else {
                buttonText = new GUIContent(notifyKeyNames[notifyKeys.IndexOf(property.intValue)]);
            }

            if (DropdownButton(id, position, buttonText))
            {
                // 找到所有的notifyKey
                FetchNotifyKeys(true);
                Action<int> onSelect = i =>
                {
                    property.intValue = notifyKeys[i];
                    property.serializedObject.ApplyModifiedProperties();
                };

                SearchablePopup.Show(position, notifyKeyNames.ToArray(),
                    notifyKeys.IndexOf(property.intValue), onSelect);
            }
            EditorGUI.EndProperty();
        }

        private static readonly List<int> notifyKeys = new List<int>();
        private static readonly List<string> notifyKeyNames = new List<string>();

        // 获取所有NotifyKeys及其子类类型
        private static List<Type> GetNotifyKeysTypes()
        {
            return (
                from domainAssembly in AppDomain.CurrentDomain.GetAssemblies()

                // alternative: from domainAssembly in domainAssembly.GetExportedTypes()
                from assemblyType in domainAssembly.GetTypes()
                where typeof(NotifyKeys).IsAssignableFrom(assemblyType)

                // alternative: where assemblyType.IsSubclassOf(typeof(B))
                // alternative: && ! assemblyType.IsAbstract
                select assemblyType).ToList();
        }

        private static void FetchNotifyKeys(bool force = false)
        {
            if (!force && !notifyKeys.IsNullOrEmpty() && !notifyKeyNames.IsNullOrEmpty())
            {
                return;
            }

            notifyKeys.Clear();
            notifyKeyNames.Clear();

            foreach (var type in GetNotifyKeysTypes())
            {
                foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.Static))
                {
                    var name = field.Name;
                    var key  = (int) field.GetValue(null);
                    notifyKeys.Add(key);
                    notifyKeyNames.Add(name);
                }
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