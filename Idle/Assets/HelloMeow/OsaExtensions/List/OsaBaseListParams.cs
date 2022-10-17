/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2021-01-02 10:01:09
 * @modify date 2021-01-02 10:01:09
 * @desc [description]
 */

using System;
using Com.TheFallenGames.OSA.Core;
using PathologicalGames;
using Sirenix.OdinInspector;

namespace HM.OsaExtensions
{
    [Serializable]
    public class OsaBaseListParams : BaseParams
    {
        [FoldoutGroup("extension")]
        [LabelText("列表元素Pool")]
        public SpawnPool Pool;
    }
}