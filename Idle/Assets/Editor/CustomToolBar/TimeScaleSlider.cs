using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace UnityToolbarExtender
{

    [InitializeOnLoad]
    public class TimeScaleSlider
    {
        static TimeScaleSlider()
        {
            ToolbarExtender.RightToolbarGUI.Add(OnToolbarGUI);
        }

        private static void OnToolbarGUI()
        {
            EditorGUILayout.LabelField("Time", GUILayout.Width(30));
            Time.timeScale =
                EditorGUILayout.Slider("", Time.timeScale, 0, 100, GUILayout.Width(200 - 30.0f));

            if (GUILayout.Button("Reset", GUILayout.Width(50)))
            {
                Time.timeScale = 1;
            }
        }
    }
}
