using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
#if UNITY_IOS
using System.IO;
using UnityEditor.iOS.Xcode;
#endif

public class PostBuildForIOS
{
    [PostProcessBuild]
    public static void OnPostProcessBuild(BuildTarget target, string pathToBuildProject)
    {
        if (target == BuildTarget.iOS)
        {
            #if UNITY_IOS

            // find and open info.plist
            string        plistPath = Path.Combine(pathToBuildProject, "Info.plist");
            PlistDocument plist     = new PlistDocument();
            plist.ReadFromString(File.ReadAllText(plistPath));
            PlistElementDict rootDict = plist.root;

            // 设置出口合规
            Debug.Log($"[iOS]设置出口合规到Info.plist");
            rootDict.SetBoolean("ITSAppUsesNonExemptEncryption", false);

            // 支持接收存档
            Debug.Log($"[iOS]设置DocumentTypes");
            var docTypes = rootDict.CreateArray("CFBundleDocumentTypes");
            var docType = docTypes.AddDict();
            docType.SetString("CFBundleTypeName", "Text");
			docType.SetString("LSHandlerRank", "Alternate");
			var contentTypes = docType.CreateArray("LSItemContentTypes");
			contentTypes.AddString("public.data");

			rootDict.SetBoolean("LSSupportsOpeningDocumentsInPlace", true);

			// GameAnalytics
			rootDict.SetString("NSUserTrackingUsageDescription", "请放心，开启权限不会获取您的隐私信息，该权限仅用于标识设备并收集App崩溃信息");

			// save
            File.WriteAllText(plistPath, plist.WriteToString());


			// Read.
			string projectPath = PBXProject.GetPBXProjectPath(pathToBuildProject);
			PBXProject project = new PBXProject();
			project.ReadFromString(File.ReadAllText(projectPath));
			string targetGUID = project.GetUnityFrameworkTargetGuid();

			// Write.
			File.WriteAllText(projectPath, project.WriteToString());

            #endif
        }

        else if (target == BuildTarget.Android)
        {
	        #if UNITY_ANDROID

	        Debug.Log($"path = {pathToBuildProject}");

	        #endif
        }
    }
}