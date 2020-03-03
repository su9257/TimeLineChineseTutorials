using UnityEngine;
using UnityEngine.Playables;

class NotificationReceiver: MonoBehaviour, INotificationReceiver
{
    public void OnNotify(Playable origin, INotification notification, object context)
    {
        if (notification != null)
        {
            double time = origin.IsValid() ? origin.GetTime() : 0.0;
            Debug.LogFormat("����һ��֪ͨ������Ϊ {0} ʱ��Ϊ {1}", notification.GetType(), time);
        }
    }
}
