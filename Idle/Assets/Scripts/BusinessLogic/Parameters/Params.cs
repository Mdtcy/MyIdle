/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2020-11-30 12:11:26
 * @modify date 2020-11-30 12:11:26
 * @desc [UiManager/PopUi/Notification如果涉及多个参数，使用该类型保存参数]
 */

using System;

namespace HM.GameBase
{
    /// <summary>
    /// 好处是可以避免boxing，如果参数类型相同且频繁调用，会因复用而减少内存分配
    /// 使用须知：调用方负责创建，使用方负责回收
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    public class Params<T1> : IDisposable, IHMPooledObject
    {
        /// <summary>
        /// 第一个参数
        /// </summary>
        public T1 Param1;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="param1"></param>
        public void Initialize(T1 param1)
        {
            Param1 = param1;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            this.ReleaseToPool();
        }

        /// <inheritdoc />
        public virtual void OnEnterPool()
        {
        }
    }

    public class Params<T1, T2> : Params<T1>
    {
        /// <summary>
        /// 第二个参数
        /// </summary>
        public T2 Param2;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="param1"></param>
        /// <param name="param2"></param>
        public void Initialize(T1 param1, T2 param2)
        {
            base.Initialize(param1);
            Param2 = param2;
        }
    }

    public class Params<T1, T2, T3> : Params<T1, T2>
    {
        /// <summary>
        /// 第三个参数
        /// </summary>
        public T3 Param3;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="param1"></param>
        /// <param name="param2"></param>
        /// <param name="param3"></param>
        public void Initialize(T1 param1, T2 param2, T3 param3)
        {
            base.Initialize(param1, param2);
            Param3 = param3;
        }
    }

    /// <summary>
    /// 创建UiDialogParams的工厂
    /// </summary>
    public class ParamsFactory
    {
        /// <summary>
        /// 创建单一参数的UiDialogParams，通常用来减少boxing
        /// </summary>
        /// <param name="param1"></param>
        /// <typeparam name="T1"></typeparam>
        /// <returns></returns>
        public static Params<T1> Create<T1>(T1 param1)
        {
            var inst = ObjectPool<Params<T1>>.Claim();
            inst.Initialize(param1);
            return inst;
        }

        /// <summary>
        /// 创建带两个参数的UiDialogParams
        /// </summary>
        /// <param name="param1"></param>
        /// <param name="param2"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <returns></returns>
        public static Params<T1, T2> Create<T1, T2>(T1 param1, T2 param2)
        {
            var inst = ObjectPool<Params<T1, T2>>.Claim();
            inst.Initialize(param1, param2);
            return inst;
        }

        /// <summary>
        /// 创建带三个参数的UiDialogParams
        /// </summary>
        /// <param name="param1"></param>
        /// <param name="param2"></param>
        /// <param name="param3"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <returns></returns>
        public static Params<T1, T2, T3> Create<T1, T2, T3>(T1 param1, T2 param2, T3 param3)
        {
            var inst = ObjectPool<Params<T1, T2, T3>>.Claim();
            inst.Initialize(param1, param2, param3);
            return inst;
        }
    }
}