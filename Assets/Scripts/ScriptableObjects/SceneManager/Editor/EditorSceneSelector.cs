using UnityEngine;
using UnityEditor;
#if UNITY_EDITOR
/// <summary>
/// Based on https://docs.unity3d.com/ScriptReference/SceneAsset.html
/// Allows Selection of Scene instead of manual typing path
/// </summary>
[CustomEditor(typeof(SceneManagerSO), true)]
public class EditorSceneSelector : Editor
{
    public override void OnInspectorGUI()
    {
        SceneManagerSO SceneManager = target as SceneManagerSO;
        SceneAsset oldScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(SceneManager.ScenePath);

        serializedObject.Update();

        EditorGUI.BeginChangeCheck();
        SceneAsset newScene = EditorGUILayout.ObjectField("Scene", oldScene, typeof(SceneAsset), false) as SceneAsset;

        if (EditorGUI.EndChangeCheck())
        {
            string newPath = AssetDatabase.GetAssetPath(newScene);

            SerializedProperty scenePathProperty = serializedObject.FindProperty("ScenePath");
            scenePathProperty.stringValue = newPath;
        }
        serializedObject.ApplyModifiedProperties();
    }
}
#endif