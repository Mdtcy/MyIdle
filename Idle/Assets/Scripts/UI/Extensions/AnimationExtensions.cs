/**
 * @author [Sligh]
 * @email [zsaveyou@163.com]
 * @create date 2020-12-07 13:22:02
 * @desc [扩展Animation的方法]
 */

using System;
using System.Collections.Generic;
using MEC;

#pragma warning disable 0649

namespace NewLife.UI.Extensions
{
    /// <summary>
    /// 扩展Animation的方法
    /// </summary>
    public static class AnimationExtensions
    {

        /// <summary>
        /// 播放指定clip，完成时调用回调
        /// </summary>
        /// <param name="anim"></param>
        /// <param name="clipName"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public static CoroutineHandle Play(this UnityEngine.Animation anim, string clipName, Action callback)
        {
            return Timing.RunCoroutine(anim.PlayAndWaitForAnim(clipName, callback));
        }
        
        /// <summary>
        /// 用协程方式播放指定clip
        /// </summary>
        /// <param name="anim"></param>
        /// <param name="clipName"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public static IEnumerator<float> PlayAndWaitForAnim(this UnityEngine.Animation anim, string clipName, Action callback)
        {
            anim.Play(clipName);

            //Wait until Animation is done Playing
            while (anim.IsPlaying(clipName))
            {
                yield return 0;
            }
            
            callback?.Invoke();
        }
    }
}
#pragma warning restore 0649