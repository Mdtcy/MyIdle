using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    public class TowerCollection : MonoBehaviour
    {
        private List<Tower> towers = new List<Tower>();
        
        public void Add (Tower tower) {
            towers.Add(tower);
        }
        
        public void GameUpdate()
        {
            for (int i = 0; i < towers.Count; i++)
            {
                if (!towers[i].GameUpdate()) 
                {
                    int lastIndex = towers.Count - 1;
                    towers[i] = towers[lastIndex];
                    towers.RemoveAt(lastIndex);
                    i -= 1;
                }
            }
        }
    }
}