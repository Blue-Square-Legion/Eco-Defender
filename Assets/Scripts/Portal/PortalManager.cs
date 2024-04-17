using System.Collections;
using System;
using UnityEngine;
using System.Threading.Tasks;

public class PortalManager : MonoBehaviour
{
    [SerializeField] private AnimatorController _DoorAnim;
  
    private SceneManagerSO _currentScene = null;

    public async void Open(SceneManagerSO manager)
    {
        if (_currentScene)
        {
            Debug.LogWarning("Scene is Already Loaded");
            await AsyncClose();
        }

        await manager.AsyncLoad();
        _currentScene = manager;
        _DoorAnim.Open();
    }

    public async void Close()
    {
        await AsyncClose();
    }

    //Async non-void to allow await 
    private async Task<bool> AsyncClose()
    {
        if(_currentScene == null)
        {
            return false;
        }

        _DoorAnim.Close();

        await Task.Delay((int)(_DoorAnim.ClipLength * 1000));
        await _currentScene.AsyncUnload();

        _currentScene = null;
        return true;
    }


}
