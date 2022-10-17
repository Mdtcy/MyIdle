/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2021-01-02 10:01:09
 * @modify date 2021-01-02 10:01:09
 * @desc [简单列表ui参数]
 */

using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace HM.OsaExtensions
{
    [Serializable]
    public class OsaSimpleListParams : OsaBaseListParams
    {
        [FoldoutGroup("extension")]
        [LabelText("列表元素prefab")]
        public GameObject Prefab;
    }
}