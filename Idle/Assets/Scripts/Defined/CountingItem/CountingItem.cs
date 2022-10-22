/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2020-08-24 16:08:36
 * @modify date 2020-08-24 16:08:36
 * @desc [带数量的物品]
 */

using System;
using HM.CustomAttributes;
using HM.GameBase;
using Sirenix.OdinInspector;

namespace NewLife.Defined
{
    [Serializable]
    public class CountingItem : IHMPooledObject, IDisposable
    {
        /// <summary>
        /// 物品配置
        /// </summary>
        [LabelText("物品配置")]
        [BaseConfig]
        public BaseConfig Item;

        /// <summary>
        /// 数量
        /// </summary>
        [LabelText("数量")]
        public int Num;

        /// <inheritdoc />
        public override string ToString()
        {
            return $"[({(Item != null ? Item.ShortDesc() : "NULL")})x{Num}]";
        }

        /// <inheritdoc />
        public void OnEnterPool()
        {
        }

        /// <summary>
        /// 重置
        /// </summary>
        public void Reset()
        {
            Item = null;
            Num  = 0;
        }

        /// <summary>
        /// 是否包含有效物品
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            return Item != null;
        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <returns></returns>
        public CountingItem Clone()
        {
            var item = ObjectPool<CountingItem>.Claim();
            item.Item = Item;
            item.Num  = Num;
            return item;
        }

        public static CountingItem Claim()
        {
            return ObjectPool<CountingItem>.Claim();
        }

        public static CountingItem Claim(BaseConfig item, int num)
        {
            var inst = ObjectPool<CountingItem>.Claim();
            inst.Item = item;
            inst.Num  = num;

            return inst;
        }

        #region IDisposable

        /// <inheritdoc />
        public void Dispose()
        {
            this.ReleaseToPool();
        }

        #endregion
    }
}