using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ManualNotification : MonoBehaviour
{
    PlayableGraph m_Graph;
    ReceiverExample m_Receiver;
    void Start()
    {
        m_Graph = PlayableGraph.Create("NotificationGraph");
        var output = ScriptPlayableOutput.Create(m_Graph, "NotificationOutput");

        //创建一个通知接收器
        m_Receiver = new ReceiverExample();
        output.AddNotificationReceiver(m_Receiver);

        //推送一个通知到这个Output
        output.PushNotification(Playable.Null, new MyNotification("菜鸟海澜"));

        m_Graph.Play();
    }

    void OnDestroy()
    {
        m_Graph.Destroy();
    }
}
