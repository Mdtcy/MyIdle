/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2022-06-24 13:06:36
 * @modify date 2022-06-24 13:06:36
 * @desc [description]
 */

using HM.GameBase;
using UnityEditor;
using UnityEngine;

namespace HM.EditorTool
{
    public class RefreshConfigContainer
    {
        [MenuItem("Assets/HelloMeow/Refresh ConfigContainer")]
        public static void Run()
        {
            foreach (string guid in AssetDatabase.FindAssets("t:Prefab"))
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var go = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                if (go.GetComponentInChildren<ConfigContainer>())
                {
                    Refresh(path);

                    return;
                }
            }

            HMLog.LogWarning($"未能找到ConfigContainer");
        }

        private static void Refresh(string path)
        {
            // load the prefab to current scene
            var prefab = (GameObject)PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath<GameObject>(path));
            var script = prefab.GetComponentInChildren<ConfigContainer>();
            HMLog.Assert(script != null);
            // refresh configs
            script.Reload();
            // apply changes to prefab asset.
            PrefabUtility.ApplyPrefabInstance(prefab, InteractionMode.AutomatedAction);
            // delete from scene
            GameObject.DestroyImmediate(prefab);
        }
    }
}