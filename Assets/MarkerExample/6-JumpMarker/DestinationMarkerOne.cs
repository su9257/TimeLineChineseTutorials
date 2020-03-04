using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.ComponentModel;
using UnityEngine.Timeline;

[DisplayName("Jump/DestinationMarkerOne")]
//[CustomStyle("DestinationMarker")]
public class DestinationMarkerOne : Marker
{
    [SerializeField] public bool active;

    void Reset()
    {
        active = true;
    }
}
