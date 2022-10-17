/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2022/02/28 18:26
 * @modify date 2022/02/28 18:26
 * @desc [SceneExtension]
 */

#pragma warning disable 0649
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace NewLife.BusinessLogic.Scene
{
    public class SceneExtension
    {
        /// <summary>
        /// 获取Build中的Scene
        /// </summary>
        /// <returns></returns>
        public static List<string> GetScenesInBuild()
        {
            List<string> scenesInBuild = new List<string>();

            for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
            {
                string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
                int    lastSlash = scenePath.LastIndexOf("/", StringComparison.Ordinal);
                scenesInBuild.Add(scenePath.Substring(lastSlash + 1,
                                                      scenePath.LastIndexOf(".", StringComparison.Ordinal) -
                                                      lastSlash -
                                                      1));
            }

            return scenesInBuild;
        }

        /// <summary>
        /// 获取已经加载的场景
        /// </summary>
        /// <returns></returns>
        public static UnityEngine.SceneManagement.Scene[] GetLoadedScenes()
        {
            int                                 sceneCount   = SceneManager.sceneCount;
            UnityEngine.SceneManagement.Scene[] loadedScenes = new UnityEngine.SceneManagement.Scene[sceneCount];

            for (int i = 0; i < sceneCount; i++)
            {
                loadedScenes[i] = SceneManager.GetSceneAt(i);
            }

            return loadedScenes;
        }

        /// <summary>
        /// 是否已经加载某个场景
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool HasLoadScene(string name)
        {
            foreach (var scene in GetLoadedScenes())
            {
                if (scene.name == name)
                {
                    return true;
                }
            }

            return false;
        }
    }
}

#pragma warning restore 0649