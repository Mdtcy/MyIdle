/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2022-01-26 09:40:16
 * @modify date 2022-01-26 09:40:16
 * @desc [如果BaseConfig是成员变量，则只序列化为Id]
 */

using System.Reflection;
using HM.GameBase;
using NewLife.Config.CustomJsonConverters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace NewLife.Config
{
    public class NewLifeContractResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member,
                                        MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);

            if (property.PropertyType == typeof(BaseConfig) ||
                property.PropertyType.IsSubclassOf(typeof(BaseConfig)))
            {
                property.Converter = new ConfigMemberJsonConverter();
            }

            return property;
        }
    }
}