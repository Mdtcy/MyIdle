/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2022-01-25 17:52:02
 * @modify date 2022-01-25 17:52:02
 * @desc [description]
 */

using System.IO;
using System.Text;
using HM;
using HM.EditorOnly;
using HM.GameBase;
using NewLife.Config.CustomJsonConverters;
using Newtonsoft.Json;

namespace NewLife.Config
{
    public class NewLifeConfigJsonExporter : ConfigJsonExporter
    {
        /// <inheritdoc />
        public override void ExportJson(BaseConfig config, string dir)
        {
            if (config == null)
            {
                HMLog.LogWarning("[NewLifeConfigJsonExporter]config is null, export json abort.");
            }

            var jsonString = JsonConvert.SerializeObject(config, settings);
            var filepath   = Path.Combine(dir, $"{config.name}.json");
            HMLog.LogDebug($"Export {config.name} json to path:{filepath}");
            File.WriteAllText(filepath, jsonString, Encoding.UTF8);
        }

        public NewLifeConfigJsonExporter()
        {
            settings = new JsonSerializerSettings {Formatting = Formatting.Indented};
            settings.Converters.Add(new UnionCommandConfigJsonConverter());
            settings.Converters.Add(new SpriteJsonConverter());
            settings.Converters.Add(new GameObjectJsonConverter());
            settings.Converters.Add(new AudioClipJsonConverter());
            settings.ContractResolver = new NewLifeContractResolver();
        }

        private readonly JsonSerializerSettings settings;
    }
}