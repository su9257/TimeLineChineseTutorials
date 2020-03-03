using UnityEngine;
using UnityEngine.Playables;

class ReceiverExample : INotificationReceiver
{
    public void OnNotify(Playable origin, INotification notification, object context)
    {
        if (notification != null)
        {
            MyNotification myNotification = notification as MyNotification;
            string content = myNotification!=null?myNotification.messageContent:"空";
            double time = origin.IsValid() ? origin.GetTime() : 0.0;
            Debug.Log($"接受通知的类型为 {notification.GetType()} 对应的事件为： {time} 对应的消息为：{content}");
        }
    }
}
