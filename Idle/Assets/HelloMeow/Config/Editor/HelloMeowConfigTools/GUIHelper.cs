using System;
using Sirenix.Utilities;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

namespace HM.ConfigTool
{
    public static class GUIHelper
    {
        /// <summary>
        /// 大图显示图片预览
        /// 竖排，第一行显示图片预览，宽高最多120x120
        /// 第二行显示ObjectField
        /// </summary>
        /// <param name="sp"></param>
        /// <param name="OnValueChanged"></param>
        /// <param name="width"></param>
        /// <param name="enabled"></param>
        public static void DisplaySpriteWithPreview(Sprite         sp,
                                               Action<object> OnValueChanged,
                                               float          width   = 120,
                                               bool           enabled = true)
        {
            Sprite sprite = sp as Sprite;
            if (enabled)
            {
                if (sprite != null)
                {
                    GUILayout.BeginVertical(GUILayout.Width(width));
                    // a little preview
                    GUILayout.Label(AssetPreview.GetAssetPreview((UnityEngine.Object) sp), GUILayout.Width(width), GUILayout.MaxHeight(width));
                    sp = (Sprite)EditorGUILayout.ObjectField(sprite, typeof(Sprite), false, GUILayout.Width(width));
                    GUILayout.EndVertical();
                }
                else
                {
                    sp = (Sprite)EditorGUILayout.ObjectField(sprite, typeof(Sprite), false, GUILayout.Width(width));
                }

                OnValueChanged?.Invoke(sp);
            }
            else
            {
                EditorGUILayout.ObjectField(sp as UnityEngine.Object, typeof(UnityEngine.Object), false, new GUILayoutOption[] { GUILayout.Width(width) });
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="width"></param>
        /// <param name="enabled">如果为false，则显示只读信息</param>
        /// <param name="value"></param>
        /// <param name="setValue"></param>
        public static void DisplayValue(System.Type valueType, object value, Action<object> OnValueChanged, float width = 120, bool enabled = true)
        {
            bool allowNull = true;

            if (value is Enum valueEnum)
            {
                if (valueType.GetAttribute<FlagsAttribute>() != null)
                {
                    OnValueChanged(EditorGUILayout.EnumFlagsField(valueEnum, GUILayout.Width(width)));
                }
                else
                {
                    if (enabled)
                    {
                        value = EditorGUILayout.EnumPopup(valueEnum, GUILayout.Width(width));
                        OnValueChanged?.Invoke(value);
                    }
                    else
                    {
                        EditorGUILayout.LabelField(valueEnum.ToString(), GUILayout.Width(width));
                    }
                }
            }
            else if (value is Bounds)
            {
                if (enabled)
                {
                    value = EditorGUILayout.BoundsField((Bounds)value, new GUILayoutOption[] { GUILayout.Width(width) });
                    OnValueChanged?.Invoke(value);
                }
                else
                {
                    EditorGUILayout.BoundsField((Bounds)value, new GUILayoutOption[] { GUILayout.Width(width) });
                }
            }
            else if (value is Color)
            {
                if (enabled)
                {
                    value = EditorGUILayout.ColorField((Color)value, new GUILayoutOption[] { GUILayout.Width(width) });
                    OnValueChanged?.Invoke(value);
                }
                else
                {
                    EditorGUILayout.ColorField((Color)value, new GUILayoutOption[] { GUILayout.Width(width) });
                }
            }
            else if (value is AnimationCurve)
            {
                if (enabled)
                {
                    value = EditorGUILayout.CurveField((AnimationCurve)value, new GUILayoutOption[] { GUILayout.Width(width) });
                    OnValueChanged?.Invoke(value);
                }
                else
                {
                    EditorGUILayout.CurveField((AnimationCurve)value, new GUILayoutOption[] { GUILayout.Width(width) });
                }
            }
            else if (value is string || valueType.IsSubclassOf(typeof(string)) || valueType == typeof(string))
            {
                if (enabled)
                {
                    var c = GUI.backgroundColor;
                    if (!allowNull && string.IsNullOrEmpty(value as string))
                    {
                        GUI.backgroundColor = Color.red;
                    }
                    value = EditorGUILayout.TextArea(value as string, new GUILayoutOption[]
                    {
                        GUILayout.Width(width),
                            GUILayout.MaxHeight(50), GUILayout.ExpandHeight(false)
                    });
                    OnValueChanged?.Invoke(value);
                    GUI.backgroundColor = c;
                }
                else
                {
                    EditorGUILayout.LabelField(value as string, new GUILayoutOption[] { GUILayout.Width(width) });
                }
            }
            else if (value is float)
            {
                if (enabled)
                {
                    value = EditorGUILayout.FloatField((float)value, new GUILayoutOption[] { GUILayout.Width(width) });
                    OnValueChanged?.Invoke(value);
                }
                else
                {
                    EditorGUILayout.LabelField(value as string, new GUILayoutOption[] { GUILayout.Width(width) });
                }
            }
            else if (value is double doubleValue)
            {
                if (enabled)
                {
                    value = EditorGUILayout.DoubleField(doubleValue, new GUILayoutOption[] { GUILayout.Width(width) });
                    OnValueChanged?.Invoke(value);
                }
                else
                {
                    EditorGUILayout.LabelField(value.ToString(), new GUILayoutOption[] { GUILayout.Width(width) });
                }
            }
            else if (value is int || valueType.IsInstanceOfType(typeof(int)))
            {
                if (enabled)
                {
                    value = EditorGUILayout.IntField((int)value, new GUILayoutOption[] { GUILayout.Width(width) });
                    OnValueChanged?.Invoke(value);
                }
                else
                {
                    EditorGUILayout.LabelField(((int)value).ToString(), new GUILayoutOption[] { GUILayout.Width(width) });
                }
            }
            else if (value is long lvalue)
            {
                if (enabled)
                {
                    lvalue = EditorGUILayout.LongField(lvalue, new GUILayoutOption[] { GUILayout.Width(width) });
                    OnValueChanged(lvalue);
                }
                else
                {
                    EditorGUILayout.LabelField($"{lvalue}", new GUILayoutOption[] { GUILayout.Width(width) });
                }
            }
            else if (value is bool)
            {
                if (enabled)
                {
                    value = EditorGUILayout.Toggle((bool)value, new GUILayoutOption[] { GUILayout.Width(width) });
                    OnValueChanged?.Invoke(value);
                }
                else
                {
                    EditorGUILayout.LabelField(((bool)value).ToString(),
                        new GUILayoutOption[] { GUILayout.Width(width) });
                }
            }
            else if (value is Vector2)
            {
                if (enabled)
                {
                    value = EditorGUILayout.Vector2Field("", (Vector2)value,
                        new GUILayoutOption[] { GUILayout.Width(width) });
                    OnValueChanged?.Invoke(value);
                }
                else
                {
                    EditorGUILayout.Vector2Field("", (Vector2)value, new GUILayoutOption[] { GUILayout.Width(width) });
                }
            }
            else if (value is Vector3)
            {
                if (enabled)
                {
                    value = EditorGUILayout.Vector3Field("", (Vector3)value,
                        new GUILayoutOption[] { GUILayout.Width(width) });
                    OnValueChanged?.Invoke(value);
                }
                else
                {
                    EditorGUILayout.Vector3Field("", (Vector3)value, new GUILayoutOption[] { GUILayout.Width(width) });
                }
            }
            else if (value is Vector4)
            {
                if (enabled)
                {
                    value = EditorGUILayout.Vector3Field("", (Vector4)value, new GUILayoutOption[] { GUILayout.Width(width) });
                    OnValueChanged?.Invoke(value);
                }
                else
                {
                    EditorGUILayout.Vector3Field("", (Vector4)value, new GUILayoutOption[] { GUILayout.Width(width) });
                }
            }
            else if (value is Sprite || valueType.IsSubclassOf(typeof(Sprite)) || valueType == typeof(Sprite))
            {
                Sprite sprite = value as Sprite;
                if (enabled)
                {
                    if (sprite != null)
                    {
                        GUILayout.BeginHorizontal(GUILayout.Width(width));
                        // a little preview
                        GUILayout.Label(AssetPreview.GetAssetPreview((UnityEngine.Object) value), GUILayout.Width(20), GUILayout.Height(20));
                        value = EditorGUILayout.ObjectField(sprite, typeof(Sprite), false, GUILayout.Width(width - 24));
                        GUILayout.EndHorizontal();
                    }
                    else
                    {
                        value = EditorGUILayout.ObjectField(sprite, typeof(Sprite), false, GUILayout.Width(width));
                    }

                    OnValueChanged?.Invoke(value);
                }
                else
                {
                    EditorGUILayout.ObjectField(value as UnityEngine.Object, typeof(UnityEngine.Object), false, new GUILayoutOption[] { GUILayout.Width(width) });
                }
            }
            else if (value is UnityEngine.Object || valueType.IsSubclassOf(typeof(UnityEngine.Object)) || valueType == typeof(UnityEngine.Object))
            {
                var obj = value as UnityEngine.Object;
                if (enabled)
                {
                    bool allowSceneObjects = obj != null ? !EditorUtility.IsPersistent(obj) : false;
                    value = EditorGUILayout.ObjectField(obj, valueType, allowSceneObjects, new GUILayoutOption[] { GUILayout.Width(width) });
                    OnValueChanged?.Invoke(value);
                }
                else
                {
                    EditorGUILayout.ObjectField(obj, valueType, false, new GUILayoutOption[] { GUILayout.Width(width) });
                }
            }
            else if (value is AssetReference)
            {
                var assetRef = value as AssetReference;
                Object obj = null;
                if (!assetRef.AssetGUID.IsNullOrWhitespace())
                {
                    var atlasPath = AssetDatabase.GUIDToAssetPath(assetRef.AssetGUID);

                    if (assetRef.SubObjectName.IsNullOrWhitespace())
                    {
                        obj = AssetDatabase.LoadAssetAtPath<Object>(atlasPath);
                    }
                    else
                    {
                        Object[] objs = AssetDatabase.LoadAllAssetRepresentationsAtPath(atlasPath);
                        // locate the sub object
                        for (int j = 0, jMax = objs.Length; j < jMax; ++j)
                        {
                            if (objs[j].name.Equals(assetRef.SubObjectName))
                            {
                                obj = objs[j];
                                break;
                            }
                        }
                    }
                }

                var objType = obj == null ? typeof(Object) : obj.GetType();
                var newObj = EditorGUILayout.ObjectField(obj, objType, false, GUILayout.Width(width));
                if (newObj != obj)
                    OnValueChanged(newObj);
            }
            else
            {
                EditorGUILayout.LabelField(valueType.ToString(), new GUILayoutOption[] { GUILayout.Width(width) });
                HMLog.LogWarning($"不支持显示的元素: {value}");
                // if (enabled)
                // {
                //     bool allowSceneObjects = value != null ? !EditorUtility.IsPersistent(value as UnityEngine.Object) : false;
                //     value = EditorGUILayout.ObjectField(value as UnityEngine.Object, typeof(UnityEngine.Object), allowSceneObjects, new GUILayoutOption[] { GUILayout.Width(width) });
                //     OnValueChanged?.Invoke(value);
                // }
                // else
                // {
                //     EditorGUILayout.ObjectField(value as UnityEngine.Object, typeof(UnityEngine.Object), false, new GUILayoutOption[] { GUILayout.Width(width) });
                // }
            }
        }
    }
}