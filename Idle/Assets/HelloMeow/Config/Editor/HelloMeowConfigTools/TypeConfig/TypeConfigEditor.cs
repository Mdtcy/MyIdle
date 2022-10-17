/*****************************************************************************/
/****************** Auto Generate Script, Do Not Modify! *********************/
/*****************************************************************************/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using HM.EditorOnly;
using HM.EditorOnly.TypeParser;
using HM.GameBase;
using UnityEditor;
using UnityEngine;
using Object = System.Object;

namespace HM.ConfigTool
{
    public class TypeConfigEditor : EditorWindow
    {
        private class TypeConfigHistory
        {
            public int MajorType;
            public int MinorType;
            public string ConfigName;
            public string InheritFrom;

            public TypeConfigHistory(TypeConfig config)
            {
                MajorType = config.MajorType;
                MinorType = config.MinorType;
                ConfigName = config.ConfigName;
                InheritFrom = config.InheritFrom;
            }

        }
        /// <summary>
        /// 所有配置
        /// </summary>
        /// <returns></returns>
        private List<TypeConfig> _arrConfig = new List<TypeConfig>();

        /// <summary>
        /// 当前正在显示的集合，比如搜索出来的结果
        /// </summary>
        /// <returns></returns>
        private List<TypeConfig> _curDisplayedConfig = new List<TypeConfig>();

        /// <summary>
        /// 存放拷贝操作的被拷贝对象
        /// </summary>
        private TypeConfig _copy;

        /// <summary>
        /// ScrollView的当前位置
        /// </summary>
        private Vector2 _svPosition;

        /// <summary>
        /// 目标类的field信息
        /// </summary>
        /// <returns></returns>
        private List<HMFieldInfo> _arrFieldInfo = new List<HMFieldInfo>();

        private Dictionary<TypeConfig, TypeConfigHistory> _histories = new Dictionary<TypeConfig, TypeConfigHistory>();

        /// <summary>
        /// 标记某列此时是按照升序还是降序排列
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, bool> _fieldOrderByAcscending = new Dictionary<string, bool>();

        /// <summary>
        /// 全局配置
        /// </summary>
        private ConfigSettings _setting;

        /// <summary>
        /// 全局配置所在路径
        /// </summary>
        private string _settingPath;

        /// <summary>
        /// 用户在搜索框中输入的文本内容
        /// </summary>
        private string _searchText;

        /// <summary>
        /// 用户选择的待搜索的范围（在哪个属性里搜索）
        /// </summary>
        private int _searchIndex = 0;

        /// <summary>
        /// 每页显示多少行数据，默认100行
        /// </summary>
        private int _itemsPerPage = 100;

        /// <summary>
        /// 当前是第几页
        /// </summary>
        private int _curPage = 0;

        /// <summary>
        /// 是否需要reload待显示的数据
        /// </summary>
        private bool _isDataDirty = true;

        /// <summary>
        /// 存放所有需要显示的数据的总数
        /// </summary>
        private int _total = 0;

        private bool _openNewWindow = true;

        // 需要具体项目提供配置类型的全称
        protected virtual string GetConfigFullTypeName(string typename)
        {
            return null;
        }

        // Json导出工具
        private ConfigJsonExporter jsonExporter;

        private ConfigJsonExporter JsonExporter => jsonExporter ??= GetJsonExporter();

        // 配置导入导出工具
        private ConfigToolkit configToolkit;

        private ConfigToolkit ConfigToolkit => configToolkit ?? (configToolkit = GetOrCreateConfigToolkit());

        private string GetConfigPath(string filename)
        {
            if (string.IsNullOrEmpty(filename))
            {
                return _setting.TypeConfigAssetRoot;
            }
            else
            {
                return Path.Combine(_setting.TypeConfigAssetRoot, filename);
            }
        }

        // 菜单入口
        public static void OpenView()
        {
            GetWindow(typeof(TypeConfigEditor), false, "Config Editor");
        }

        protected virtual ConfigToolkit GetOrCreateConfigToolkit()
        {
            throw new NotImplementedException();
        }

        private HelloMeowTypeSerializer serializer;

