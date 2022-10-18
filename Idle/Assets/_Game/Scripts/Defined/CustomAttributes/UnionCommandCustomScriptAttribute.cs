/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2022-10-05 10:31:31
 * @modify date 2022-10-05 10:31:31
 * @desc []
 */

using System;
using UnityEngine;

#pragma warning disable 0649
namespace NewLife.Defined.CustomAttributes
{
	[AttributeUsage(AttributeTargets.Field)]
    public class UnionCommandCustomScriptAttribute : PropertyAttribute
    {
    }
}
#pragma warning restore 0649