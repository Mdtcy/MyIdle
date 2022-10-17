/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2020-06-19 23:06:19
 * @modify date 2020-06-19 23:06:19
 * @desc [description]
 */

namespace HM.Notification
{
    public interface IReceiveNotification
    {
        void Subscribe(int key,
                       NotificationCenter.OnNotification callback,
                       NotificationCenter.CallbackOccurrence callbackNumber = NotificationCenter.CallbackOccurrence.CE_UNIQUE_ONLY);

        void Unsubscribe(int key, NotificationCenter.OnNotification callback);
    }
}