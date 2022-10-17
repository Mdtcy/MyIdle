using UnityEditor;
using UnityEngine;
namespace HM.AssetEditing
{
    public static class HMEditorUtility
    {
        public static void Popup(string content, params object[] objects)
        {
            Popup(EditorWindow.focusedWindow, content, objects);
        }

         public static void Popup(EditorWindow window, string content, params object[] objects)
        {
            if (objects.Length > 0)
            {
                window.ShowNotification(new GUIContent(string.Format(content, objects)));
            }
            else
            {
                window.ShowNotification(new GUIContent(content));
            }
        }
    }
}