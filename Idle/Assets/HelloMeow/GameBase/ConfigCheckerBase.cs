/**
 * @author [jie.wen]
 * @email [example@mail.com]
 * @create date 2018-12-14 21:59:31
 * @modify date 2018-12-14 21:59:31
 * @desc [用于判断和确定Item类型的静态类]
 */
using System;

namespace HM.GameBase
{
    /// <summary>
    /// 用于判断和确定Item类型的静态类
    /// </summary>
    public static class ConfigCheckerBase
    {
        ////////////////////////////////////////////////////////////////////////////
        // basic
        ////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 拿到ItemId的大类型
        /// </summary>
        /// <param name="itemid">itemid</param>
        /// <returns></returns>
        public static int MajorType(int itemid)
        {
            return Convert.ToInt32(Math.Floor(itemid / 1000000.0f));
        }

        /// <summary>
        /// 拿到ItemId的小类型
        /// </summary>
        /// <param name="itemid">itemid</param>
        /// <returns></returns>
        public static int MinorType(int itemid)
        {
            return Convert.ToInt32(Math.Floor((itemid % 1000000) / 1000.0f));
        }

        /// <summary>
        /// 拿到ItemId的大类型+小类型组成的6位数字
        /// </summary>
        /// <param name="itemid">itemid</param>
        /// <returns></returns>
        public static int TypeId(int itemid)
        {
            return Convert.ToInt32(Math.Floor(itemid / 1000.0f));
        }

        /// <summary>
        /// 拿到ItemId的最后三位（编号）
        /// </summary>
        /// <param name="itemid">itemid</param>
        /// <returns></returns>
        public static int TypeIndex(int itemid)
        {
            return Convert.ToInt32(Math.Floor(itemid % 1000.0f));
        }

        /// <summary>
        /// 判断itemid的大类型是否等于给定类型
        /// </summary>
        /// <param name="itemid">itemid</param>
        /// <param name="type">大类型的数字表示（三位）</param>
        /// <returns></returns>
        public static bool IsMajorType(int itemid, int type)
        {
            return MajorType(itemid) == type;
        }

        /// <summary>
        /// 判断itemid的小类型是否等于给定类型
        /// </summary>
        /// <param name="itemid">itemid</param>
        /// <param name="type">小类型的数字表示（三位）</param>
        /// <returns></returns>
        public static bool IsMinorType(int itemid, int type)
        {
            return MinorType(itemid) == type;
        }


    }
}