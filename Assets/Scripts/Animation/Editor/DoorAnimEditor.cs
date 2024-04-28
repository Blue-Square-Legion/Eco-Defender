using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DevEditors
{
#if UNITY_EDITOR
    [CustomEditor(typeof(DoorAnimatorController), editorForChildClasses: true)]
    public class DoorAnimEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUI.enabled = Application.isPlaying;

            DoorAnimatorController e = target as DoorAnimatorController;
            if (GUILayout.Button("Open"))
                e.Open();

            if (GUILayout.Button("Close"))
                e.Close();
        }
    }
#endif
}