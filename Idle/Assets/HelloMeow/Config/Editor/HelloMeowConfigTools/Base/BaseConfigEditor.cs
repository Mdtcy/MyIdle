using UnityEditor;
using HM.GameBase;

namespace HM.ConfigTool
{
    [CustomEditor(typeof(BaseConfig))]
    public class BaseConfigEditor : Editor
    {
        private ConfigSettings _setting;

        protected string ConfigName { get; set; }
        protected int MajorType { get; set; }
        protected int MinorType { get; set; }
    }
}