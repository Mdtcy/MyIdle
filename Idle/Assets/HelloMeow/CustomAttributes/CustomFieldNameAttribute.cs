/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2020-03-13 15:03:18
 * @modify date 2020-03-13 15:03:18
 * @desc [description]
 */

using System;
using UnityEngine;

namespace HM.CustomAttributes
{
	[AttributeUsage(AttributeTargets.Field)]
	public class CustomFieldNameAttribute : PropertyAttribute
	{
		public readonly string CustomFieldName;

		public CustomFieldNameAttribute(string customFieldName)
		{
			CustomFieldName = customFieldName;
		}
	}
}