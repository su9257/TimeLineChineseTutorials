using UnityEngine;
using UnityEngine.Playables;

public class JumpReceiver : MonoBehaviour, INotificationReceiver
{
    public void OnNotify(Playable origin, INotification notification, object context)
    {
        var jumpMarker = notification as JumpMarker;
        if (jumpMarker == null) return;

        var destinationMarker = jumpMarker.destinationMarker;
        if (destinationMarker != null && destinationMarker.active)
        {

            var timelinePlayable = origin.GetGraph().GetRootPlayable(0);
            var timelinePlayable1 = origin.GetGraph().GetRootPlayable(1);
            Debug.Log(origin.GetGraph().GetRootPlayableCount());
            Debug.Log(origin.GetGraph().GetPlayableCount());
            timelinePlayable.SetTime(destinationMarker.time);
        }
    }
}

