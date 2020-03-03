using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class ScheduledNotification : MonoBehaviour
{
    PlayableGraph m_Graph;
    ReceiverExample m_Receiver;

    void Start()
    {
        m_Graph = PlayableGraph.Create("NotificationGraph");
        var output = ScriptPlayableOutput.Create(m_Graph, "NotificationOutput");

        //创建一个消息接收器
        m_Receiver = new ReceiverExample();
        //添加消息接收器到output
        output.AddNotificationReceiver(m_Receiver);

        //创建 TimeNotificationBehaviour
        var timeNotificationPlayable = ScriptPlayable<TimeNotificationBehaviour>.Create(m_Graph);
        output.SetSourcePlayable(timeNotificationPlayable);

        //添加对应的触发事件
        var notificationBehaviour = timeNotificationPlayable.GetBehaviour();
        notificationBehaviour.AddNotification(2.0, new MyNotification("菜鸟海澜"));

        m_Graph.Play();
    }

    void OnDestroy()
    {
        m_Graph.Destroy();
    }
}
