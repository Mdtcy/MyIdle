/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2020-06-23 10:06:57
 * @modify date 2020-06-23 10:06:57
 * @desc [description]
 */

namespace NewLife.BusinessLogic.UniqueId
{
    public class UniqueIdGenerator : IIdGenerator<int>
    {
        [ES3Serializable]
        private static int counter = 801000000;

        public int Generate()
        {
            return ++counter;
        }
    }
}