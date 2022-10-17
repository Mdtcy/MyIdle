/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2020-07-01 14:07:37
 * @modify date 2020-07-01 14:07:37
 * @desc [description]
 */

using System;
using System.Collections.Generic;
using HM.ConfigTool;
using HM.GameBase;
using NewLife.Defined;
using NewLife.Defined.Condition;
using UnityEngine;
using Object = UnityEngine.Object;

namespace NewLife.Config
{
    public class NewLifeConfigCollectionEditor : ConfigCollectionEditor
    {
        protected override bool RenderValueCustom(Object conf, HMFieldInfo fi)
        {
            if (fi.Info.FieldType == typeof(List<UnionConditionConfig>))
            {
                RenderListOfCondition(conf, fi);
                return true;
            }
            if (fi.Info.FieldType == typeof(List<UnionCommandConfig>))
            {
                RenderListOfCommand(conf, fi);
                return true;
            }
            return false;
        }

        private void RenderListOfCommand(Object conf, HMFieldInfo fi)
        {
            GUILayout.BeginVertical(GUIStyle.none, GUILayout.Width(FieldWidth));
            {
                var listType = fi.Info.FieldType;
                var value = fi.Info.GetValue(conf);
                // bugfix: 报错non-static method requires a target
                if (value == null)
                {
                    value = Activator.CreateInstance(listType);
                    fi.Info.SetValue(conf, value);
                }

                var list = value as List<UnionCommandConfig>;
                Debug.Assert(list != null);

                var addMethod = listType.GetMethod("Add");
                Debug.Assert(addMethod != null);
                if (GUILayout.Button("+", GUILayout.Width(FieldWidth)))
                {
                    addMethod.Invoke(list, new object[] { new UnionCommandConfig() });
                }

                int removeIndex = -1;
                for (int i = 0; i < list.Count; i++)
                {
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
                    var command = list[i];
                    GUILayout.BeginVertical(GUILayout.Width(FieldWidth));
                    {
                        // 类型
                        GUIHelper.DisplayValue(typeof(CommandType), command.CommandType,
                                               v => { command.CommandType = (CommandType) v; });
                        if (command.CommandType == CommandType.AcquireItem)
                        {
                            // 配置
                            GUIHelper.DisplayValue(typeof(BaseConfig), command.Target,
                                                   v => { command.Target = v as BaseConfig; });
                            // 数量
                            GUIHelper.DisplayValue(typeof(int), command.Num,
                                                   v => { command.Num = (int) v; });
                        }
                        else
                        {
                            GUILayout.Label("请指定条件类型");
                        }
                    }
                    GUILayout.EndVertical();
                }

                if (removeIndex != -1)
                {
                    list.RemoveAt(removeIndex);
                }
            }
            GUILayout.EndVertical();
            GUILayout.Space(20);
        }

        private void RenderListOfCondition(Object conf, HMFieldInfo fi)
        {
            GUILayout.BeginVertical(GUIStyle.none, GUILayout.Width(FieldWidth));
            {
                var listType = fi.Info.FieldType;
                var value = fi.Info.GetValue(conf);
                // bugfix: 报错non-static method requires a target
                if (value == null)
                {
                    value = Activator.CreateInstance(listType);
                    fi.Info.SetValue(conf, value);
                }

                var list = value as List<UnionConditionConfig>;
                Debug.Assert(list != null);

                var addMethod = listType.GetMethod("Add");
                Debug.Assert(addMethod != null);
                if (GUILayout.Button("+", GUILayout.Width(FieldWidth)))
                {
                    addMethod.Invoke(list, new object[] { new UnionConditionConfig() });
                }

                int removeIndex = -1;
                for (int i = 0; i < list.Count; i++)
                {
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
                    var condition = list[i];
                    GUILayout.BeginVertical(GUILayout.Width(FieldWidth));
                    {
                        // 类型
                        GUIHelper.DisplayValue(typeof(ConditionType), condition.ConditionType,
                                               v => { condition.ConditionType = (ConditionType) v; });

                        GUILayout.Label("请指定条件类型");
                    }
                    GUILayout.EndVertical();
                }

                if (removeIndex != -1)
                {
                    list.RemoveAt(removeIndex);
                }
            }
            GUILayout.EndVertical();
            GUILayout.Space(20);
        }
    }
}
