/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2022-08-26 15:05:12
 * @modify date 2022-08-26 15:05:12
 * @desc []
 */

using System;
using UnityEngine;

#pragma warning disable 0649
namespace NewLife.UI.Extensions
{
    [Serializable]
    public class GameObjectActiveSetting
    {
        [SerializeField]
        private GameObject gameObject;

        [SerializeField]
        private bool active;

        public void Apply()
        {
            gameObject.SetActive(active);
        }

        public void Undo()
        {
            gameObject.SetActive(!active);
        }
    }
}
#pragma warning restore 0649