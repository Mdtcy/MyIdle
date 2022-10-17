using System;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using System.Linq;
using System.Reflection;
#endif

namespace HM.ConfigTool
{
    public class HMFieldInfo
    {
#if UNITY_EDITOR
        public FieldInfo Info { get; set; }
#endif
        // public ConfigFieldProperty Property { get; set; }
        // public int order { get; set; }
    }

    public static class TypeHelper
    {
        public static List<HMFieldInfo> ParseFields(Type targetType)
        {
            var ret = new List<HMFieldInfo>();

#if UNITY_EDITOR
            foreach (var fi in targetType.GetFields().ToList())
            {
                if (fi.IsStatic)
                {
                    Debug.LogFormat("Static field located! {0}", fi.Name);
                    continue;
                }

                HMFieldInfo f = new HMFieldInfo();
                f.Info = fi;
                ret.Add(f);
            }
#endif

            return ret;
        }
    }
}