/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2022年12月24日
 * @modify date 2022年12月24日
 * @desc [任务组UI]
 */

#pragma warning disable 0649
using UnityEngine;

namespace Test
{
    public class OnHpUpdatedSignal
    {
        public Entity Entity;

        public OnHpUpdatedSignal(Entity entity)
        {
            Entity = entity;
        }
    }
}
#pragma warning restore 0649