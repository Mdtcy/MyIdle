/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2022-01-25 17:33:23
 * @modify date 2022-01-25 17:33:23
 * @desc [description]
 */

using System;
using Newtonsoft.Json;
using UnityEngine.AddressableAssets;

namespace NewLife.Config.CustomJsonConverters
{
    public class AssetReferenceJsonConverter : NewLifeJsonConverterBase<AssetReference>
    {
        public override void WriteJson(JsonWriter writer, AssetReference value, JsonSerializer serializer)
        {
            if (value != null && value.RuntimeKeyIsValid())
            {
                writer.WriteValue(GetAssetPath(value.editorAsset));
            }
            else
            {
                writer.WriteValue("");
            }
        }

        public override AssetReference ReadJson(JsonReader     reader,
                                       Type           objectType,
                                       AssetReference          existingValue,
                                       bool           hasExistingValue,
                                       JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}