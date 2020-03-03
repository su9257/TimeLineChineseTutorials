using System.ComponentModel;
using UnityEngine;
using UnityEngine.Timeline;

[DisplayName("自定义Marker/AnnotationMarker")]
[CustomStyle("AnnotationMarker")]
public class AnnotationMarker : Marker
{
    [TextArea] public string annotation;
}
