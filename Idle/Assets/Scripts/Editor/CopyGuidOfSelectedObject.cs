/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2020-03-04 11:03:39
 * @modify date 2020-03-04 11:03:39
 * @desc [description]
 */

using UnityEditor;

public class CopyGuidOfSelectedObject
{
    [MenuItem("Assets/HelloMeow/Copy Guid to clipboard")]
	public static void CopyGuid()
	{
		if (Selection.activeObject)
		{
			string path = AssetDatabase.GetAssetPath(Selection.activeObject);
			var guid = AssetDatabase.AssetPathToGUID(path);
			EditorGUIUtility.systemCopyBuffer = guid;
			HM.HMLog.LogDebug($"guid[{guid}]已拷贝到剪贴板|文件={path}");
		}
	}
}