/*****************************************************************************/
/****************** Auto Generate Script, Do Not Modify! *********************/
/*****************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using HM.CustomAttributes;
using Sirenix.OdinInspector;
using HM.Extensions;
using HM.GameBase;
using Sirenix.Utilities;
using UnityEditor;
using UnityEngine;
using Object = System.Object;

namespace HM.ConfigTool
{
    /// <summary>
    /// Demo code
    /// var window = (ConfigCollectionEditor)EditorWindow.GetWindow(typeof(ConfigCollectionEditor), false, "title");
    /// window.TargetTypeConfig = src;
    /// window.Show();
    /// </summary>
    public class ConfigCollectionEditor : EditorWindow
    {
        protected const float FieldWidth = 120;
        /// <summary>
        /// 存放拷贝操作的被拷贝对象
        /// </summary>
        private UnityEngine.Object _copy;

        /// <summary>
        /// 全局配置
        /// </summary>
        private ConfigSettings _setting;

        /// <summary>
        /// 全局配置所在路径
        /// </summary>
        private string _settingPath;

        private TypeConfig _typeConfig;

        // 字段值缺失时的提示文字样式（红色文字）
        protected GUIStyle styleMissing;

        public TypeConfig TargetTypeConfig
        {
            private get => _typeConfig;
            set
            {
                _typeConfig = value;

                if (_setting == null)AutoFindSettings();
                var nam = GetConfigTypeName(_typeConfig.ConfigName);

                // determine XyzConfig's type
                _targetType = Type.GetType(GetConfigTypeName(_typeConfig.ConfigName));
                Debug.Assert(_targetType != null, $"Failed to find type: {_targetType}");
                _targetTypeName = _typeConfig.ConfigName + "Config";

                // fetch fields information
                _arrFieldInfo = TypeHelper.ParseFields(_targetType);
                ReloadAllConfigs();

                titleContent = new GUIContent(_typeConfig.ConfigName);
            }
        }

        /// <summary>
        /// 所有配置
        /// </summary>
        /// <returns></returns>
        protected List<UnityEngine.Object> _arrConfig = new List<UnityEngine.Object>();

        /// <summary>
        /// 当前正在显示的集合，比如搜索出来的结果
        /// </summary>
        /// <returns></returns>
        private List<UnityEngine.Object> _curDisplayedConfig = new List<UnityEngine.Object>();

        /// <summary>
        /// ScrollView的当前位置
        /// </summary>
        private Vector2 _svPosition;

        /// <summary>
        /// 目标类的field信息
        /// </summary>
        /// <returns></returns>
        private List<HMFieldInfo> _arrFieldInfo = new List<HMFieldInfo>();

        /// <summary>
        /// 标记某列此时是按照升序还是降序排列
        /// </summary>
        /// <returns></returns>
        // private Dictionary<string, bool> _fieldOrderByAcscending = new Dictionary<string, bool>();

        /// <summary>
        /// 用户在搜索框中输入的文本内容
        /// </summary>
        private string _searchText;

        /// <summary>
        /// 用户选择的待搜索的范围（在哪个属性里搜索）
        /// </summary>
        private int _searchIndex = 0;

        /// <summary>
        /// 每页显示多少行数据，默认10行
        /// </summary>
        private int _itemsPerPage = 10;

        /// <summary>
        /// 当前是第几页
        /// </summary>
        private int _curPage = 0;

        /// <summary>
        /// 是否需要reload待显示的数据
        /// </summary>
        protected bool _forceToRepaint = true;

        /// <summary>
        /// 存放所有需要显示的数据的总数
        /// </summary>
        private int _total = 0;

        private Type _targetType;

        private string _targetTypeName;

        private bool _supportPaging = true;

        /// <summary>
        /// 批量添加新纪录的数量
        /// </summary>
        private int _rowsToAdd = 0;

        private bool CheckBeforeRender()
        {
            if (null == _setting)
            {
                AutoFindSettings();
            }

            if (null == _setting)
            {
                Popup("未找到ConfigSetting配置文件，请创建");
                return false;
            }

            if (_targetType == null)
            {
                // 修改了代码后回到Editor，值会被清掉
                Popup("请关闭后重新打开");
                return false;
            }

            return true;
        }

        private void OnEnable()
        {
            styleMissing = new GUIStyle()
            {
                normal = new GUIStyleState()
                {
                    textColor = Color.red
                }
            };
        }

        protected virtual void FilterCustomContents(ref List<UnityEngine.Object> filteredConfigs)
        {

        }

        private void PrepareFieldInfo()
        {
            // fetch fields information
            _arrFieldInfo = TypeHelper.ParseFields(_targetType);
            SortFieldInfo();
        }

        private void FilterFinalRenderList()
        {
            // 如果没有必要，不需要每次刷新都重新整理待显示的配置(config list)
            if (_forceToRepaint)
            {
                var result = _arrConfig;
                if (!string.IsNullOrEmpty(_searchText))
                {
                    var fi = _arrFieldInfo[_searchIndex];
                    result = _arrConfig.Where(x => fi.Info.GetValue(x).ToString().ToLower().Contains(_searchText.ToLower())).ToList();
                }

                FilterCustomContents(ref result);

                if (_supportPaging)
                {
                    _curDisplayedConfig = result.Skip(_curPage * _itemsPerPage).Take(_itemsPerPage).ToList();
                }
                else
                {
                    _curDisplayedConfig = result;
                }

                _total = result.Count;

                _forceToRepaint = false;
            }
        }

        private void RenderConfigFields()
        {
            GUILayout.BeginHorizontal(GUI.skin.GetStyle("GroupBox"));
            foreach (var field in _arrFieldInfo)
            {
                var nameAttr = field.Info.GetCustomAttribute<CustomFieldNameAttribute>();
                if (nameAttr != null)
                {
                    if (GUILayout.Button(nameAttr.CustomFieldName, GUILayout.Width(FieldWidth))) {}
                }
                else
                {
                    var customNameAttr = field.Info.GetCustomAttribute<LabelTextAttribute>();

                    if (customNameAttr != null)
                    {
                        if (GUILayout.Button(customNameAttr.Text, GUILayout.Width(FieldWidth))) { }
                    }
                    else
                    {
                        if (GUILayout.Button(field.Info.Name, GUILayout.Width(FieldWidth))) { }
                    }
                }

                GUILayout.Space(20);
            }
            // + 一些额外操作
            GUILayout.Label(new GUIContent("       ---] 操作 [---"), new GUILayoutOption[] { GUILayout.Width(FieldWidth) });
            GUILayout.Space(85);
            GUILayout.EndHorizontal();
        }

        private void RenderValue_GenericType(UnityEngine.Object conf, HMFieldInfo fi)
        {
            if (fi.Info.FieldType.IsList())
            {
                RenderValue_List(conf, fi);
            }
            else if (fi.Info.FieldType.IsHashSet())
            {
                RenderValue_HashSet(conf, fi);
            }
            else
            {
                HMLog.LogWarning($"Unrecognized type [{fi.Info.FieldType.GetNiceName()}]");
            }
        }

        // 显示HashSet类型
        private void RenderValue_HashSet(UnityEngine.Object conf, HMFieldInfo fi)
        {
            var value = fi.Info.GetValue(conf);
            // bugfix: 报错non-static method requires a target
            if (value == null)
            {
                value = Activator.CreateInstance(fi.Info.FieldType);
                fi.Info.SetValue(conf, value);
            }

            Type valueType = fi.Info.FieldType;

            GUILayout.BeginVertical(GUIStyle.none, GUILayout.Width(FieldWidth));

            // 添加新元素
            var addMethod = valueType.GetMethod("Add");
            Debug.Assert(addMethod != null);
            if (GUILayout.Button("+", GUILayout.Width(FieldWidth)))
            {
                addMethod.Invoke(value, new object[] { null });
            }

            // 元素暂存在list里进行显示，更方便一些
            var elemType = addMethod.GetParameters()[0].ParameterType;
            var listInstance = (IList)typeof(List<>).MakeGenericType(elemType).GetConstructor(Type.EmptyTypes)?.Invoke(null);
            Debug.Assert(listInstance != null);

            // move all elements from hashSet to list
            foreach (var elem in (IEnumerable) value)
            {
                listInstance.Add(elem);
            }
            for (int i = 0; i < listInstance.Count; i++)
            {
                var elem = listInstance[i];

                if (elem != null && elemType.IsSubclassOf(typeof(BaseConfig)))
                {
                    // 显示id+name
                    var baseConf = elem as BaseConfig;
                    if (baseConf != null)
                    {
                        var str = baseConf.ConfigIndex.ToString("000");
                        if (!string.IsNullOrEmpty(baseConf.Name))
                        {
                            str += "|";
                            str += baseConf.Name;
                        }
                        GUILayout.Label(str, GUILayout.Width(FieldWidth), GUILayout.Height(20));
                    }
                }

                GUIHelper.DisplayValue(elemType, elem, v =>
                    {
                        int idx = i;
                        if (elem != v && v != null && (v.GetType().IsSubclassOf(elemType) || v.GetType() == elemType))
                        {
                            listInstance[idx] = v;
                        }
                        else
                        {
                            if (v != null && !(v.GetType().IsSubclassOf(elemType) || v.GetType() == elemType))
                            {
                                Popup("类型不符合，需要{0}", elemType);
                            }
                        }
                    });
            }
            GUILayout.EndVertical();

            // copy back to hashSet
            var clearMethod = valueType.GetMethod("Clear");
            Debug.Assert(clearMethod != null);
            clearMethod.Invoke(value, null);

            foreach (var elem in listInstance)
            {
                addMethod.Invoke(value, new []{elem});
            }

            EditorUtility.SetDirty(conf);
        }

        protected virtual void RenderListItem(Type itemType, object item, int index, Action<object> onValueChanged)
        {
            if (itemType == typeof(BaseConfig) || itemType.IsSubclassOf(typeof(BaseConfig)))
            {
                RenderBaseConfig(itemType, item as BaseConfig, index, onValueChanged);
            }
            else
            {
                GUIHelper.DisplayValue(itemType, item, onValueChanged);
            }
        }

        private void RenderBaseConfig(Type configType, BaseConfig config, int index, Action<object> onValueChanged)
        {
            if (config == null)
            {
                GUIHelper.DisplayValue(configType, config, onValueChanged);

                return;
            }

            // 显示配置名
            {
                var str = config.ConfigIndex.ToString("000");
                if (!string.IsNullOrEmpty(config.Name))
                {
                    str += "|";
                    str += config.Name;
                }
                GUILayout.Label(str, GUILayout.Width(FieldWidth), GUILayout.Height(20));
            }

            // 如果配置了图片，则显示图片预览
            GUILayout.BeginHorizontal();

            var valueWidth = FieldWidth;
            var sprite = config.Image;
            var thumbnail = AssetPreview.GetAssetPreview(sprite);
            if (thumbnail != null)
            {
                GUILayout.Label(thumbnail, GUILayout.Width(20), GUILayout.Height(20));
                valueWidth -= 20;
            }

            GUIHelper.DisplayValue(typeof(BaseConfig), config, onValueChanged, valueWidth - 23);

            GUILayout.EndHorizontal();
        }

        private void RenderValue_List(UnityEngine.Object conf, HMFieldInfo fi)
        {
            GUILayout.BeginVertical(GUIStyle.none, GUILayout.Width(FieldWidth));

            var value = fi.Info.GetValue(conf);
            // bugfix: 报错non-static method requires a target
            if (value == null)
            {
                value = Activator.CreateInstance(fi.Info.FieldType);
                fi.Info.SetValue(conf, value);
            }
            Type listType = fi.Info.FieldType;

            PropertyInfo propertyInfo = listType.GetProperty("Item");
            Type elemType = propertyInfo.PropertyType;

            var addMethod = listType.GetMethod("Add");
            var removeMethod = listType.GetMethod("RemoveAt");

            if (GUILayout.Button("+", GUILayout.Width(FieldWidth)))
            {
                addMethod.Invoke(value, new object[] { null });
            }

            int count = value == null ? 0 : Convert.ToInt32(listType.GetProperty("Count").GetValue(value, null));

            int removeIndex = -1;

            for (int i = 0; i < count; i++)
            {
                object elemValue = propertyInfo.GetValue(value, new object[] { i });

                GUILayout.BeginHorizontal(GUILayout.Width(FieldWidth));
                {
                    GUILayout.Label($"--> No.{i+1}");
                    // remove button
                    if (GUILayout.Button("X", GUILayout.Width(60)))
                    {
                        removeIndex = i;
                    }
                }
                GUILayout.EndHorizontal();
                GUILayout.BeginVertical(GUILayout.Width(FieldWidth));
                {
                    RenderListItem(elemType, elemValue, i, newElem =>
                    {
                        propertyInfo.SetValue(value, newElem, new Object[]{i});
                    });
                }
                GUILayout.EndVertical();
            }

            if (removeIndex != -1)
            {
                removeMethod.Invoke(value, new object[] { removeIndex });
            }
            GUILayout.EndVertical();

            GUILayout.Space(20);
        }

        private void RenderValue_Normal(UnityEngine.Object conf, HMFieldInfo fi)
        {
            var showConfigName = false;
            // Normal type
            var value = fi.Info.GetValue(conf);
            if (value != null && (fi.Info.FieldType.IsSubclassOf(typeof(BaseConfig)) || fi.Info.FieldType == typeof(BaseConfig)))
            {
                showConfigName = true;
                GUILayout.BeginVertical(GUILayout.Width(FieldWidth));
                // 显示id+name
                var baseConf = value as BaseConfig;
                if (baseConf != null)
                {
                    var str = baseConf.ConfigIndex.ToString("000");
                    if (!string.IsNullOrEmpty(baseConf.Name))
                    {
                        str += "|";
                        str += baseConf.Name;
                    }
                    GUILayout.Label(str, GUILayout.Width(FieldWidth), GUILayout.Height(20));
                }
            }

            if (value is Sprite sprite)
            {
                GUIHelper.DisplaySpriteWithPreview(sprite, v => fi.Info.SetValue(conf, v));
            }
            else
            {
                GUIHelper.DisplayValue(fi.Info.FieldType, value, v =>
                {
                    if (value != v)
                    {
                        fi.Info.SetValue(conf, v);
                        // 如果不写这句话，那么关闭-重新打开Unity会发现数据重置了
                        EditorUtility.SetDirty(conf);
                    }
                });
            }
            if (showConfigName)
            {
                GUILayout.EndVertical();
            }
            GUILayout.Space(20);
        }

        protected virtual bool RenderValueCustom(UnityEngine.Object conf, HMFieldInfo fi)
        {
            return false;
        }

        private void RenderConfigValues()
        {
            foreach (var conf in _curDisplayedConfig.ToList())
            {
                GUILayout.BeginHorizontal(GUI.skin.GetStyle("GroupBox"));
                foreach (var fi in _arrFieldInfo)
                {
                    if (RenderValueCustom(conf, fi))
                    {

                    }
                    else if (IsSubclassOfRawGeneric(typeof(SerializableDictionary<,>), fi.Info.FieldType))
                    {
                        RenderValue_SerializableDictionary(conf, fi);
                    }
                    else if (fi.Info.FieldType.IsGenericType)
                    {
                        // todo: only List<T> is supported
                        RenderValue_GenericType(conf, fi);
                    }
                    else
                    {
                        RenderValue_Normal(conf, fi);
                    }
                }
                RenderExtensionButtons(conf);
                GUILayout.Space(20);

                if (GUILayout.Button("定位", GUI.skin.GetStyle("Button"), new GUILayoutOption[] { GUILayout.Width(50) }))
                {
                    Selection.activeObject = AssetDatabase.LoadMainAssetAtPath(AssetDatabase.GetAssetPath(conf));
                }

                GUILayout.EndHorizontal();
            }
        }

        protected virtual void RenderCustomContents() {}

        private void OnGUI()
        {
            if (CheckBeforeRender())
            {
                // 第一行显示搜索框
                RenderSearchField();

                // 第二行显示操作栏(Reload|Add|Close)
                RenderOperations();

                // 整理配置要显示的属性
                PrepareFieldInfo();

                // 子类派生接口
                RenderCustomContents();

                // 过滤要显示的配置 (RenderCustomContents可能会对列表筛选，所以优先被调用)
                FilterFinalRenderList();

                // ** 每行一个显示所有配置
                // 最外层是一个ScrollView
                _svPosition = GUILayout.BeginScrollView(_svPosition);
                GUILayout.BeginVertical(GUI.skin.GetStyle("GroupBox"));

                // 表头 - 显示配置的所有属性名
                RenderConfigFields();
                RenderConfigValues();

                // 最外层结束
                GUILayout.EndVertical();
                GUILayout.EndScrollView();
                // ** 显示所有配置结束

                // 显示分页信息
                RenderPaging();
            }
        }

        private void SortFieldInfo()
        {
            if (_arrFieldInfo == null || _arrFieldInfo.Count <= 0)return;
            _arrFieldInfo.Sort((a, b) =>
            {
                var attrA = a.Info.GetCustomAttribute<PropertyOrderAttribute>();
                var attrB = b.Info.GetCustomAttribute<PropertyOrderAttribute>();

                // 定义了order的属性会比没定义的属性靠前
                var orderA = (int)(attrA?.Order ?? 1000000);
                var orderB = (int)(attrB?.Order ?? 1000000);

                if (orderA == orderB)
                {
                    return string.Compare(a.Info.Name, b.Info.Name, StringComparison.Ordinal);
                }

                return orderA.CompareTo(orderB);
            });
        }

        private object keyObject;
        private object valueObject;

        private void RenderValue_SerializableDictionary(UnityEngine.Object conf, HMFieldInfo baseFI)
        {
            var value = baseFI.Info.GetValue(conf);

            if (value == null)
            {
                value = Activator.CreateInstance(baseFI.Info.FieldType);
                baseFI.Info.SetValue(conf, value);
            }

            ((ISerializationCallbackReceiver)value).OnBeforeSerialize();

            // todo: 只能是SerializableDictionary的第一级子类
            var fields = new List<HMFieldInfo>();
            foreach (var fi in baseFI.Info.FieldType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance).ToList())
            {
                var f = new HMFieldInfo {Info = fi};
                fields.Add(f);
            }

            var fieldKeys = new HMFieldInfo();
            fieldKeys.Info = baseFI.Info.FieldType.BaseType.GetField("keys", BindingFlags.NonPublic | BindingFlags.Instance);

            var fieldValues = new HMFieldInfo();
            fieldValues.Info = baseFI.Info.FieldType.BaseType.GetField("values", BindingFlags.NonPublic | BindingFlags.Instance);

            GUILayout.BeginVertical(GUIStyle.none, new GUILayoutOption[] { GUILayout.Width(FieldWidth) });

            {
                GUILayout.BeginVertical();

                var keyType = fieldKeys.Info.FieldType.GetGenericArguments()[0];
                if (keyType.IsPrimitive)
                {
                    if (keyObject == null || !keyObject.GetType().IsPrimitive)
                    {
                        keyObject = Activator.CreateInstance(keyType);
                    }
                }
                GUIHelper.DisplayValue(keyType, keyObject, o => keyObject = o, FieldWidth);

                var valueType = fieldValues.Info.FieldType.GetGenericArguments()[0];
                if (valueType.IsPrimitive)
                {
                    valueObject = Activator.CreateInstance(valueType);
                }
                GUIHelper.DisplayValue(valueType, valueObject, o => valueObject = o, FieldWidth);

                GUILayout.EndVertical();

                if (keyObject != null && valueObject != null)
                {
                    if (GUILayout.Button("+", GUILayout.Width(FieldWidth)))
                    {
                        var addMethodKeys = fieldKeys.Info.FieldType.GetMethod("Add");
                        addMethodKeys.Invoke(fieldKeys.Info.GetValue(value), new object[] { keyObject});

                        var addMethodValues = fieldValues.Info.FieldType.GetMethod("Add");
                        addMethodValues.Invoke(fieldValues.Info.GetValue(value), new object[] { valueObject });

                        ((ISerializationCallbackReceiver)value).OnAfterDeserialize();

                        keyObject = null;
                        valueObject = null;
                    }
                }
            }

            int removeIndex = -1;

            PropertyInfo keyPi = fieldKeys.Info.FieldType.GetProperty("Item");
            PropertyInfo valuePi = fieldValues.Info.FieldType.GetProperty("Item");

            var keyValues = fieldKeys.Info.GetValue(value);
            if (keyValues == null)
            {
                fieldKeys.Info.SetValue(value, Activator.CreateInstance(fieldKeys.Info.FieldType));
            }
            var valValues = fieldValues.Info.GetValue(value);

            int count = keyValues == null ? 0 : Convert.ToInt32(fieldKeys.Info.FieldType.GetProperty("Count").GetValue(keyValues, null));

            for (int i = 0; i < count; i++)
            {
                if (i >= Convert.ToInt32(fieldKeys.Info.FieldType.GetProperty("Count").GetValue(keyValues, null)))
                {
                    // 有可能访问一半的时候，数据才更新
                    continue;
                }
                object keyValue = keyPi.GetValue(keyValues, new object[] { i });
                Type keyType = keyPi.PropertyType;
                object valValue = valuePi.GetValue(valValues, new object[] { i });
                Type valType = valuePi.PropertyType;

                GUILayout.BeginHorizontal();
                GUILayout.BeginVertical();

                // key
                GUIHelper.DisplayValue(fieldKeys.Info.FieldType, keyValue, v =>
                {
                    if (keyValue != v && v != null && (v.GetType().IsSubclassOf(keyType) || v.GetType() == keyType))
                    {
                        keyPi.SetValue(keyValues, v, new object[] { i });
                        ((ISerializationCallbackReceiver)value).OnAfterDeserialize();
                        // 如果不写这句话，那么关闭-重新打开Unity会发现数据重置了
                        EditorUtility.SetDirty(conf);
                    }
                    else
                    {
                        if (v != null && !(v.GetType().IsSubclassOf(keyType) || v.GetType() == keyType))
                        {
                            Popup("类型不符合，需要{0}", keyType);
                        }
                    }
                }, FieldWidth);

                // value
                GUIHelper.DisplayValue(fieldValues.Info.FieldType, valValue, v =>
                {
                    if (valValue != v && v != null && (v.GetType().IsSubclassOf(valType) || v.GetType() == valType))
                    {
                        valuePi.SetValue(valValues, v, new object[] { i });
                        ((ISerializationCallbackReceiver)value).OnAfterDeserialize();
                        // 如果不写这句话，那么关闭-重新打开Unity会发现数据重置了
                        EditorUtility.SetDirty(conf);
                    }
                    else
                    {
                        if (v != null && !(v.GetType().IsSubclassOf(valType) || v.GetType() == valType))
                        {
                            Popup("类型不符合，需要{0}", valType);
                        }
                    }
                }, FieldWidth);
                GUILayout.EndVertical();

                if (true) // Event.current.type == EventType.Layout
                {
                    // 不要在Repaint和Layout事件之间对数据增减，会报错
                    if (GUILayout.Button("X", new GUILayoutOption[] { GUILayout.Width(20) }))
                    {
                        removeIndex = i;
                    }

                    if (removeIndex != -1)
                    {
                        var rmMethodKeys = fieldKeys.Info.FieldType.GetMethod("RemoveAt");
                        rmMethodKeys.Invoke(fieldKeys.Info.GetValue(value), new object[] { removeIndex });

                        var rmMethodValues = fieldValues.Info.FieldType.GetMethod("RemoveAt");
                        rmMethodValues.Invoke(fieldValues.Info.GetValue(value), new object[] { removeIndex });

                        ((ISerializationCallbackReceiver)value).OnAfterDeserialize();

                        removeIndex = -1;
                    }
                }

                GUILayout.EndHorizontal();
            }

            GUILayout.EndVertical();
            GUILayout.Space(20);
        }

        static bool IsSubclassOfRawGeneric(Type generic, Type toCheck)
        {
            while (toCheck != null && toCheck != typeof(object))
            {
                var cur = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
                if (generic == cur)
                {
                    return true;
                }
                toCheck = toCheck.BaseType;
            }
            return false;
        }

        /// <summary>
        /// 刷新全局配置
        /// </summary>
        private void AutoFindSettings()
        {
            _settingPath = string.Empty;
            AssetDatabase.Refresh();
            var guids = AssetDatabase.FindAssets("t:ConfigSettings");
            if (guids.Length > 0)
            {
                _settingPath = AssetDatabase.GUIDToAssetPath(guids[0]);
                _setting = AssetDatabase.LoadAssetAtPath<ConfigSettings>(_settingPath);
            }
        }

        private void RenderSearchField()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            var names = new string[_arrFieldInfo.Count];
            for (int i = 0; i < _arrFieldInfo.Count; i++)
            {
                names[i] = _arrFieldInfo[i].Info.Name;
            }

            // choose which field to sort by.
            do
            {
                int v = _searchIndex;
                _searchIndex = EditorGUILayout.Popup(_searchIndex, names, new GUILayoutOption[] { GUILayout.Width(FieldWidth), GUILayout.Height(40) });
                if (v != _searchIndex)
                {
                    _forceToRepaint = true;
                }
            } while (false);

            // type search keyword
            do
            {
                string v = _searchText;
                _searchText = EditorGUILayout.TextField(_searchText, GUI.skin.GetStyle("ToolbarSeachTextField"), new GUILayoutOption[] { GUILayout.Width(300) });
                if (string.IsNullOrEmpty(v) != string.IsNullOrEmpty(_searchText))
                {
                    _forceToRepaint = true;
                }
                if (!string.IsNullOrEmpty(_searchText) && !_searchText.Equals(v))
                {
                    _forceToRepaint = true;
                }
            } while (false);
            GUILayout.EndHorizontal();
        }

        private void RenderOperations()
        {
            GUILayout.BeginHorizontal(GUI.skin.GetStyle("GroupBox"));

            if (GUILayout.Button("Reload", GUI.skin.GetStyle("ButtonLeft"), new GUILayoutOption[] { GUILayout.Height(30) }))
            {
                ReloadAllConfigs();
                _forceToRepaint = true;
            }

            if (GUILayout.Button("Add", GUI.skin.GetStyle("ButtonMid"), new GUILayoutOption[] { GUILayout.Height(30) }))
            {
                AddConfig();
                _forceToRepaint = true;
            }

            if (GUILayout.Button("Close(X)", GUI.skin.GetStyle("ButtonRight"), new GUILayoutOption[] { GUILayout.Height(30) }))
            {
                Close();
            }

            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal(GUI.skin.GetStyle("GroupBox"), GUILayout.Width(FieldWidth));
            if (GUILayout.Button("Add Multiple Records:", GUI.skin.GetStyle("Button"), new GUILayoutOption[] { GUILayout.Height(30) }))
            {
                AddConfigs(_rowsToAdd);
                _forceToRepaint = true;
            }
            int.TryParse(GUILayout.TextField(_rowsToAdd.ToString(), new GUILayoutOption[] { GUILayout.Height(30), GUILayout.Width(60) }), out _rowsToAdd);
            GUILayout.EndHorizontal();
        }

        public void ReloadAllConfigs()
        {
            // find all configs
            _arrConfig.Clear();
            var dir = Path.Combine(_setting.ConfigAssetRoot, TargetTypeConfig.ConfigName);
            foreach (var guid in AssetDatabase.FindAssets("t:" + _targetTypeName, new string[] { dir }))
            {
                _arrConfig.Add(AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guid), _targetType));
            }
            _forceToRepaint = true;
        }

        private void AddConfigs(int num)
        {
            var dir = Path.Combine(_setting.ConfigAssetRoot, TargetTypeConfig.ConfigName);
            Directory.CreateDirectory(dir);

            for (int i = 0; i < num; i++)
            {
                var id = NextId();

                var asset = (BaseConfig)ScriptableObject.CreateInstance(_targetType);
                asset.Id = id;
                asset.OnCreated();
                AssetDatabase.CreateAsset(asset, Path.Combine(dir, ConfigFileName(id) + ".asset"));
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            // Reload
            ReloadAllConfigs();
            Popup("创建成功");
        }

        private void AddConfig()
        {
            var dir = Path.Combine(_setting.ConfigAssetRoot, TargetTypeConfig.ConfigName);
            Directory.CreateDirectory(dir);

            var id = NextId();

            var asset = (BaseConfig)ScriptableObject.CreateInstance(_targetType);
            asset.Id = id;
            asset.OnCreated();
            AssetDatabase.CreateAsset(asset, Path.Combine(dir, ConfigFileName(id) + ".asset"));
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            // Reload
            ReloadAllConfigs();
            Popup("创建成功");
        }

        private void GenerateAll()
        {
            int i = 1;
            foreach (var c in _arrConfig)
            {
                var bc = (BaseConfig)c;
                // new id
                bc.Id = ConfigId(i);

                Debug.Log("Rename -> " + AssetDatabase.GetAssetPath(c) + " to " + ConfigFileName(bc.Id));

                // new asset name
                Debug.Log(AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(c), ConfigFileName(bc.Id) + ".asset"));
                i++;

            }

            AssetDatabase.Refresh();

            _forceToRepaint = true;
        }

        private int NextId()
        {
            var dir = Path.Combine(_setting.ConfigAssetRoot, TargetTypeConfig.ConfigName);

            var filenames = new List<string>();
            foreach (var guid in AssetDatabase.FindAssets("t:" + _targetTypeName, new string[] { dir }))
            {
                filenames.Add(Path.GetFileNameWithoutExtension(AssetDatabase.GUIDToAssetPath(guid)));
            }

            int i = 1;
            for (; i <= filenames.Count; i++)
            {
                var id = ConfigId(i);
                if (!filenames.Contains(ConfigFileName(id)))
                {
                    return id;
                }
            }
            return ConfigId(i);
        }

        private int ConfigId(int idx)
        {
            return Convert.ToInt32(string.Format("{0:D3}{1:D3}{2:D3}", TargetTypeConfig.MajorType, TargetTypeConfig.MinorType, idx));
        }

        private string ConfigFileName(int id)
        {
            return string.Format("{0}{1}", TargetTypeConfig.ConfigName, id);
        }

        private void RenderExtensionButtons(UnityEngine.Object src)
        {
            // 拷贝
            if (GUILayout.Button("拷贝", GUI.skin.GetStyle("ButtonLeft"), new GUILayoutOption[] { GUILayout.Width(40) }))
            {
                _copy = Instantiate(src);
                Popup("拷贝成功");
            }

            // 删除
            if (GUILayout.Button("删除", GUI.skin.GetStyle("ButtonMid"), new GUILayoutOption[] { GUILayout.Width(40) }))
            {
                if (AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(src)))
                {
                    Popup("删除成功");
                    ReloadAllConfigs();
                }
            }

            // 粘贴
            if (GUILayout.Button("粘贴", GUI.skin.GetStyle("ButtonRight"), new GUILayoutOption[] { GUILayout.Width(40) }))
            {
                if (_copy == null)
                {
                    Popup("请先复制数据！");
                    return;
                }

                if (_arrFieldInfo.Count == 0)
                {
                    Popup("请刷新再试");
                    return;
                }

                foreach (var fi in _arrFieldInfo)
                {
                    if (fi.Info.Name == "Id")continue;
                    fi.Info.SetValue(src, fi.Info.GetValue(_copy));
                }
            }
        }

        protected void RenderPaging()
        {
            GUILayout.BeginHorizontal(GUI.skin.GetStyle("GroupBox"));
            int maxIndex = Mathf.FloorToInt((_total - 1) / (float)_itemsPerPage);

            if (maxIndex < _curPage)
            {
                _curPage = 0;
            }

            // 允许设置单页显示的数量
            GUILayout.Label(string.Format(@"共{2}项 | 第{0}页/共{1}页", _curPage + 1, maxIndex + 1, _total), GUILayout.Width(120));
            GUILayout.Label("单页最大数量", GUILayout.Width(80));
            int v = _itemsPerPage;
            int.TryParse(GUILayout.TextField(_itemsPerPage.ToString(), GUILayout.Width(80)), out _itemsPerPage);
            if (v != _itemsPerPage)
            {
                _forceToRepaint = true;
            }

            if (GUILayout.Button("前页", GUI.skin.GetStyle("ButtonLeft"), GUILayout.Height(16)))
            {
                if (_curPage - 1 < 0)
                    _curPage = 0;
                else
                    _curPage -= 1;

                _forceToRepaint = true;
            }

            if (GUILayout.Button("后页", GUI.skin.GetStyle("ButtonRight"), GUILayout.Height(16)))
            {
                if (_curPage + 1 > maxIndex)
                    _curPage = maxIndex;
                else
                    _curPage++;

                _forceToRepaint = true;
            }

            GUILayout.EndHorizontal();
        }

        protected void Popup(string content, params object[] objects)
        {
            if (objects.Length > 0)
            {
                ShowNotification(new GUIContent(string.Format(content, objects)));
            }
            else
            {
                ShowNotification(new GUIContent(content));
            }
        }

        #region Subclass should override


        private string GetAssemblyName()
        {
            return _setting.AssemblyName;
        }

        private string GetConfigNamespace()
        {
            return _setting.ConfigNamespace;
        }

        private string GetConfigTypeName(string configName)
        {
            return $"{GetConfigNamespace()}.{configName}Config, {GetAssemblyName()}";
        }

        #endregion
    }
}