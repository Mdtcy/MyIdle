using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Unit
{
    [CreateAssetMenu(fileName = "EntityConfig", menuName = "EntityConfig", order = 0)]
    public class EntityConfig : ScriptableObject
    {
        // [TableList]
        // public List<AttrValue> InitAttrs;
        //
        // [Serializable]
        // public class AttrValue
        // {
        //     public AttributeType AttributeType;
        //
        //     public float Value;
        // }
    }
}