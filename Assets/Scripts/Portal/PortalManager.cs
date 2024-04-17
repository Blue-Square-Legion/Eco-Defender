using System.Collections;
using System;
using UnityEngine;
using System.Threading.Tasks;

public class PortalManager : MonoBehaviour
{
    [SerializeField] private AnimatorController _DoorAnimator;
  
    private SceneManagerSO _currentScene = null;

    private bool _isBlocked = false;    //throtle open/close

    public async void Open(SceneManagerSO manager)
    {
        if (_isBlocked)
        {
            return;
        }

        _isBlocked = true;
        await AsyncOpen(manager);
        _isBlocked = false;
    }

    public async void Close()
    {
        if (_isBlocked)
        {
            return;
        }

        _isBlocked = true;
        await AsyncClose();
        _isBlocked = false;
    }


    //Async non-void to allow await
    private async Task<bool> AsyncOpen(SceneManagerSO manager)
    {
        if (_currentScene)
        {
            Debug.LogWarning("Scene is Already Loaded. Closing Previous then Opening.");
            await AsyncClose();
        }

        await manager.AsyncLoad();
        _currentScene = manager;
        _DoorAnimator.Open();

        await Task.Delay((int)(_DoorAnimator.ClipLength * 1000));
        return true;
    }
    
    private async Task<bool> AsyncClose()
    {
        if(_currentScene == null)
        {
            return false;
        }

        _DoorAnimator.Close();

        await Task.Delay((int)(_DoorAnimator.ClipLength * 1000));
        await _currentScene.AsyncUnload();

        _currentScene = null;
        return true;
    }


}
