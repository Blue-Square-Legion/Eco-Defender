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

    public abstract class GenericEventEditor<T> : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUI.enabled = Application.isPlaying;

            GenericEventChannelSO<T> e = target as GenericEventChannelSO<T>;
            if (GUILayout.Button("Raise"))
                e.Invoke(e._testData);
        }
    }
}