using System.Collections.Generic;
using UnityEngine;

namespace Game.Projectile
{
    public class Projectile : MonoBehaviour
    {
        /// <summary>
        /// 要发射子弹的这个人的gameObject，这里就认角色（拥有ChaState的）
        /// 当然可以是null发射的，但是写效果逻辑的时候得小心caster是null的情况
        /// </summary>
        public GameObject caster;

        /// <summary>
        /// 子弹的初速度，单位：米/秒，跟Tween结合获得Tween得到当前移动速度
        /// </summary>
        public float speed;

        /// <summary>
        /// 子弹的生命周期，单位：秒
        /// </summary>
        public float duration;

        /// <summary>
        /// 子弹已经存在了多久了，单位：秒
        /// 毕竟duration是可以被重设的，比如经过一个aoe，生命周期减半了
        /// </summary>
        public float timeElapsed = 0;

        // /// <summary>
        // /// 子弹的轨迹函数
        // /// <param name="t">子弹飞行了多久的时间点，单位秒。</param>
        // /// <return>返回这一时间点上的速度和偏移，Vector3就是正常速度正常前进</return>
        // /// </summary>
        // public BulletTween tween = null;
        //
        // /// <summary>
        // /// 本帧的移动
        // /// </summary>
        // public Vector3 moveForce = new Vector3();
        //
        // ///<summary>
        // ///子弹命中纪录
        // ///</summary>
        // public List<BulletHitRecord> hitRecords = new List<BulletHitRecord>();
        //
        // /// <summary>
        // /// 子弹创建后多久是没有碰撞的，这样比如子母弹之类的，不会在创建后立即命中目标，但绝大多子弹还应该是0的
        // /// 单位：秒
        // /// </summary>
        // public float canHitAfterCreated = 0;
        //
        // ///<summary>
        // ///子弹正在追踪的目标，不太建议使用这个，最好保持null
        // ///</summary>
        // public GameObject followingTarget = null;
        //
        // /// <summary>
        // /// 子弹传入的参数，逻辑用的到的临时记录
        // /// </summary>
        // public Dictionary<string, object> param = new Dictionary<string, object>();
        //
        // /// <summary>
        // /// 还能命中几次
        // /// </summary>
        // public int hp = 1;
        //
        // private bool smoothMove;
        //
        // private UnitRotate unitRotate;
        // private UnitMove   unitMove;
        // private GameObject viewContainer;
    }
}