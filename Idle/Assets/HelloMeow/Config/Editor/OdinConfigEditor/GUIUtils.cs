/**
 * @author [Boluo]
 * @email [tktetb@163.com]
 * @create date 2022年10月17日 16:18:54
 * @modify date 2022年10月17日 16:18:54
 * @desc []
 */

#pragma warning disable 0649
namespace HelloMeow.Config.Editor.OdinConfigEditor
{
    using Sirenix.Utilities;
    using System;
    using UnityEditor;
    using UnityEngine;
    using Sirenix.Utilities.Editor;

    public static class GUIUtils
    {
        public static bool SelectButtonList(ref Type selectedType, Type[] typesToDisplay)
        {
            var rect = GUILayoutUtility.GetRect(0, 25);

            for (int i = 0; i < typesToDisplay.Length; i++)
            {
                var name    = typesToDisplay[i].Name;
                var btnRect = rect.Split(i, typesToDisplay.Length);

                if (GUIUtils.SelectButton(btnRect, name, typesToDisplay[i] == selectedType))
                {
                    selectedType = typesToDisplay[i];

                    return true;
                }
            }

            return false;
        }

        public static bool SelectButton(Rect rect, string name, bool selected)
        {
            if (GUI.Button(rect, GUIContent.none, GUIStyle.none))
                return true;

            if (Event.current.type == EventType.Repaint)
            {
                var style = new GUIStyle(EditorStyles.miniButtonMid);
                style.stretchHeight = true;
                style.fixedHeight   = rect.height;
                style.Draw(rect, GUIHelper.TempContent(name), false, false, selected, false);
            }


            return false;
        }

    }
}
#pragma warning restore 0649