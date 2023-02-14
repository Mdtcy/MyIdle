/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2022年12月21日
 * @modify date 2022年12月21日
 * @desc [任务组UI]
 */

#pragma warning disable 0649
using IdleGame;
using UnityEngine;
using UnityEngine.Pool;

namespace Game.Projectile
{
    public class TrackBulletPool : MonoBehaviour
    {
        #region FIELDS

        // Collection checks will throw errors if we try to release an item that is already in the pool.
        public bool collectionChecks = true;
        public int  maxPoolSize      = 10;

        [SerializeField]
        private Transform pfbBullet;

        IObjectPool<TrackBullet> pool;

        public IObjectPool<TrackBullet> Pool =>
            pool ??= new ObjectPool<TrackBullet>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool,
                                                 OnDestroyPoolObject, collectionChecks, 100, maxPoolSize);

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        public TrackBullet Get()
        {
            return Pool.Get();
        }

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        private void OnDestroyPoolObject(TrackBullet bullet)
        {
            Destroy(bullet.gameObject);
        }

        private void OnReturnedToPool(TrackBullet bullet)
        {
            // todo 进行回收
            bullet.gameObject.SetActive(false);
        }

        private void OnTakeFromPool(TrackBullet bullet)
        {
            // todo 进行一些初始化
            bullet.gameObject.SetActive(true);
        }

        private TrackBullet CreatePooledItem()
        {
            var go         = Instantiate(pfbBullet);
            var bullet = go.GetComponent<TrackBullet>();

            return bullet;
        }

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649