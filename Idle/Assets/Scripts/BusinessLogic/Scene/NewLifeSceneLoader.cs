/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2022/02/28 16:42
 * @modify date 2022/02/28 16:42
 * @desc [负责场景加载]
 */

#pragma warning disable 0649
using System.Collections.Generic;
using HM;
using HM.Extensions;
using MEC;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NewLife.BusinessLogic.Scene
{
    /// <summary>
    /// 负责场景加载
    /// </summary>
    public class NewLifeSceneLoader
    {
        #region FIELDS

        // 是否加载中
        private bool         isLoading;

        // local
        private string       sceneToLoad;
        private List<string> scenesToUnload;
        private string       sceneToTransition;
        private float        loadProgress       = 0f;
        private float        interpolatedLoadProgress;

        // 进度条速度
        private float progressBarSpeed = 1f;

        #endregion

        #region PROPERTIES

        /// <summary>
        /// 正在加载中
        /// </summary>
        public bool IsLoading => isLoading;

        /// <summary>
        /// 经过插值得出的进度
        /// </summary>
        public float InterpolatedLoadProgress => interpolatedLoadProgress;

        #endregion

        #region PUBLIC METHODS


        /// <summary>
        /// 加载场景
        /// </summary>
        /// <param name="sceneToLoad"></param>
        /// <param name="scenesToUnload"></param>
        /// <param name="loadingSceneName"></param>
        /// <param name="progressBarSpeed"></param>
        public void LoadScene(string       sceneToLoad,
                              List<string> scenesToUnload,
                              string       loadingSceneName = "",
                              float        progressBarSpeed = 1f)
        {
            if (isLoading)
            {
                HMLog.LogWarning($"[SceneLoader]当前正在加载场景，不可同时加载场景:{sceneToLoad}");
                return;
            }

            isLoading = true;
            var scenesInBuild = SceneExtension.GetScenesInBuild();
            HMLog.Assert(loadingSceneName == string.Empty || scenesInBuild.Contains(loadingSceneName), $"[SceneLoader]Build设置中未包含场景:{loadingSceneName}");
            HMLog.Assert(scenesInBuild.Contains(sceneToLoad), $"[SceneLoader]Build设置中未包含场景:{sceneToLoad}");
            foreach (var scene in scenesToUnload)
            {
                HMLog.Assert(scenesInBuild.Contains(scene), $"[SceneLoader]Build设置中未包含场景:{scene}");
            }

            sceneToTransition     = loadingSceneName;
            this.sceneToLoad      = sceneToLoad;
            this.scenesToUnload   = scenesToUnload;
            this.progressBarSpeed = progressBarSpeed;

            Timing.RunCoroutine(ILoadSequence());
        }

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        // 加载场景队列
        private IEnumerator<float> ILoadSequence()
        {
            Init();

            if (sceneToTransition != string.Empty)
            {
                // 加载LoadingScreen
                yield return
                    Timing.WaitUntilDone(SceneManager.LoadSceneAsync(sceneToTransition, LoadSceneMode.Additive));
            }

            // todo 无法使用Timing.WaitUntilDone进行分离 原因暂时未知
            // 卸载要卸载的场景
            foreach (var scene in scenesToUnload)
            {
                var unloadOriginAsyncOperation = SceneManager.UnloadSceneAsync(scene);

                while (!unloadOriginAsyncOperation.isDone)
                {
                    yield return Timing.WaitForOneFrame;
                }
            }

            // 如果还没加载目标场景，加载出来
            if (!SceneExtension.HasLoadScene(sceneToLoad))
            {
                var loadDestinationAsyncOperation =
                    SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);

                // 允许直接加载完
                loadDestinationAsyncOperation.allowSceneActivation = true;

                while (!loadDestinationAsyncOperation.isDone)
                {
                    loadProgress = loadDestinationAsyncOperation.progress;
                    UpdateInterpolatedLoadProgress();

                    yield return Timing.WaitForOneFrame;
                }
            }

            if (sceneToTransition != string.Empty)
            {
                // 卸载LoadingScreen
                yield return Timing.WaitUntilDone(IUnloadTransitionScene(), Segment.RealtimeUpdate);
            }

            OnComplete();
        }

        // 初始化
        private void Init()
        {
            Application.backgroundLoadingPriority = ThreadPriority.High;
            loadProgress                          = 0;
            interpolatedLoadProgress              = 0;
        }

        // 结束时
        private void OnComplete()
        {
            isLoading = false;
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneToLoad));
        }

        // 卸载加载场景
        private IEnumerator<float> IUnloadTransitionScene()
        {
            yield return Timing.WaitForOneFrame;
            var  unloadLoadingAsyncOperation = SceneManager.UnloadSceneAsync(sceneToTransition);

            while (!unloadLoadingAsyncOperation.isDone)
            {
                yield return Timing.WaitForOneFrame;
            }
        }

        // 更新插值Progress
        private void UpdateInterpolatedLoadProgress()
        {
            interpolatedLoadProgress = MathExtensions.Approach(interpolatedLoadProgress,
                                                               loadProgress,
                                                               Time.fixedDeltaTime * progressBarSpeed);
        }

        #endregion

        #region STATIC METHODS
        #endregion
    }
}

#pragma warning restore 0649