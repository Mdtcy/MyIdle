﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using System;
using System.Linq;

namespace MoreMountains.Tools
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(MonoBehaviour), true, isFallback = true)]
    public class MMMonoBehaviourDrawer : UnityEditor.Editor
    {
        /*
        public static event Action DrawAfterGUI;*/

        private bool _requiresConstantRepaint;

        protected virtual void OnEnable()
        {
            if (!target || !serializedObject.targetObject)
            {
                return;
            }

            _requiresConstantRepaint =
                serializedObject.targetObject.GetType().GetCustomAttribute<MMRequiresConstantRepaintAttribute>() != null;
        }

        /*
        private readonly List<GenericInspectorDrawer> _drawers = new List<GenericInspectorDrawer>();

        protected virtual void OnEnable()
        {
            if (!target || !serializedObject.targetObject)
            {
                return;
            }

            _drawers.Add(new ButtonDrawer(this, target, serializedObject));
            _drawers.Add(new ScriptableObjectDrawer(this, target, serializedObject));
            _drawers.Add(new ReorderableListDrawer(this, target, serializedObject));

            _requiresConstantRepaint =
                serializedObject.targetObject.GetType().GetCustomAttribute<RequiresConstantRepaintAttribute>() != null;
        }

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();
            serializedObject.Update();

            BeforeDrawInspector();

            DrawInspector(serializedObject.GetIterator());

            AfterDrawInspector();

            if (DrawAfterGUI != null)
            {
                DrawAfterGUI();
                DrawAfterGUI = null;
            }

            serializedObject.ApplyModifiedProperties();

            if (EditorGUI.EndChangeCheck())
            {
                OnInspectorChanged();
            }
        }

        protected virtual void BeforeDrawInspector()
        {
            for (int i = 0; i < _drawers.Count; ++i)
            {
                _drawers[i].BeforeDrawInspector();
            }
        }

        protected virtual void DrawInspector(SerializedProperty iterator)
        {
            for (var enterChildren = true; iterator.NextVisible(enterChildren); enterChildren = false)
            {
                DrawProperty(iterator);
            }
        }

        protected virtual void AfterDrawInspector()
        {
            for (int i = 0; i < _drawers.Count; ++i)
            {
                _drawers[i].AfterDrawInspector();
            }
        }

        protected virtual void OnInspectorChanged()
        {
            for (int i = 0; i < _drawers.Count; ++i)
            {
                _drawers[i].OnInspectorChanged();
            }
        }

        protected virtual void DrawProperty(SerializedProperty property)
        {
            for (var i = 0; i < _drawers.Count; ++i)
            {
                var inspectorDrawer = _drawers[i];
                if (!inspectorDrawer.CanDrawProperty(property))
                {
                    continue;
                }

                inspectorDrawer.DrawProperty(property);
                return;
            }

            // Default inspector behavior
            using (new EditorGUI.DisabledScope("m_Script" == property.propertyPath))
            {
                EditorGUILayout.PropertyField(property, true);
            }
        }*/

        public override bool RequiresConstantRepaint()
        {
            return _requiresConstantRepaint;
        }
    }
}