/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年1月27日
 * @modify date 2023年1月27日
 * @desc [任务组UI]
 */

#pragma warning disable 0649
using System.Collections.Generic;
using DefaultNamespace.Events;
using HM.Extensions;
using IdleGame;
using QFramework;
using QFramework.Example;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Wave
{
    public class WaveManager : MonoBehaviour
    {
        #region FIELDS

        [InlineEditor(InlineEditorModes.FullEditor)]
        [SerializeField]
        private WaveData waveData;

        [SerializeField]
        private List<Transform> spawnPoints;

        private List<Entity> enemies = new List<Entity>();

        // local
        private int currentWave = -1;

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
            NextWave();
            TypeEventSystem.Global.Register<WaveFinishedEvent>(e => { NextWave();})
                           .UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        private void Update()
        {
            for (int index = enemies.Count - 1; index >= 0; index--)
            {
                var enemy = enemies[index];

                if (enemy == null || enemy.dead)
                {
                    enemies.Remove(enemy);
                }
            }

            if (enemies.Count == 0 && currentWave < waveData.Waves.Count)
            {
                UIKit.OpenPanel<UISelectSkill>();
            }
        }

        [SerializeField]
        private float offset = 0;



        private void NextWave()
        {
            currentWave++;
            if(currentWave >= waveData.Waves.Count)
            {
                Debug.Log("游戏结束");
                return;
            }

            for (int i = 0; i < waveData.Waves[currentWave].Count; i++)
            {
                Vector3 s = new Vector3(Random.Range(-offset, offset), Random.Range(-offset, offset), 0);
                var enemy = Instantiate(waveData.Waves[currentWave].Prefab, spawnPoints.Random().position + s, Quaternion.identity);
                enemies.Add(enemy.GetComponent<Entity>());
            }
        }

        #endregion

        #region STATIC METHODS

        #endregion
    }

}
#pragma warning restore 0649