        private void OnGUI()
        {
            if (null == _setting)
            {
                AutoFindSettings();
            }

            if (null == _setting)
            {
                Popup("未找到ConfigSetting配置文件，请创建");
                return;
            }

            _arrFieldInfo = TypeHelper.ParseFields(typeof(TypeConfig));

            // 全局配置路径
            GUILayout.BeginHorizontal(GUI.skin.GetStyle("GroupBox"));
            GUILayout.Label("全局配置路径:" + _settingPath);
            if (GUILayout.Button("自动查找", GUI.skin.GetStyle("Button")))
            {
                AutoFindSettings();

                if (_setting)
                {
                    Popup("全局配置加载成功");
                }

                _isDataDirty = true;
            }
            GUILayout.EndHorizontal();

            SetupAreaSearchField();

            // 操作按钮s
            SetupAreaOperations();

            _svPosition = GUILayout.BeginScrollView(_svPosition);
            GUILayout.BeginVertical(GUI.skin.GetStyle("GroupBox"));
            // title
            GUILayout.BeginHorizontal(GUI.skin.GetStyle("GroupBox"));
            foreach (var field in _arrFieldInfo)
            {
                if (GUILayout.Button(field.Info.Name, GUILayout.Width(100)))
                {
                    SortBy(field);
                }
                GUILayout.Space(20);
            }
            // 基本操作
            GUILayout.Label(new GUIContent("       ---] 操作 [---"), new GUILayoutOption[] { GUILayout.Width(100) });
            GUILayout.Space(85);
            _openNewWindow = GUILayout.Toggle(_openNewWindow, "是否打开新窗口");

            GUILayout.EndHorizontal();

            if (_isDataDirty)
            {
                if (!string.IsNullOrEmpty(_searchText))
                {
                    var fi = _arrFieldInfo[_searchIndex];
                    _curDisplayedConfig = _arrConfig
                        // .Where(x => x.MajorType == Convert.ToInt32(_searchText))
                        .Where(x => fi.Info.GetValue(x).ToString().ToLower().Contains(_searchText.ToLower()))
                        .Skip(_curPage * _itemsPerPage).Take(_itemsPerPage).ToList();
                    _total = _arrConfig.Where(x => fi.Info.GetValue(x).ToString().ToLower().Contains(_searchText.ToLower())).ToList().Count;
                }
                else
                {
                    _curDisplayedConfig = _arrConfig.Skip(_curPage * _itemsPerPage).Take(_itemsPerPage).ToList();
                    _total = _arrConfig.Count;
                }
                _isDataDirty = false;
            }

            // values
            foreach (var conf in _curDisplayedConfig.ToList())
            {
                bool clsExist = File.Exists(Path.Combine(_setting.ConfigClassOutputRoot, $"{conf.ConfigName}Config.cs"));
                var c = GUI.backgroundColor;
                if (!clsExist)
                {
                    GUI.backgroundColor = Color.yellow;
                }
                GUILayout.BeginHorizontal(GUI.skin.GetStyle("GroupBox"));
                foreach (var fi in _arrFieldInfo)
                {
                    var value = fi.Info.GetValue(conf);
                    GUIHelper.DisplayValue(fi.Info.FieldType, value, v =>
                    {
                        fi.Info.SetValue(conf, v);

                        if (!_histories.ContainsKey(conf))
                        {
                            _histories.Add(conf, new TypeConfigHistory(conf));
                        }
                        // 如果不写这句话，那么关闭-重新打开Unity会发现数据重置了
                        EditorUtility.SetDirty(conf);
                    });
                    GUILayout.Space(20);
                }
                RenderGenerateButton(conf);
                GUILayout.Space(20);
                RenderExtensionButtons(conf);
                GUILayout.EndHorizontal();
                GUI.backgroundColor = c;
            }
            GUILayout.EndVertical();
            GUILayout.EndScrollView();

            RenderPaging();
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

        private void SortBy(HMFieldInfo field)
        {
            var name = field.Info.Name;
            bool orderByAscending = _fieldOrderByAcscending.ContainsKey(name) && _fieldOrderByAcscending[name];
            _fieldOrderByAcscending[name] = !orderByAscending;
            if (orderByAscending)
            {
                _curDisplayedConfig = _curDisplayedConfig.OrderBy(x => field.Info.GetValue(x)).ToList();
            }
            else
            {
                _curDisplayedConfig = _curDisplayedConfig.OrderByDescending(x => field.Info.GetValue(x)).ToList();
            }
        }

        private void SetupAreaSearchField()
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
                _searchIndex = EditorGUILayout.Popup(_searchIndex, names, new GUILayoutOption[] { GUILayout.Width(100) });
                if (v != _searchIndex)
                {
                    _isDataDirty = true;
                }
            } while (false);

