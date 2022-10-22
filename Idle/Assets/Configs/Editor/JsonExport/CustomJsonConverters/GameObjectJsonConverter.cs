/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2022-01-25 17:33:23
 * @modify date 2022-01-25 17:33:23
 * @desc [description]
 */

using System;
using Newtonsoft.Json;
using UnityEngine;

namespace NewLife.Config.CustomJsonConverters
{
    public class GameObjectJsonConverter : NewLifeJsonConverterBase<GameObject>
    {
        public override void WriteJson(JsonWriter writer, GameObject value, JsonSerializer serializer)
        {
            writer.WriteValue(value == null ? "" : GetAssetPath(value));
        }

        public override GameObject ReadJson(JsonReader     reader,
                                            Type           objectType,
                                            GameObject     existingValue,
                                            bool           hasExistingValue,
                                            JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}