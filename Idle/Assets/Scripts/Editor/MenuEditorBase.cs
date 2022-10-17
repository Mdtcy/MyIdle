/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2022-04-22 10:43:14
 * @modify date 2022-04-22 10:43:14
 * @desc [description]
 */

using System.IO;
using UnityEditor;

#pragma warning disable 0649
namespace NewLife.Editor
{

    public enum SelectionType
    {
        None = 0,
        File = 1,
        Dir  = 2,
    }

    public class MenuEditorBase
    {
        protected static bool AnySelected(out SelectionType type, out string path)
        {
            type = SelectionType.None;
            path = AssetDatabase.GetAssetPath(Selection.activeObject);

            // 选中了一个文件
            if (File.Exists(path))
            {
                type = SelectionType.File;

                return true;
            }

            // 选中了一个目录
            if (Directory.Exists(path))
            {
                type = SelectionType.Dir;

                return true;
            }

            // 没有选中任何
            return false;
        }
    }
}
#pragma warning restore 0649