/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年2月7日
 * @modify date 2023年2月7日
 * @desc [GameStarter]
 */

#pragma warning disable 0649
using System.Collections.Generic;
using DefaultNamespace.Item;
using QFramework;
using UnityEngine;

namespace DefaultNamespace.Game
{
    public class GameStarter : MonoBehaviour
    {
        #region FIELDS

        [SerializeField]
        private List<ItemDef> itemDefs;

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
            ResKit.Init();
            ItemCatalog.SetItemDefs(itemDefs);
        }

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649