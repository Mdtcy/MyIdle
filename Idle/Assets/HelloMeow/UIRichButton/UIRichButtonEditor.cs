/**
 * @author [BoLuo]
 * @email [tktetb@163.com]
 * @create date 2021-01-14
 * @modify date 2021-01-14
 * @desc [UIRichButton inspector界面]
 */

#if UNITY_EDITOR

    using HM.UIRichButton;
    using UnityEditor;
    using UnityEditor.UI;

    /// <summary>
    /// UIRichButton inspector界面
    /// </summary>
    [CustomEditor(typeof(UIRichButtonMaster))]
    public class UIRichButtonEditor : ButtonEditor
    {
        SerializedProperty m_OnDiasbledProperty;
        SerializedProperty m_OnHishlightedProperty;
        SerializedProperty m_OnNormalProperty;
        SerializedProperty m_OnPressedProperty;
        SerializedProperty m_OnSelectedProperty;

        protected override void OnEnable()
        {
            base.OnEnable();
            m_OnDiasbledProperty = serializedObject.FindProperty("evtOnDisabled");
            m_OnHishlightedProperty = serializedObject.FindProperty("evtOnHighlighted");
            m_OnNormalProperty = serializedObject.FindProperty("evtOnNormal");
            m_OnPressedProperty = serializedObject.FindProperty("evtOnPressed");
            m_OnSelectedProperty = serializedObject.FindProperty("evtOnSelected");
        }

        public override void OnInspectorGUI()
        {
            // Show default inspector property editor
            // DrawDefaultInspector();
            base.OnInspectorGUI();

            EditorGUILayout.Space();

            serializedObject.Update();
            EditorGUILayout.PropertyField(m_OnDiasbledProperty);
            EditorGUILayout.PropertyField(m_OnHishlightedProperty);
            EditorGUILayout.PropertyField(m_OnNormalProperty);
            EditorGUILayout.PropertyField(m_OnPressedProperty);
            EditorGUILayout.PropertyField(m_OnSelectedProperty);
            serializedObject.ApplyModifiedProperties();

        }
    }

#endif