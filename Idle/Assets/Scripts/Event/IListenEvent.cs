/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2022年12月25日
 * @modify date 2022年12月25日
 * @desc [任务组UI]
 */

#pragma warning disable 0649
namespace Event
{
    public interface IListenEvent<T> where T : EEvent
    {
        public void Trigger(T t);
    }
}
#pragma warning restore 0649