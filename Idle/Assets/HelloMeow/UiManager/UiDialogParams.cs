/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2020-06-17 18:06:34
 * @modify date 2020-06-17 18:06:34
 * @desc [description]
 */

using System;

namespace HM.GameBase
{
    public class UiDialogParams : IHMPooledObject, IDisposable
    {
        private object[] parameters;

        /// <summary>
        /// 查询指定位置的参数
        /// </summary>
        /// <param name="index"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetParameter<T>(int index)
        {
            if (index < 0 || index >= parameters.Length)
            {
                HMLog.LogError($"[UiDialogParams] Out of bounds! index = {index}");
                return default;
            }
            return (T)parameters[index];
        }

        public void OnEnterPool()
        {
            parameters = null;
        }

        public class Factory
        {
            public UiDialogParams Create(params object[] objects)
            {
                var inst = ObjectPool<UiDialogParams>.Claim();
                inst.parameters = objects;
                return inst;
            }
        }

        public void Dispose()
        {
            var inst = this;
            ObjectPool<UiDialogParams>.Release(ref inst);
        }
    }
}