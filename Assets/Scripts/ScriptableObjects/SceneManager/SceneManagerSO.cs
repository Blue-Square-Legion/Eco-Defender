using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

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

    public async Task<bool> AsyncLoad(LoadSceneMode type = LoadSceneMode.Additive)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(ScenePath, type);
        return await GenTask(operation, () => OnLoadComplete?.Invoke());
    }

    public async Task<bool> AsyncUnload()
    {
        AsyncOperation operation = SceneManager.UnloadSceneAsync(ScenePath);
        return await GenTask(operation, () => OnUnloadComplete?.Invoke());
    }

    private async Task<bool> GenTask(AsyncOperation operation, Action callback)
    {
        TaskCompletionSource<bool> tsc = new TaskCompletionSource<bool>();

        Action< AsyncOperation> handler = null;
        handler = (AsyncOperation _) => {
            callback();
            tsc.SetResult(true);
            operation.completed -= handler;
        };

        operation.completed += handler;
        return await tsc.Task;
    }


    public bool IsLoaded()
    {
        return SceneManager.GetSceneByPath(ScenePath).IsValid();
    }
}