            // type search keyword
            do
            {
                string v = _searchText;
                _searchText = EditorGUILayout.TextField(_searchText, GUI.skin.GetStyle("ToolbarSeachTextField"), new GUILayoutOption[] { GUILayout.Width(300) });
                if (!string.IsNullOrEmpty(v) && !v.Equals(_searchText))
                {
                    _isDataDirty = true;
                }
            } while (false);
            GUILayout.EndHorizontal();
        }

        private void RenderExtensionButtons(TypeConfig src)
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
                RemoveTypeConfigFiles(src.ConfigName);
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
                    fi.Info.SetValue(src, fi.Info.GetValue(_copy));
                }
            }
        }

        protected virtual ConfigCollectionEditor GetDetailedEditor()
        {
            return GetOrCreateDetailedWindow<ConfigCollectionEditor>();
        }

        protected virtual ConfigJsonExporter GetJsonExporter()
        {
            return new ConfigJsonExporter();
        }

        protected ConfigCollectionEditor GetOrCreateDetailedWindow<T>() where T : ConfigCollectionEditor
        {
            if (_openNewWindow)
            {
                return CreateInstance<T>();
            }
            else
            {
                return EditorWindow.GetWindow(typeof(T), false) as T;
            }
        }

        private void RenderGenerateButton(TypeConfig src)
        {

            if (GUILayout.Button("GO", GUI.skin.GetStyle("ButtonLeft"), new GUILayoutOption[] { GUILayout.Width(40) }))
            {
                // create assets dir
                var dir = Path.Combine(_setting.ConfigAssetRoot, src.ConfigName);
                Directory.CreateDirectory(dir);
                var window = GetDetailedEditor();
                window.minSize = new Vector2(1000, 700);
                window.TargetTypeConfig = src;
                window.titleContent = new GUIContent(src.ConfigName);
                window.Show();
            }

            if (GUILayout.Button("生成", GUI.skin.GetStyle("ButtonMid"), GUILayout.Width(40)))
            {
                Generate(src);
            }

            if (GUILayout.Button("更新", GUI.skin.GetStyle("ButtonRight"), GUILayout.Width(40)))
            {
                TryUpdate(src);
            }

            if (GUILayout.Button("导出Csv", GUI.skin.GetStyle("ButtonLeft"), GUILayout.Width(75)))
            {
                string path = EditorUtility.SaveFilePanel("选择导出路径",
                                                       Application.dataPath,
                                                       $"导出{src.ConfigName}",
                                                       "csv");

                if (string.IsNullOrEmpty(path))
                {
                    return;
                }
                ConfigToolkit.Export(src, path, this);
            }

            if (GUILayout.Button("导入Csv", GUI.skin.GetStyle("ButtonRight"), GUILayout.Width(75)))
            {
                string path = EditorUtility.OpenFilePanel("选择导入路径",
                                                       Application.dataPath,
                                                       "csv");

                if (string.IsNullOrEmpty(path))
                {
                    return;
                }
                ConfigToolkit.Import(src, path, this);
            }

            if (GUILayout.Button("导出Json", GUI.skin.GetStyle("Button"), GUILayout.Width(75)))
            {
                string dir = Path.Combine(Application.dataPath, "JsonExported");
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                dir = EditorUtility.SaveFolderPanel("选择导出Json目录",
                                                    dir,
                                                    "");
                if (string.IsNullOrEmpty(dir))
                {
                    return;
                }

                // 拿到所有配置asset
                var configs = LoadAssetsOfType(src);

                foreach (var config in configs)
                {
                    JsonExporter.ExportJson(config as BaseConfig, dir);
                }
            }
        }

        private List<Object> LoadAssetsOfType(TypeConfig typeConfig)
        {
            // 确定目标配置类型XyzConfig
            string configTypeName = $"{typeConfig.ConfigName}Config";
            var configType = Type.GetType(GetConfigFullTypeName(configTypeName));

            // 拿到所有配置asset
            var configs = new List<Object>();
            foreach (string guid in AssetDatabase.FindAssets($"t:{configTypeName}"))
            {
                configs.Add(AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guid), configType));
            }

            return configs;
        }

        private void OpenDetailedEditor<T>(bool forceOpenNewWindow, TypeConfig target)where T : ConfigCollectionEditor
        {
            if (forceOpenNewWindow)
            {
                var newWindow = CreateInstance<T>();
                newWindow.titleContent = new GUIContent(target.ConfigName);
                newWindow.TargetTypeConfig = target;
                newWindow.Show();
            }
            else
            {
                var window = (T)EditorWindow.GetWindow(typeof(T), false, target.ConfigName);
                window.TargetTypeConfig = target;
                window.Show();
            }
        }

        private void SetupAreaOperations()
        {
            GUILayout.BeginHorizontal(GUI.skin.GetStyle("GroupBox"));

            if (GUILayout.Button("Reload", GUI.skin.GetStyle("ButtonLeft"), new GUILayoutOption[] { GUILayout.Height(30) }))
            {
                ReloadAllConfigs();
                _isDataDirty = true;
            }

            if (GUILayout.Button("Add", GUI.skin.GetStyle("ButtonMid"), new GUILayoutOption[] { GUILayout.Height(30) }))
            {
                AddConfig();
                _isDataDirty = true;
            }

            if (GUILayout.Button("Export(Json)", GUI.skin.GetStyle("ButtonMid"), new GUILayoutOption[] { GUILayout.Height(30) }))
            {
                ExportAllToJson();
            }

            // 如果有需要再关掉comment
            // if (GUILayout.Button("重命名TypeConfig", GUI.skin.GetStyle("ButtonMid"), new GUILayoutOption[] { GUILayout.Height(30) }))
            // {
            //     // Rename all config asset
            //     var guids = AssetDatabase.FindAssets("t:TypeConfig", new[] {GetConfigPath(null)});
            //     foreach (string guid in guids)
            //     {
            //         string path       = AssetDatabase.GUIDToAssetPath(guid);
            //         var    typeConfig = AssetDatabase.LoadAssetAtPath<TypeConfig>(path);
            //         AssetDatabase.RenameAsset(path, GetTypeConfigAssetName(typeConfig));
            //     }
            // }

            if (GUILayout.Button("Close(X)", GUI.skin.GetStyle("ButtonRight"), new GUILayoutOption[] { GUILayout.Height(30) }))
            {
                GenerateAll();
                _isDataDirty = true;
            }

            GUILayout.EndHorizontal();
        }

        private void ExportAllToJson()
        {
            string dir = Path.Combine(Application.dataPath, "JsonExported");
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            dir = EditorUtility.SaveFolderPanel("选择导出Json目录",
                                                dir,
                                                "");
            if (string.IsNullOrEmpty(dir))
            {
                return;
            }

            // 拿到所有配置asset
            foreach (var config in LoadAllConfigs())
            {
                GetJsonExporter().ExportJson(config, dir);
            }
        }

        private List<BaseConfig> LoadAllConfigs()
        {
            var configs = new List<BaseConfig>();
            foreach (var typeConfig in _arrConfig)
            {
                string dir = Path.Combine(_setting.ConfigAssetRoot, typeConfig.ConfigName);
                string targetTypeName = typeConfig.ConfigName + "Config";
                foreach (string guid in AssetDatabase.FindAssets("t:" + targetTypeName, new[] { dir }))
                {
                    configs.Add(AssetDatabase.LoadAssetAtPath<BaseConfig>(AssetDatabase.GUIDToAssetPath(guid)));
                }
            }
            return configs;
        }

        private void SortConfigs()
        {
            _arrConfig.Sort((a, b) =>
            {
                if (a.MajorType == b.MajorType) return a.MinorType.CompareTo(b.MinorType);
                return a.MajorType.CompareTo(b.MajorType);
            });
        }

        private void ReloadAllConfigs()
        {
            AutoFindSettings();
            _arrConfig.Clear();
            foreach (var guid in AssetDatabase.FindAssets("t:TypeConfig", new string[] { GetConfigPath(null) }))
            {
                _arrConfig.Add(AssetDatabase.LoadAssetAtPath<TypeConfig>(AssetDatabase.GUIDToAssetPath(guid)));
            }

            if (_arrConfig.Count == 0)
            {
                Popup("{0}目录中没有找到配置", GetConfigPath(null));
                return;
            }
            SortConfigs();
            _isDataDirty = true;
        }

        private string GetTypeConfigAssetName(TypeConfig typeConfig)
        {
            return $"TypeConfig_{typeConfig.ConfigName}_{typeConfig.MajorType:000}{typeConfig.MinorType:000}.asset";
        }

        private void AddConfig()
        {
            Directory.CreateDirectory(GetConfigPath(null));
            var guids = AssetDatabase.FindAssets("t:TypeConfig", new[] {GetConfigPath(null)});
            ReloadAllConfigs();

            // create new config and name it based on config count.
            var assetName = $"TypeConfig_Untitled_{guids.Length}.asset";
            var config = ScriptableObject.CreateInstance<TypeConfig>();
            AssetDatabase.CreateAsset(config, GetConfigPath(assetName));
            AssetDatabase.SaveAssets();

            // Reload
            ReloadAllConfigs();
            Popup("创建{0}成功", assetName);
        }

        private void GenerateAll()
        {
            Close();
        }

        public void RemoveTypeConfigFiles(string configName)
        {
            var classFilePath = Path.Combine(_setting.ConfigClassOutputRoot, configName + "Config.cs");
            if (File.Exists(classFilePath))
            {
                HM.HMLog.LogDebug("删除文件{0}", classFilePath);
                File.Delete(classFilePath);
            }
        }

        private void TryUpdate(TypeConfig config)
        {
            if (!_histories.ContainsKey(config)) return;

            var oldConf = _histories[config];

            bool majorTypeModified = oldConf.MajorType.Equals(config.MajorType);
            bool minorTypeModified = oldConf.MinorType.Equals(config.MinorType);
            bool configNameTypeModified = oldConf.ConfigName.Equals(config.ConfigName);

            // 更新asset
            if (majorTypeModified || minorTypeModified || configNameTypeModified)
            {
                foreach (string guid in AssetDatabase.FindAssets($"t:{oldConf.ConfigName}Config"))
                {
                    string path = AssetDatabase.GUIDToAssetPath(guid);
                    var baseConfig = AssetDatabase.LoadAssetAtPath<BaseConfig>(path);
                    // 修改id
                    int newId = Convert.ToInt32($"{config.MajorType:000}{config.MinorType:000}{baseConfig.ConfigIndex:000}");
                    baseConfig.Id = newId;
                    EditorUtility.SetDirty(baseConfig);
                    // 修改name
                    string newName = $"{config.ConfigName}{newId}";
                    AssetDatabase.RenameAsset(path, newName);
                }
            }

            // 更新配置类
            UpdateConfig(oldConf.ConfigName, config);

            // 更新配置编辑器类
            // UpdateConfigEditor(oldConf.ConfigName, config);

            // 更新自己的名字
            if (majorTypeModified || minorTypeModified || configNameTypeModified)
            {
                RenameTypeConfigAsset(config);
            }
            //
            _histories.Remove(config);
        }

        private void UpdateConfig(string oldConfigName, TypeConfig config)
        {
            string oldClassName = $"{oldConfigName}Config";
            string oldPath =  Path.Combine(_setting.ConfigClassOutputRoot, oldClassName+".cs");
            if (!File.Exists(oldPath))
            {
                HMLog.LogWarning($"未找到配置类，请检查路径:{oldPath}");
                return;
            }

            string content = File.ReadAllText(oldPath);

            {
                var regex = new Regex("fileName = \"([^\"]*)\", menuName = \"Config/([^\"]*)\"");
                var match = regex.Match(content);
                Debug.Assert(match.Groups.Count >= 3);
                var matchedContent = match.Groups[0].ToString();
                var fileNameMatched = match.Groups[1].ToString();
                var menuNameMatched = match.Groups[2].ToString();
                HMLog.LogDebug($"Whole = {matchedContent}, fileNameMatched = {fileNameMatched}, menuNameMatched = {menuNameMatched}");

                string newContent = matchedContent.Replace(fileNameMatched, $"{config.ConfigName}");
                newContent = newContent.Replace(menuNameMatched, $"{config.ConfigName}");
                content = content.Replace(matchedContent, newContent);
            }

            {
                var regex = new Regex("class\\s*([a-zA-Z]*Config)\\s*:\\s*([a-zA-Z]*Config)");
                var match = regex.Match(content);
                Debug.Assert(match.Groups.Count >= 3);
                var matchedContent = match.Groups[0].ToString();
                var className = match.Groups[1].ToString();
                var baseClassName = match.Groups[2].ToString();
                HMLog.LogDebug($"Whole = {matchedContent}, old = {className}, base = {baseClassName}");

                string newContent = matchedContent.Replace(className, $"{config.ConfigName}Config");
                newContent = newContent.Replace(baseClassName, $"{config.InheritFrom}");
                content = content.Replace(matchedContent, newContent);
            }


            // 保存
            // * 文件名要同步修改（如果改了的话）
            string newPath = Path.Combine(_setting.ConfigClassOutputRoot, config.ConfigName + "Config.cs");
            File.Move(oldPath, newPath);
            File.WriteAllText(newPath, content, System.Text.Encoding.UTF8);
        }

        private void RenameTypeConfigAsset(TypeConfig conf)
        {
            HMLog.LogVerbose($"重命名TypeConfig: {GetConfigPath(conf.name+".asset")} ===> {GetTypeConfigAssetName(conf)}");
            AssetDatabase.RenameAsset(GetConfigPath(conf.name+".asset"), GetTypeConfigAssetName(conf));
            AssetDatabase.SaveAssets();
        }

        private void Generate(TypeConfig conf)
        {
            // 重命名conf自己的名字
            RenameTypeConfigAsset(conf);

            // 如果XyzConfig.cs已经存在，则给确认框
            Directory.CreateDirectory(_setting.ConfigClassOutputRoot);
            var outputPath = Path.Combine(_setting.ConfigClassOutputRoot, conf.ConfigName + "Config.cs");
            if (File.Exists(outputPath))
            {
                if (!EditorUtility.DisplayDialog("配置已存在",
                                                 $"是否替换已有配置：{"Assets" + outputPath.Replace(Application.dataPath, "")}?",
                                                 "确认", "取消"))
                {
                    return;
                }
            }

            GenerateConfig(conf.ConfigName, conf.InheritFrom);
            // GenerateConfigEditor(conf.ConfigName, conf.MajorType, conf.MinorType);
            // create assets dir
            var dir = Path.Combine(_setting.ConfigAssetRoot, conf.ConfigName);
            Directory.CreateDirectory(dir);

            AssetDatabase.Refresh();
        }

        private void GenerateConfig(string confName, string inheritFrom)
        {
            Directory.CreateDirectory(_setting.ConfigClassOutputRoot);

            var className = confName + "Config.cs";
            var outputPath = Path.Combine(_setting.ConfigClassOutputRoot, className);

            // remove old if exists
            if (File.Exists(outputPath))
            {
                File.Delete(outputPath);
            }

            // load template and replace with given config information.
            var templatePath = Path.Combine(_setting.TemplateRoot, "XyzConfig.txt");
            if (!File.Exists(templatePath))
            {
                Popup("Template not exist at path:{0}", templatePath);
                return;
            }
            var template = File.ReadAllText(templatePath);
            // todo
            template = template.Replace("$[Namespace]", _setting.ConfigNamespace);
            template = template.Replace("$[ConfigName]", confName);
            template = template.Replace("$[InheritFrom]", inheritFrom);

            // save
            File.WriteAllText(outputPath, template, System.Text.Encoding.UTF8);
        }

        private void Popup(string content, params object[] objects)
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
                _isDataDirty = true;
            }

            if (GUILayout.Button("前页", GUI.skin.GetStyle("ButtonLeft"), GUILayout.Height(16)))
            {
                if (_curPage - 1 < 0)
                    _curPage = 0;
                else
                    _curPage -= 1;

                _isDataDirty = true;
            }

            if (GUILayout.Button("后页", GUI.skin.GetStyle("ButtonRight"), GUILayout.Height(16)))
            {
                if (_curPage + 1 > maxIndex)
                    _curPage = maxIndex;
                else
                    _curPage++;

                _isDataDirty = true;
            }

            GUILayout.EndHorizontal();
        }
    }
}