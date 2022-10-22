/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2022-02-15 10:20:03
 * @modify date 2022-02-15 10:20:03
 * @desc [description]
 */

using System.IO;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

namespace NewLife.Config.CustomJsonConverters
{
    public abstract class NewLifeJsonConverterBase<T> : JsonConverter<T>
    {
        // 查询指定资源的路径
        protected string GetAssetPath(Object value)
        {
            string path = Path.ChangeExtension(AssetDatabase.GetAssetPath(value), null);
            return path.Replace("Assets/", string.Empty);
        }
    }
}