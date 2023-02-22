/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年2月15日
 * @modify date 2023年2月15日
 * @desc []
 */

#pragma warning disable 0649
using Damage;
using DefaultNamespace.Item;
using DefaultNamespace.System;
using IdleGame.Buff;
using UnityEngine;

namespace DefaultNamespace.Game
{
    public class GameManager : MonoBehaviour
    {
        #region FIELDS

        public GlobalEventManager GlobalEventManager;
        public DamageManager      DamageManager;
        public Inventory          Inventory;

        public ItemContent ItemContent;
        public BuffContent BuffContent;

        public static GameManager   Instance;

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        private void OnEnable()
        {
            if (Instance != null)
            {
                Debug.LogError("Only one GameManager can exist at a time.");

                return;
            }

            Instance = this;
        }

        private void OnDisable()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649