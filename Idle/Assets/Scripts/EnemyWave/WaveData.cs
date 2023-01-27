using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Wave
{
    [CreateAssetMenu(fileName = "WaveData", menuName = "WaveData", order = 0)]
    public class WaveData : ScriptableObject
    {
        public List<Wave> Waves;

        [Serializable]
        public class Wave
        {
            public int Count;

            public GameObject Prefab;
        }
    }
}