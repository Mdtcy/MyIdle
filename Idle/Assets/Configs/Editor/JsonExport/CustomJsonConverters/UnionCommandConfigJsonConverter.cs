/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2022-01-25 17:33:23
 * @modify date 2022-01-25 17:33:23
 * @desc [description]
 */

using System;
using NewLife.Defined.Condition;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NewLife.Config.CustomJsonConverters
{
    public class UnionCommandConfigJsonConverter : NewLifeJsonConverterBase<UnionCommandConfig>
    {
        public override void WriteJson(JsonWriter writer, UnionCommandConfig value, JsonSerializer serializer)
        {
            var data = new JObject
            {
                ["CommandType"]   = (int) value.CommandType,
                ["Target"]        = value.Target == null ? "" : GetAssetPath(value.Target),
                ["Num"]           = value.Num,
            };

            data.WriteTo(writer);
        }

        public override UnionCommandConfig ReadJson(JsonReader         reader,
                                                    Type               objectType,
                                                    UnionCommandConfig existingValue,
                                                    bool               hasExistingValue,
                                                    JsonSerializer     serializer)
        {
            throw new NotImplementedException();
        }
    }
}