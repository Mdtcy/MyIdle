/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2021-01-02 09:01:10
 * @modify date 2021-01-02 09:01:10
 * @desc [description]
 */

using System;
using Com.TheFallenGames.OSA.CustomAdapters.GridView;
using PathologicalGames;
using UnityEngine;
#pragma warning disable 0649
namespace HM.OsaExtensions
{
    [Serializable]
    public class OsaBaseGridParams : GridParams
    {
        [SerializeField]
        private SpawnPool pool;

        /// <summary>
        /// use spawnPool by default to spawn cell gameObject
        /// </summary>
        public SpawnPool Pool => pool;
    }
}
#pragma warning restore 0649