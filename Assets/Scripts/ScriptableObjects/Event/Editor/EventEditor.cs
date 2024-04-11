using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace EventSO
{
    [CustomEditor(typeof(EventSO), editorForChildClasses: true)]
    public class EventEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUI.enabled = Application.isPlaying;

            EventSO e = target as EventSO;
            if (GUILayout.Button("Raise"))
                e.Invoke();
        }
    }
}