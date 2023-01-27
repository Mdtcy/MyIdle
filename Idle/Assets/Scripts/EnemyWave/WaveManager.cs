/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年1月27日
 * @modify date 2023年1月27日
 * @desc [任务组UI]
 */

#pragma warning disable 0649
using QFramework;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Wave
{
    public class WaveManager : MonoBehaviour
    {
        #region FIELDS

        [SerializeField]
        private WaveData waveData;

        [SerializeField]
        private Transform spawnPoint;

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        private void Start()
        {
            Spawn();
        }

        [Button]
        void Spawn()
        {
            for (int i = 0; i < waveData.Waves[0].Count; i++)
            {
                Instantiate(waveData.Waves[0].Prefab, spawnPoint);
            }
        }

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649