/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2020-12-29 17:12:03
 * @modify date 2020-12-29 17:12:03
 * @desc [description]
 */

using Com.TheFallenGames.OSA.CustomAdapters.GridView;
using PathologicalGames;
using UnityEngine;

namespace HM.OsaExtensions
{
    public class OsaBaseCellGroupViewsHolder<TCellViewsHolder> : CellGroupViewsHolder<TCellViewsHolder>
        where TCellViewsHolder : OsaBaseCellViewsHolder, new()
    {
        public SpawnPool Pool { get; set; }

        /// <inheritdoc />
        protected override GameObject InstantiateFromPrefab(GameObject cellPrefab, Transform parent, bool worldPositionStays)
        {
            return Pool.Spawn(cellPrefab, parent).gameObject;
        }
    }
}