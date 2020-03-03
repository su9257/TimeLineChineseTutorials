
using UnityEngine;
using UnityEngine.Playables;

public class MyNotification : INotification
 {
    public string messageContent;
    public MyNotification(string message = null)
    {
        messageContent = message;
    }
     public PropertyName id { get; }
 }