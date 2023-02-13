/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2022年12月21日
 * @modify date 2022年12月21日
 * @desc []
 */

#pragma warning disable 0649
using QFramework;
using UnityEngine;

namespace Game.Projectile
{
    public class ProjectileManager : MonoBehaviour
    {
        #region FIELDS

        // todo 注入接口
        [Zenject.Inject]
        private ProjectilePool projectilePool;

        [SerializeField]
        private Transform root;

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        public void CreateProjectile(Vector3 firePosition)
        {
            // 生成 输入prefab 位置 旋转角度 父节点
            var projectile = projectilePool.Get();
            var projectileTrans = projectile.transform;
            projectileTrans.position = firePosition;
            projectileTrans.rotation = Quaternion.identity;
            projectileTrans.SetParent(root);

            // 传入子弹所需的参数
        }

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        private void FixedUpdate()
        {

        }

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649