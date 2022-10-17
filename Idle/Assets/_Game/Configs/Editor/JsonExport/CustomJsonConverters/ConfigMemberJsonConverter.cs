/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2022-01-25 17:33:23
 * @modify date 2022-01-25 17:33:23
 * @desc [description]
 */

using System;
using HM.GameBase;
using Newtonsoft.Json;

namespace NewLife.Config.CustomJsonConverters
{
    public class ConfigMemberJsonConverter : NewLifeJsonConverterBase<BaseConfig>
    {
        public override void WriteJson(JsonWriter writer, BaseConfig value, JsonSerializer serializer)
        {
            writer.WriteValue(value == null ? "" : GetAssetPath(value));
        }

        public override BaseConfig ReadJson(JsonReader     reader,
                                            Type           objectType,
                                            BaseConfig     existingValue,
                                            bool           hasExistingValue,
                                            JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}