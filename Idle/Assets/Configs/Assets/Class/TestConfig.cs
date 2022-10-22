using HelloMeow.Config.Editor.OdinConfigEditor;
using UnityEngine;
using HM.GameBase;

namespace NewLife.Config
{
    [System.Serializable]
    [ManageableData]
    [CreateAssetMenu(fileName = "Test", menuName = "Config/Test")]
    public class TestConfig : BaseConfig
    {
        // Id和Name在BaseConfig已有定义，不需要重复定义
        // 其他类型请自行定义↓↓↓
    }
}