/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2020-06-12 13:06:16
 * @modify date 2020-06-12 13:06:16
 * @desc [description]
 */

/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2020-03-06 14:03:49
 * @modify date 2020-03-06 14:03:49
 * @desc [description]
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using HM;
using HM.Notification;
using NewLife.Defined;
using UnityEditor;

namespace NewLife.EditorTool
{
    public class GenerateNotifyKeysEnum
    {
        private static string enumSavePath = "Assets/_Game/Configs/NotifyKeysEnum.cs";
        private static string fileTemplate = "namespace EditorOnly {\n**FILE_CONTENT**}";
        private static string enumTemplate = "\tpublic enum NotifyKeysEditorEnum\n\t{**ENUMS**}";

        [MenuItem("Assets/HelloMeow/Generate Notify Keys Enum")]
        public static void GenerateEnum()
        {
            GenerateForTypes(typeof(NotifyKeys), typeof(NewLifeNotifyKeys));
        }

        private static void GenerateForTypes(params Type[] types)
        {
            // generate enum definition
            var enums = new StringBuilder("\n");

            foreach (var type in types)
            {
                // acquire field information
                var fields = new List<FieldInfo>();

                foreach (var fi in type.GetFields().ToList())
                {
                    if (!fi.IsStatic) continue;
                    fields.Add(fi);
                }

                // sort by field name
                fields.Sort((a, b) => string.Compare(a.Name, b.Name, StringComparison.Ordinal));

                foreach (var fieldInfo in fields)
                {
                    enums.AppendLine($"\t\t{fieldInfo.Name} = {fieldInfo.GetValue(type)},");
                }
            }

            var content = enumTemplate.Replace("**ENUMS**", enums.ToString());
            var fileContent = fileTemplate.Replace("**FILE_CONTENT**", content);
            // save to disk
            var filePath = Path.GetFullPath(enumSavePath);
            File.WriteAllText(filePath, fileContent);

            HMLog.LogDebug($"Generate Notify Keys Enum at path [{filePath}]");
        }
    }
}