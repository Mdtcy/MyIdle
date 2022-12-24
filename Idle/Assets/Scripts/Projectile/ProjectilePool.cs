/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2022年12月21日
 * @modify date 2022年12月21日
 * @desc [任务组UI]
 */

#pragma warning disable 0649
using UnityEngine;
using UnityEngine.Pool;

namespace Game.Projectile
{
    public class ProjectilePool : MonoBehaviour
    {
        #region FIELDS

        // Collection checks will throw errors if we try to release an item that is already in the pool.
        public bool collectionChecks = true;
        public int  maxPoolSize      = 10;

        [SerializeField]
        private Transform pfbProjectile1;

        IObjectPool<Projectile> pool;

        public IObjectPool<Projectile> Pool =>
            pool ??= new ObjectPool<Projectile>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool,
                                                OnDestroyPoolObject, collectionChecks, 100, maxPoolSize);

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        public Projectile Get()
        {
            return Pool.Get();
        }

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        private void OnDestroyPoolObject(Projectile projectile)
        {
            Destroy(projectile.gameObject);
        }

        private void OnReturnedToPool(Projectile projectile)
        {
            // todo 进行回收
            projectile.gameObject.SetActive(false);
        }

        private void OnTakeFromPool(Projectile projectile)
        {
            // todo 进行一些初始化
            projectile.gameObject.SetActive(true);
        }

        private Projectile CreatePooledItem()
        {
            var go         = Instantiate(pfbProjectile1);
            var projectile = go.GetComponent<Projectile>();

            return projectile;
        }

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649