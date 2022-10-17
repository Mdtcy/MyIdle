/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2020-03-06 14:03:22
 * @modify date 2020-03-06 14:03:22
 * @desc [Select AppConfig from inspector]
 */

using System;
using UnityEngine;

namespace HM.CustomAttributes
{
	[AttributeUsage(AttributeTargets.Field)]
	public class AppConfigAttribute : PropertyAttribute
	{
	}
}