/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2020-07-20 19:07:38
 * @modify date 2020-07-20 19:07:38
 * @desc [可以同时支持若干指令类型的聚合指令]
 */

using System;
using System.IO;
using HM.GameBase;
using NewLife.Defined.CustomAttributes;
using NewLife.Defined.MiniProgram;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NewLife.Defined.Condition
{
    [Serializable]
    public class UnionCommandConfig : IMiniProgramJsonClient
    {
        /// <summary>
        /// 命令类型
        /// </summary>
        public CommandType CommandType;

        #region Custom Script

        [ShowIf("CommandType", CommandType.CustomScript)]
        [UnionCommandCustomScript]
        public string CustomScript;

        #endregion

        #region AcquireItem

        /// <summary>
        /// 目标物品配置
        /// </summary>
        [HideIf("CommandType", CommandType.CustomScript)]
        public BaseConfig Target;

        /// <summary>
        /// 目标物品配置路径(小程序使用)
        /// </summary>
        [HideInInspector]
        public string TargetName;

        /// <summary>
        /// 数量
        /// </summary>
        [HideIf("CommandType", CommandType.SwitchWeather)]
        [HideIf("CommandType", CommandType.CustomScript)]
        public int Num = 1;

        #endregion

        #region UpdateNpcProperty

        // target

        /// <summary>
        /// 联系人属性
        /// </summary>
        [ShowIf("CommandType", CommandType.UpdateNpcProperty)]
        public ContactPropertyType PropertyType;

        // num

        #endregion

        #region SetScheduleState

        // target

        /// <summary>
        /// 行程状态
        /// </summary>
        [ShowIf("CommandType", CommandType.SetScheduleState)]
        public ScheduleState ScheduleState;

        #endregion

        /// <inheritdoc />
        public void OnWillExport()
        {
            TargetName = string.Empty;
            if (Target == null) return;
            #if UNITY_EDITOR
            TargetName = Path.ChangeExtension(UnityEditor.AssetDatabase.GetAssetPath(Target), null);
            #endif
        }
    }
}
