using UnityEngine;
using HM.GameBase;

namespace NewLife.Config
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "Profession", menuName = "Config/Profession")]
    public class ProfessionConfig : BaseConfig
    {
        // Id和Name在BaseConfig已有定义，不需要重复定义
        // 其他类型请自行定义↓↓↓

        public int SalaryPerDay(int level)
        {
            return 1;
        }

        public int ExpToNextLevel(int level)
        {
            return 10;
        }
    }
}