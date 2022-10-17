/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2020-05-04 21:05:04
 * @modify date 2020-05-04 21:05:04
 * @desc [description]
 */

using HM.Interface;

namespace HM.GameBase
{
    public class ConfigGetter : IConfigGetter
    {
        public T GetConfig<T>(int itemId) where T : BaseConfig
        {
            return configContainer.GetConfig<T>(itemId);
        }

        private readonly ConfigContainer configContainer;

        public ConfigGetter(ConfigContainer container)
        {
            configContainer = container;
        }
    }
}