/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2020-07-20 15:07:13
 * @modify date 2020-07-20 15:07:13
 * @desc [所有可能条件放在一起]
 */

using System;
using HM.GameBase;
using Sirenix.OdinInspector;

namespace NewLife.Defined.Condition
{
    [Serializable]
    public class UnionConditionConfig
    {
        /// <summary>
        /// 条件类型
        /// </summary>
        public ConditionType ConditionType;

        /// <summary>
        /// 目标配置
        /// </summary>
        public BaseConfig Target;

        public override string ToString()
        {
            switch (ConditionType)
            {
                default:
                    return $"[UnionConditionConfig type = {ConditionType}]";
            }
        }
    }
}
