/**
 * @author [jie.wen]
 * @email [example@mail.com]
 * @create date 2019-05-07 09:57:05
 * @modify date 2019-05-07 09:57:05
 * @desc [将某个类型的所有配置导出成excel]
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HM.ConfigTool;
using HM.EditorOnly.TypeParser;
using HM.GameBase;
using Sirenix.Utilities;
using Unity.EditorCoroutines.Editor;
using UnityEditor;
using UnityEngine;
using Object = System.Object;

namespace HM.EditorOnly
{
    public abstract class ConfigToolkit
    {
        private static readonly IGridWriter Writer = new GridCsvWriter();
        private static readonly IGridReader Reader = new GridCsvReader();
        // 如果为null，标记为NUL
        private const string NullStr = "NUL";
        // 标记不会导出/导入的值
        private const string SkipToken = "SKP";

        private readonly HelloMeowTypeSerializer serializer;

        // 不支持的类型
        private readonly List<Type> skippedTypes = new List<Type>();

        // 需要具体项目提供配置类型的全称
        protected abstract string GetConfigFullTypeName(string typename);

        private bool ShouldSkipType(Type type)
        {
            return skippedTypes.Contains(type);
        }

        protected ConfigToolkit(HelloMeowTypeSerializer serializer)
        {
            this.serializer = serializer;
        }

        /// <summary>
        /// 导出指定配置到csv文件
        /// </summary>
        /// <param name="typeConfig"></param>
        /// <param name="outputPath"></param>
        public void Export(TypeConfig typeConfig, string outputPath, EditorWindow window)
        {
            HMLog.LogDebug($"开始导出{typeConfig.ConfigName}");

            HMLog.LogVerbose("不支持的类型：");

            foreach (var type in skippedTypes)
            {
                HMLog.LogVerbose($"* {type}");
            }

            window.StartCoroutine(SteppedExport(typeConfig, outputPath, window));
        }

        private IEnumerator SteppedExport(TypeConfig typeConfig, string outputPath, EditorWindow window)
        {
            // 确定目标配置类型XyzConfig
            string configTypeName = $"{typeConfig.ConfigName}Config";
            var configType = Type.GetType(GetConfigFullTypeName(configTypeName));

            // 拿到所有配置asset
            var configs = LoadAssetsOfType(typeConfig);

            // 拿到所有的fields
            var fields = TypeHelper.ParseFields(configType);

            // 开始写
            Writer.NewGrid(outputPath);

            // 写表头（各个field名）
            Writer.NewRow();
            foreach (var field in fields)
            {
                Writer.AppendCell(field.Info.Name);
            }

            // 写类型（各个field类型）
            Writer.NewRow();
            foreach (var field in fields)
            {
                Writer.AppendCell(field.Info.FieldType.ToString());
            }

            // 写入每个配置的值
            foreach (var config in configs)
            {
                Writer.NewRow();
                foreach (var field in fields)
                {
                    if (field.Info.FieldType.IsGenericType)
                    {
                        // only List<T> is supported
                        var list = field.Info.GetValue(config);
                        var listType = field.Info.FieldType;

                        if (list == null)
                        {
                            Writer.AppendCell(NullStr);
                            continue;
                        }

                        int count = Convert.ToInt32(listType.GetProperty("Count").GetValue(list, null));
                        if (count <= 0)
                        {
                            Writer.AppendCell(NullStr);
                            continue;
                        }

                        var itemInfo = listType.GetProperty("Item");
                        Debug.Assert(itemInfo != null);
                        var itemType  = itemInfo.PropertyType;
                        // 数组内的元素拼接成字符串，用'|'分隔
                        var sb = new System.Text.StringBuilder();
                        for (int i = 0; i < count; i++)
                        {
                            var itemValue = itemInfo.GetValue(list, new object[] {i});
                            sb.Append(serializer.ToString(itemType, itemValue));
                            if (i < count - 1)
                            {
                                // 最后一个元素之后不加分隔符，否则导入时分隔符之后被认为是一个空元素
                                sb.Append("|");
                            }
                        }
                        Writer.AppendCell(sb.ToString());
                    }
                    else
                    {
                        var value = field.Info.GetValue(config);
                        var valueType = field.Info.FieldType;
                        Writer.AppendCell(serializer.ToString(valueType, value));
                    }
                }
            }

            Writer.FinishGrid();
            HMLog.LogDebug($"导出成功，路径为[{outputPath}]");

            yield break;
        }

        public void Import(TypeConfig typeConfig, string path, EditorWindow window)
        {
            HMLog.LogDebug($"开始导入：路径=[{path}]，目标类型={typeConfig.ConfigName}");

            HMLog.LogVerbose("不支持的类型：");
            foreach (var type in skippedTypes)
            {
                HMLog.LogVerbose($"* {type}");
            }

            window.StartCoroutine(SteppedImport(typeConfig, path, window));

        }

        private IEnumerator SteppedImport(TypeConfig typeConfig, string path, EditorWindow window)
        {
            // 确定目标配置类型XyzConfig
            string configTypeName = $"{typeConfig.ConfigName}Config";
            var configType = Type.GetType(GetConfigFullTypeName(configTypeName));

            // 拿到所有配置asset
            var configs = LoadAssetsOfType(typeConfig);

            // 拿到所有的fields
            var fields = TypeHelper.ParseFields(configType);

            // 加载到内存
            Reader.Load(path);
            // 检查下
            Debug.Assert(Reader.Count() >= 2); // 至少包括field和类型两行

            int totalCnt = Reader.Count() - 2;
            int succeedCnt = 0;

            // 第一行是field名
            var fieldNames = Reader.NextRow();
            // 找到id对应的位置
            int idIndex = 0;
            foreach (string s in fieldNames)
            {
                if (s.Equals("Id")) break;
                idIndex++;
            }
            // 第二行是类型，跳过
            Reader.NextRow();

            int i = 1;
            // 一行一行加载
            while (Reader.HasNext())
            {
                var row = Reader.NextRow();
                var id = Convert.ToInt32(row[idIndex]);

                bool alreadyExist = false;
                foreach (var config in configs)
                {
                    if (((BaseConfig) config).Id.Equals(id))
                    {
                        // 找到了对应配置
                        HMLog.LogVerbose($"[ConfigToolkit]导入({i}/{configs.Count})::更新已有配置[id = {id}]");
                        LoadDataToConfig(row, fieldNames, config, fields);
                        succeedCnt++;
                        alreadyExist = true;
                        i++;
                    }
                }

                if (!alreadyExist)
                {
                    // 不存在现有配置，创建新的
                    HMLog.LogVerbose($"[ConfigToolkit]导入({i}/{configs.Count})::创建新配置[id = {id}]");
                    var config = CreateNewConfig(typeConfig.ConfigName, id);
                    LoadDataToConfig(row, fieldNames, config, fields);
                    succeedCnt++;
                    i++;
                }

                yield return new EditorWaitForSeconds(0.1f);
            }
            AssetDatabase.SaveAssets();
            HMLog.LogDebug($"导入完成：总计{totalCnt}条记录，成功导入{succeedCnt}条，失败{(totalCnt - succeedCnt)}条");
        }

        private BaseConfig CreateNewConfig(string configName, int id)
        {
            string configTypeName = $"{configName}Config";
            var configType = Type.GetType(GetConfigFullTypeName(configTypeName));

            HMLog.LogVerbose($"创建新配置:({configName}{id}.asset), " +
                             $"保存路径:{ConfigAssetDir(configName)}");

            var asset = ScriptableObject.CreateInstance(configType);
            ((BaseConfig)asset).Id = id;
            AssetDatabase.CreateAsset(asset, Path.Combine(ConfigAssetDir(configName), $"{configName}{id}.asset"));
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            return asset as BaseConfig;
        }

        private ConfigSettings configSettings;
        private ConfigSettings FindConfigSettings()
        {
            if (configSettings != null)
            {
                return configSettings;
            }

            var guids = AssetDatabase.FindAssets("t:ConfigSettings");
            if (guids.Length <= 0)
            {
                HMLog.LogWarning($"未能找到ConfigSettings");
                return null;
            }

            configSettings = AssetDatabase.LoadAssetAtPath<ConfigSettings>(AssetDatabase.GUIDToAssetPath(guids[0]));
            return configSettings;
        }

        private string ConfigAssetDir(string configTypeName)
        {
            var settings = FindConfigSettings();
            if (settings == null)
            {
                return null;
            }

            var dir = Path.Combine(settings.ConfigAssetRoot, configTypeName);
            Directory.CreateDirectory(dir);
            return dir;
        }

        private void LoadDataToConfig(List<string> fieldValues,
                                      List<string> fieldNames,
                                      Object config,
                                      List<HMFieldInfo> fieldInfos)
        {
            Debug.Assert(fieldNames.Count == fieldValues.Count);
            for (int i = 0; i < fieldNames.Count; i++)
            {
                string name = fieldNames[i];
                string value = fieldValues[i];
                // 标注跳过则不写入
                if (value.Equals(SkipToken)) continue;

                foreach (var fi in fieldInfos)
                {
                    if (fi.Info.Name.Equals(name))
                    {
                        // 找到了对应字段，开始写入
                        var valueType = fi.Info.FieldType;

                        if (ShouldSkipType(valueType))
                        {
                            HMLog.LogVerbose($"[ConfigToolkit]跳过不支持的类型:{valueType}");
                            break;
                        }

                        if (valueType.IsGenericType)
                        {
                            var list = fi.Info.GetValue(config);

                            // 清空list
                            if (list == null)
                            {
                                list = Activator.CreateInstance(fi.Info.FieldType);
                                fi.Info.SetValue(config, list);
                            }
                            else
                            {
                                valueType.ClearList(list);
                            }

                            if (value == NullStr)
                            {
                                continue;
                            }

                            var itemInfo = fi.Info.FieldType.GetListItemPropertyInfo();
                            Debug.Assert(itemInfo != null);
                            var itemType  = itemInfo.PropertyType;

                            // 数组元素是由"|"拼接在一起的
                            var items = value.Split("|".ToCharArray()).ToList();
                            for (int j = 0; j < items.Count; j++)
                            {
                                string item = items[j];
                                // 数组+1个空位
                                valueType.AppendItemToList(list);

                                if (serializer.TryToParse(itemType, item, out var result))
                                {
                                    itemInfo.SetValue(list, result, new object[] {j});
                                }
                                else
                                {
                                    HMLog.LogVerbose($"[ConfigToolkit]读取数据失败：fieldName={name}, value = {value}, itemIndex = {j}");
                                }
                            }
                        }
                        else
                        {
                            if (serializer.TryToParse(valueType, value, out var result))
                            {
                                fi.Info.SetValue(config, result);
                            }
                            else
                            {
                                HMLog.LogVerbose($"[ConfigToolkit]读取数据失败：fieldName={name}, value = {value}");
                            }
                        }
                    }
                }
            }
            EditorUtility.SetDirty((UnityEngine.Object)config);
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
    }
}