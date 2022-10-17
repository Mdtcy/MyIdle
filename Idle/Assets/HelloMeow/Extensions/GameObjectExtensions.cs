using UnityEngine;

namespace HM.Extensions {
	public static class GameObjectExtensions
	{
		/// <summary>
		/// Gets or add a component. Usage example:
		/// BoxCollider boxCollider = transform.GetOrAddComponent<BoxCollider>();
		/// </summary>
		static public T GetOrAddComponent<T> (this GameObject child) where T: Component {
			T result = child.GetComponent<T>();
			if (result == null) {
				result = child.gameObject.AddComponent<T>();
			}
			return result;
        }

        static public bool TryGetComponent<T> (this GameObject child, ref T comp) where T: Component {
			comp = child.GetComponent<T>();
            return comp != null;
        }

        public static RectTransform RTrans(this GameObject go)
        {
            return go.GetComponent<RectTransform>();
        }

	    public static T FindInParentByType<T>(this GameObject go) where T:Component
	    {
	        var parent = go.transform.parent;
	        T com = null;
	        while (parent)
	        {
	            com = parent.GetComponent<T>();
	            if (com)
	                return com;
	            parent = parent.parent;
	        }
	        return null;
	    }

        /// <summary>
        /// Shortcut
        /// </summary>
        static public GameObject CreateChild(this GameObject obj, string name)
        {
            GameObject robj = new GameObject();
            robj.name = name;
            robj.transform.parent = obj.transform;

            return robj;
        }
        /// <summary>
        /// Get full path to object
        /// </summary>
        static public string GetFullName(this GameObject theObj)
        {
            string path = "/" + theObj.name;
            GameObject obj = theObj;
            while (obj.transform.parent != null)
            {
                obj = obj.transform.parent.gameObject;
                path = "/" + obj.name + path;
            }

            return path;
        }
	}
}
