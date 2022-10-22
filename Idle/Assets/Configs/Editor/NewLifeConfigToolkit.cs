/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2021-05-13 17:05:11
 * @modify date 2021-05-13 17:05:11
 * @desc [description]
 */

using HM.EditorOnly;
using HM.EditorOnly.TypeParser;

namespace NewLife.Config
{
    public class NewLifeConfigToolkit : ConfigToolkit
    {
        /// <inheritdoc />
        protected override string GetConfigFullTypeName(string typename)
        {
            return $"NewLife.Config.{typename}, NewLife.Configs";
        }

        public NewLifeConfigToolkit(HelloMeowTypeSerializer serializer) : base(serializer)
        {
        }
    }
}