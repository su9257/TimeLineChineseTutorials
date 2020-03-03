using UnityEngine;
using UnityEngine.Playables;

class NotificationReceiver: MonoBehaviour, INotificationReceiver
{
    public void OnNotify(Playable origin, INotification notification, object context)
    {
        if (notification != null)
        {
            double time = origin.IsValid() ? origin.GetTime() : 0.0;
            Debug.LogFormat("接受一个通知，类型为 {0} 时间为 {1}", notification.GetType(), time);
        }
    }
}
