using System.ComponentModel;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[DisplayName("自定义Marker/NotificationMarker")]
public class NotificationMarker : Marker, INotification
{
    public PropertyName id { get; }
}
