/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2020-06-01 11:06:28
 * @modify date 2020-06-01 11:06:28
 * @desc [自动生成ConfigChecker.cs]
 */

using System.Collections.Generic;
using System.IO;
using System.Text;
using HM;
using HM.ConfigTool;
using UnityEditor;

namespace NewLife.EditorTool
{
    public class GenerateConfigChecker
    {
        // 0: majorType
        private static readonly string _majorDef = "private const int Major{0} = {0};";
        // 0: majorType
        // 1: minorType
        private static readonly string _minorDef = "private const int Minor{0}{1:d3} = {1};";

        private const string NameSpace = "NewLife.BusinessLogic.Item";

        /// <summary>
        /// 0: configName
        /// 1: majorType
        /// 2: minorType
        /// </summary>
        private static readonly string _methodDef =
            "public bool IsItem{0}(int itemId){{return ConfigCheckerBase.IsMajorType(itemId, Major{1}) && ConfigCheckerBase.IsMinorType(itemId, Minor{1}{2:d3});}}";

        private static readonly string _methodDel = "bool IsItem{0}(int itemId);";
        private const string ClassTemplatePath = "Assets/_Game/Scripts/Editor/GenerateConfigChecker/ConfigCheckerTemplate.txt";
        private const string InterfaceTemplatePath = "Assets/_Game/Scripts/Editor/GenerateConfigChecker/IConfigCheckerTemplate.txt";

        // FIXME: 各个项目调整ConfigChecker所在路径
        private const string IConfigCheckerPath = "Assets/_Game/Scripts/BusinessLogic/Item/IConfigChecker.cs";
        private const string ConfigCheckerPath = "Assets/_Game/Scripts/BusinessLogic/Item/ConfigChecker.cs";

        [MenuItem("Assets/HelloMeow/Generate ConfigChecker.cs")]
        public static void Run()
        {
            HMLog.LogInfo($"请确保ConfigChecker路径正确:{ConfigCheckerPath}");
            // locate all TypeConfig assets
            var typeConfigs = new List<TypeConfig>();
            foreach (var guid in AssetDatabase.FindAssets("t:TypeConfig"))
            {
                var asset = AssetDatabase.LoadAssetAtPath<TypeConfig>(AssetDatabase.GUIDToAssetPath(guid));
                typeConfigs.Add(asset);
            }

            // sort
            typeConfigs.Sort((a, b) =>
            {
                if (a.MajorType == b.MajorType) return a.MinorType.CompareTo(b.MinorType);
                return a.MajorType.CompareTo(b.MajorType);
            });

            GenerateClass(typeConfigs);
            GenerateInterface(typeConfigs);
        }

        private static void GenerateClass(List<TypeConfig> typeConfigs)
        {
            Dictionary<int, bool> majorMap = new Dictionary<int, bool>();
            // generate check method for each config
            var sb = new StringBuilder();
            var tab = "\t\t";
            foreach (var typeConfig in typeConfigs)
            {
                // define major type
                if (!majorMap.ContainsKey(typeConfig.MajorType))
                {
                    sb.AppendLine(tab + string.Format(_majorDef, typeConfig.MajorType));
                    majorMap.Add(typeConfig.MajorType, true);
                }

                // define minor type
                sb.AppendLine(tab + string.Format(_minorDef, typeConfig.MajorType, typeConfig.MinorType));

                // create check method
                sb.AppendLine(tab +
                              string.Format(_methodDef, typeConfig.ConfigName, typeConfig.MajorType,
                                            typeConfig.MinorType));
            }

            // open template and write
            var template = File.ReadAllText(ClassTemplatePath);
            template = template.Replace("$[namespace]", NameSpace);
            template = template.Replace("$[content]", sb.ToString());

            File.WriteAllText(ConfigCheckerPath, template, Encoding.UTF8);
        }

        private static void GenerateInterface(List<TypeConfig> typeConfigs)
        {
            // generate check method for each config
            var sb = new StringBuilder();
            var tab = "\t\t";
            foreach (var typeConfig in typeConfigs)
            {
                // create check method
                sb.AppendLine(tab + string.Format(_methodDel, typeConfig.ConfigName));
            }

            // open template and write
            var template = File.ReadAllText(InterfaceTemplatePath);
            template = template.Replace("$[namespace]", NameSpace);
            template = template.Replace("$[content]", sb.ToString());


            File.WriteAllText(IConfigCheckerPath, template, Encoding.UTF8);
        }
    }
}