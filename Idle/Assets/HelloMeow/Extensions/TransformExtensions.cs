/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2019-10-24 17:10:31
 * @modify date 2019-10-24 17:10:31
 * @desc [description]
 */

using System.Collections.Generic;
using UnityEngine;

namespace HM.Extensions
{
    public static class TransformExtensions
    {
        /// <summary>
		/// Gets or add a component. Usage example:
		/// BoxCollider boxCollider = transform.GetOrAddComponent<BoxCollider>();
		/// </summary>
		public static T GetOrAddComponent<T> (this Transform self) where T: Component
        {
			T result = self.GetComponent<T>();
			if (result == null) {
				result = self.gameObject.AddComponent<T>();
			}
			return result;
        }

        /// <summary>
        /// 当前transform是否有指定类型的component
        /// </summary>
        /// <param name="self"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool HasComponent<T>(this Transform self) where T: Component
        {
	        return self.GetComponent<T>() != null;
        }

        public static Transform GetLastChild(this Transform trans)
        {
            if (trans.childCount > 0)
            {
                return trans.GetChild(trans.childCount - 1);
            }
            return null;
        }

        /// <summary>
        /// 删除等于指定名字的子节点(EditorOnly)
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="name"></param>
        public static void DestroyChildrenImmediateWithName(this Transform transform, string name)
		{
			//Add children to list before destroying
			//otherwise GetChild(i) may bomb out
			var children = new List<Transform>();

			for (var i = 0; i < transform.childCount; i++)
			{
				var child = transform.GetChild(i);

				if (child.name == name)
				{
					children.Add(child);
				}
			}

			foreach (var child in children)
			{
				Object.DestroyImmediate(child.gameObject);
			}
		}

        /// <summary>
        /// 直接删除指定transform的所有子节点(EditorOnly)
        /// </summary>
        /// <param name="transform"></param>
        public static void DestroyChildrenImmediate(this Transform transform)
		{
			//Add children to list before destroying
			//otherwise GetChild(i) may bomb out
			var children = new List<Transform>();

			for (var i = 0; i < transform.childCount; i++)
			{
				var child = transform.GetChild(i);
				children.Add(child);
			}

			foreach (var child in children)
			{
				Object.DestroyImmediate(child.gameObject);
			}
		}
    }
}