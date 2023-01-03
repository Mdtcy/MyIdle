/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年1月3日
 * @modify date 2023年1月3日
 * @desc [任务组UI]
 */

#pragma warning disable 0649
using UnityEngine;

namespace Game.FloatingText
{
    public interface IFloatingText
    {
        /// <summary>
        /// 生成伤害数字
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="text"></param>
        public void SpawnDamageNum(Vector3 pos, string text);
    }
}
#pragma warning restore 0649