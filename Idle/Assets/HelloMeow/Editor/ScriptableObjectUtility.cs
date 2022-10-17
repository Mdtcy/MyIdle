using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace HM.AssetEditing
{
    public static class ScriptableObjectUtility
    {
        /// <summary>
        //  This makes it easy to create, name and place unique new ScriptableObject asset files.
        /// </summary>
        public static void CreateAsset<T> () where T : ScriptableObject
        {
            T asset = ScriptableObject.CreateInstance<T> ();

            string path = AssetDatabase.GetAssetPath (Selection.activeObject);
            if (path == "")
            {
                path = "Assets";
            }
            else if (Path.GetExtension (path) != "")
            {
                path = path.Replace (Path.GetFileName (AssetDatabase.GetAssetPath (Selection.activeObject)), "");
            }

            string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath (path + "/New " + typeof(T).ToString() + ".asset");

            AssetDatabase.CreateAsset (asset, assetPathAndName);

            AssetDatabase.SaveAssets ();
            AssetDatabase.Refresh();
            EditorUtility.FocusProjectWindow ();
            Selection.activeObject = asset;
        }

        public static T GetAsset<T> (string assetName) where T : ScriptableObject
        {
            string path = "Assets/Resources/Assets/" + assetName + ".asset";

            FileInfo fileInfo = new FileInfo(path);
            if ( !fileInfo.Exists ) {
                bool ret = EditorUtility.DisplayDialog( "Error", assetName + " does not exist, create One?.", "yes", "no");
                if (ret) {
                    return CreateAndReturnAsset<T> (assetName);
                }
            }

            return AssetDatabase.LoadAssetAtPath<T> (path);
        }

        public static T CreateAndReturnAsset<T> (string assetName) where T : ScriptableObject
        {
            T asset = ScriptableObject.CreateInstance<T> ();

            string path = "Assets/Resources/Assets/" + assetName + ".asset";
            string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath (path);

            bool doCreate = true;
            FileInfo fileInfo = new FileInfo(path);
            if ( fileInfo.Exists ) {
                doCreate = EditorUtility.DisplayDialog( assetName + " already exists.",
                    "Do you want to overwrite the old one?",
                    "Yes", "No" );
            }

            if (doCreate) {
                AssetDatabase.DeleteAsset (path);
                AssetDatabase.CreateAsset (asset, path);
                AssetDatabase.SaveAssets ();
                AssetDatabase.Refresh ();
                EditorUtility.FocusProjectWindow ();
            } else {
                asset = AssetDatabase.LoadAssetAtPath<T> (path);
            }
            return asset;
        }

        /// <summary>
        /// Create a ScriptableObject of type T, calling the method Initialize() if present.
        /// </summary>
        /// <typeparam name="T">The ScriptableObject type.</typeparam>
        /// <returns>The new ScriptableObject.</returns>
        public static T CreateScriptableObject<T>()where T : ScriptableObject
        {
            var scriptableObject = ScriptableObject.CreateInstance<T>()as T;
            InitializeScriptableObject(scriptableObject);
            return scriptableObject;
        }

        /// <summary>
        /// Create a ScriptableObject of a specified type, calling Initialize() if present.
        /// </summary>
        /// <param name="type">The ScriptableObject type.</param>
        /// <returns>The new ScriptableObject.</returns>
        public static ScriptableObject CreateScriptableObject(Type type)
        {
            var scriptableObject = ScriptableObject.CreateInstance(type);
            InitializeScriptableObject(scriptableObject);
            return scriptableObject;
        }

        /// <summary>
        /// Calls Initialize() on a ScriptableObject if present.
        /// </summary>
        /// <param name="scriptableObject">The ScriptableObject to initialize.</param>
        public static void InitializeScriptableObject(ScriptableObject scriptableObject)
        {
            if (scriptableObject == null)return;
            var methodInfo = scriptableObject.GetType().GetMethod("Initialize");
            if (methodInfo != null)methodInfo.Invoke(scriptableObject, null);
        }

        /// <summary>
        /// Makes a deep copy of a ScriptableObject list by instantiating copies of the
        /// list elements.
        /// </summary>
        /// <param name="original">List to clone.</param>
        /// <returns>A second list containing new instances of the list elements.</returns>
        public static List<T> CloneList<T>(List<T> original)where T : ScriptableObject
        {
            var copy = new List<T>();
            if (original != null)
            {
                for (int i = 0; i < original.Count; i++)
                {
                    if (original[i] is T)
                    {
                        copy.Add(ScriptableObject.Instantiate(original[i])as T);
                    }
                    else
                    {
                        if (Debug.isDebugBuild)Debug.LogWarning("CloneList<" + typeof(T).Name + ">: Element " + i + " is null.");
                        copy.Add(null);
                    }
                }
            }
            return copy;
        }
    }
}
