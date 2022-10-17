/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2021-08-02 17:08:09
 * @modify date 2021-08-02 17:08:09
 * @desc [description]
 */

using System;
using System.Collections.Generic;

namespace HM.GameBase
{
    public interface IItemsGetter<T> : IHMPooledObject where T : ItemBase
    {
        /// <summary>
        /// 获取所有类型为T的item
        /// </summary>
        /// <returns></returns>
        List<T> Get();

        /// <summary>
        /// 包含的元素数量
        /// </summary>
        /// <returns></returns>
        int Count();

        /// <summary>
        /// 依据指定条件过滤
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="result"></param>
        void Filter(Func<T, bool> predicate, ref List<T> result);

        /// <summary>
        /// 是否包含指定itemId的物品
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        bool Contains(int itemId);

        /// <summary>
        /// 监听集合变化消息
        /// </summary>
        /// <param name="onValueChanged"></param>
        void Subscribe(Action onValueChanged);

        /// <summary>
        /// 取消监听集合变化消息
        /// </summary>
        /// <param name="onValueChanged"></param>
        void Unsubscribe(Action onValueChanged);
    }
}