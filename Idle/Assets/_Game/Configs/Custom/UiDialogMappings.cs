/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2022-10-05 12:52:25
 * @modify date 2022-10-05 12:52:25
 * @desc []
 */

using System;
using System.Collections.Generic;
using HM.GameBase;
using NewLife.Defined.CustomAttributes;
using Sirenix.OdinInspector;
using UnityEngine;

#pragma warning disable 0649
namespace NewLife.Config
{
    [CreateAssetMenu(fileName = "UiDialogMappings", menuName = "HelloMeow/UiDialogMappings")]
    public class UiDialogMappings : ScriptableObject
    {
        [Serializable]
        public class DialogMapping
        {
            [LabelText("类型")]
            [UiDialogType]
            public string Type;

            [LabelText("预制体")]
            public GameObject Prefab;
        }

        // 保存dialogType和prefab的对应关系
        [TableList]
        [SerializeField]
        private List<DialogMapping> mappings = new List<DialogMapping>();

        public ListFastEnumerator<DialogMapping> GetEnumerator()
        {
            return new ListFastEnumerator<DialogMapping>(mappings);
        }
    }
}
#pragma warning restore 0649