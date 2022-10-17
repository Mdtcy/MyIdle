using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace HM.EditorOnly
{
    public class QuickOpenScene : EditorWindow
    {
        private List<FileInfo> paths;
        private List<FileInfo> filteredPaths;
        private string filter;
        private bool focus;
        private int index;
        private Vector2 pos;

        private void OnEnable()
        {
            paths = new List<FileInfo>();
            filteredPaths = new List<FileInfo>();
            ReloadScenes();
        }

        private void OnGUI()
        {
            pos = GUILayout.BeginScrollView(pos);
            var color = GUI.color;
            for (var i = 0; i < filteredPaths.Count; i++)
            {
                var path = filteredPaths[i];
                if (i == index)
                {
                    GUI.color = new Color(0x45 / 255f, 0xBD / 255f, 0xF2 / 255f);
                }
                if (GUILayout.Button(path.Name.Replace(Path.GetExtension(path.Name), "")))
                {
                    OpenScene(path);
                }
                GUI.color = color;
            }
            GUILayout.EndScrollView();
            DrawSearchText();
            DrawReloadButton();
        }

        private void DrawReloadButton()
        {
            if (GUILayout.Button("刷新场景", GUI.skin.GetStyle("Button")))
            {
                ReloadScenes();
            }
        }

        private void ReloadScenes()
        {
            paths.Clear();
            filteredPaths.Clear();
            DirectoryInfo d = new DirectoryInfo(Application.dataPath + "/Scenes/");
            foreach (var fileInfo in d.GetFiles())
            {
                if (Path.GetExtension(fileInfo.FullName) == ".unity")
                {
                    paths.Add(fileInfo);
                    filteredPaths.Add(fileInfo);
                }
            }
        }

        private void DrawSearchText()
        {
            GUILayout.BeginHorizontal();
            GUI.SetNextControlName("searchText");
            var newFilter = EditorGUILayout.TextField(filter);
            GUILayout.EndHorizontal();
            if (newFilter != filter)
            {
                filteredPaths.Clear();
                filter = newFilter;
                if (string.IsNullOrEmpty(filter))
                {
                    filteredPaths.AddRange(paths);
                } else
                {
                    foreach (var fileInfo in paths)
                    {
                        var name = fileInfo.Name.Replace(Path.GetExtension(fileInfo.Name), "").ToLower();
                        if (name.Contains(filter.ToLower()))
                        {
                            filteredPaths.Add(fileInfo);
                        }
                    }
                }
            }
            if (Event.current.isKey)
            {
                switch (Event.current.keyCode)
                {
                    case KeyCode.Return:
                    case KeyCode.KeypadEnter:
                        if (index >= 0 && index < filteredPaths.Count)
                            OpenScene(filteredPaths[index]);
                        break;
                    case KeyCode.DownArrow:
                        index = (index + 1) % filteredPaths.Count;
                        Repaint();
                        break;
                    case KeyCode.UpArrow:
                        index = (index + -1 + filteredPaths.Count) % filteredPaths.Count;
                        Repaint();
                        break;
                }
            }
            if (!focus)
            {
                focus = true;
                EditorGUI.FocusTextInControl("searchText");
            }
        }

        private void OpenScene(FileInfo path)
        {
            AssetDatabase.SaveAssets();
            var p = path.FullName;
            p = p.Replace("\\", "/");
            p = p.Substring(p.IndexOf("Assets"), p.Length - p.IndexOf("Assets"));
            EditorSceneManager.OpenScene(p);
        }

        [MenuItem("HelloMeowLab/快速打开场景 #&O")]
        public static void QuickOpenSceneEntrance()
        {
            GetWindow<QuickOpenScene>();
        }
    }
}
