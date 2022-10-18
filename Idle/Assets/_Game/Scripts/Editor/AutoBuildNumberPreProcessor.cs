/**
 * @author [BoLuo]
 * @email [tktetb@163.com]
 * @create date 2020-1-6 05:51:18
 * @desc [打包预处理工具，自动更新小版本号]
 */
using System;
using UnityEditor;
using Debug = UnityEngine.Debug;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

/// <summary>
/// 打包预处理工具，自动更新小版本号
/// </summary>
public class AutoBuildNumberPreProcessor : Editor, IPreprocessBuildWithReport
{
    #region IPreprocessBuildWithReport

    /// <inheritdoc />
    public int callbackOrder => 9; // 要在BuildInfoPreProcessor之前

    /// <inheritdoc />
    public void OnPreprocessBuild(BuildReport report)
    {
        #if UNITY_IOS
        var prev = PlayerSettings.iOS.buildNumber;
        PlayerSettings.iOS.buildNumber = (Convert.ToInt32(prev) + 1).ToString();
        Debug.Log($"更新iOS版本号为:V{PlayerSettings.bundleVersion}({PlayerSettings.iOS.buildNumber})");
        #elif UNITY_ANDROID
        PlayerSettings.Android.bundleVersionCode++;
        Debug.Log($"更新安卓版本号为:V{PlayerSettings.bundleVersion}({PlayerSettings.Android.bundleVersionCode})");
        #endif
    }

    #endregion
}