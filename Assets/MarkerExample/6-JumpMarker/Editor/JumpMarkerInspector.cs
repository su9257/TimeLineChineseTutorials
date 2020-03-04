using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.Timeline;

[CustomEditor(typeof(JumpMarker))]
public class JumpMarkerInspector : Editor
{
    const string k_TimeLabel = "将要跳转到的时间为 {0}";
    const string k_NoJumpLabel = "{0} 没有激活.";
    const string k_AddMarker = "没有找到任何跳转目标的Marker";
    const string k_JumpTo = "跳转到";
    const string k_None = "啥都没有None";

    SerializedProperty m_DestinationMarker;
    SerializedProperty m_EmitOnce;
    SerializedProperty m_EmitInEditor;
    SerializedProperty m_Time;

    void OnEnable()
    {
        Debug.Log("OnEnable");
        m_DestinationMarker = serializedObject.FindProperty("destinationMarker");
        m_EmitOnce = serializedObject.FindProperty("emitOnce");
        m_EmitInEditor = serializedObject.FindProperty("emitInEditor");
        m_Time = serializedObject.FindProperty("m_Time");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        var marker = target as JumpMarker;
        
        using (var changeScope = new EditorGUI.ChangeCheckScope())
        {
            EditorGUILayout.PropertyField(m_Time);
            EditorGUILayout.Space();
            
            var destinationMarkers = DestinationMarkersFor(marker);
            if (!destinationMarkers.Any())
                DrawNoJump();
            else
                DrawJumpOptions(destinationMarkers);

            if (changeScope.changed)
            {
                serializedObject.ApplyModifiedProperties();
                TimelineEditor.Refresh(RefreshReason.ContentsModified);
            }
        }
    }

    void DrawNoJump()
    {
        EditorGUILayout.HelpBox(k_AddMarker, MessageType.Info);
        
        using (new EditorGUI.DisabledScope(true))
        {
            EditorGUILayout.Popup(k_JumpTo, 0, new[]{ k_None });
            EditorGUILayout.PropertyField(m_EmitOnce);
            EditorGUILayout.PropertyField(m_EmitInEditor);
        }
    }
    
    void DrawJumpOptions(IList<DestinationMarker> destinationMarkers)
    {
        var destinationMarker = DrawDestinationPopup(destinationMarkers);
        DrawTimeLabel(destinationMarker);
        EditorGUILayout.PropertyField(m_EmitOnce);
        EditorGUILayout.PropertyField(m_EmitInEditor);
    }

    DestinationMarker DrawDestinationPopup(IList<DestinationMarker> destinationMarkers)
    {
        var popupIndex = 0;
        var destinationMarkerIndex = destinationMarkers.IndexOf(m_DestinationMarker.objectReferenceValue as DestinationMarker);
        if (destinationMarkerIndex != -1)
            popupIndex = destinationMarkerIndex + 1;

        DestinationMarker destinationMarker = null;
        using (var changeScope = new EditorGUI.ChangeCheckScope())
        {
            var newIndex = EditorGUILayout.Popup(k_JumpTo, popupIndex, GeneratePopupOptions(destinationMarkers).ToArray());
            
            if (newIndex > 0)
                //destinationMarker = destinationMarkers.ElementAt(newIndex - 1);
                destinationMarker = destinationMarkers[newIndex - 1];

            if (changeScope.changed)
                m_DestinationMarker.objectReferenceValue = destinationMarker;
        }
        
        return destinationMarker;
    }

    static void DrawTimeLabel(DestinationMarker destinationMarker)
    {
        if (destinationMarker != null)
        {
            if (destinationMarker.active)
                EditorGUILayout.HelpBox(string.Format(k_TimeLabel, destinationMarker.time.ToString("0.##")), MessageType.Info);
            else
                EditorGUILayout.HelpBox(string.Format(k_NoJumpLabel, destinationMarker.name), MessageType.Warning);
        }
    } 
    
    static IList<DestinationMarker> DestinationMarkersFor(Marker marker)
    {
        var destinationMarkers = new List<DestinationMarker>();
        var parent = marker.parent;
        if (parent != null)
            destinationMarkers.AddRange(parent.GetMarkers().OfType<DestinationMarker>().ToList());

        return destinationMarkers;
    }

    static IEnumerable<string> GeneratePopupOptions(IEnumerable<DestinationMarker> markers)
    {
        yield return k_None;

        foreach (var marker in markers)
        {
            yield return marker.name;
        }
    }
}