using System;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Load/Unloads Async levels.
/// Requires Scene to be in Build project setting or Loaded in Scene as Subscene.
/// </summary>
[CreateAssetMenu(fileName ="SceneLoader", menuName ="Misc/SceneLoader")]
public class SceneManagerSO : ScriptableObject
{
    //Scene object > path (Set via Editor Script)
    public string ScenePath;

    public Action OnLoadComplete, OnUnloadComplete;

    public void AsyncLoad()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(ScenePath, LoadSceneMode.Additive);
        operation.completed += (AsyncOperation _) => OnLoadComplete?.Invoke();
    }

    public void AsyncUnload()
    {
        AsyncOperation operation = SceneManager.UnloadSceneAsync(ScenePath);
        operation.completed += (AsyncOperation _) => OnUnloadComplete?.Invoke();
    }

    public bool IsLoaded()
    {
        return SceneManager.GetSceneByPath(ScenePath).IsValid();
    }
}
