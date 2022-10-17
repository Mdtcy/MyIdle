/**
 * @author [jie.wen]
 * @email [jie.wen@hellomeowlab.com]
 * @create date 2020-06-15 23:06:57
 * @modify date 2020-06-15 23:06:57
 * @desc [description]
 */

using System;

namespace HM.Notification
{
    public interface ISendNotification
    {
        void Publish(int key, object data = null, Action<bool> onFinish = null);

        void Publish(int key, long data, Action<bool> onFinish = null);
    }
}