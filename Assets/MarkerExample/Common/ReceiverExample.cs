using UnityEngine;
using UnityEngine.Playables;

class ReceiverExample : INotificationReceiver
{
    public void OnNotify(Playable origin, INotification notification, object context)
    {
        if (notification != null)
        {
            MyNotification myNotification = notification as MyNotification;
            string content = myNotification!=null?myNotification.messageContent:"��";
            double time = origin.IsValid() ? origin.GetTime() : 0.0;
            Debug.Log($"����֪ͨ������Ϊ {notification.GetType()} ��Ӧ���¼�Ϊ�� {time} ��Ӧ����ϢΪ��{content}");
        }
    }
}
