using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace HM
{
    public static class UiHelper
    {
        static public GameObject AddChild(GameObject parent, string name)
        {
            if (parent == null)
                throw new ArgumentNullException("parent");

            var go = new GameObject(name);
            go.layer = parent.layer;

            var tc = go.GetComponent<RectTransform>();
            var tp = parent.GetComponent<RectTransform>();
            tc.SetParent(tp, false);

            return go;
        }

        public static GameObject AddChild(GameObject parent, GameObject prefab)
        {
            if (parent == null)
                throw new ArgumentNullException("parent");
            if (prefab == null)
                throw new ArgumentNullException("prefab");

            var go = Object.Instantiate(prefab);
            go.layer = parent.layer;

            var tc = go.GetComponent<RectTransform>();
            var tp = parent.GetComponent<RectTransform>();
            tc.SetParent(tp, false);

            return go;
        }
    }
}