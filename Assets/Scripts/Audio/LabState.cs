using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabState : MonoBehaviour
{
    public string stateGroupName = "MusicStates";
    public string stateName = "Lab"; // 

    // Function to set the state in Wwise
    public void SetLabState()
    {

        // Set the state in Wwise
        AkSoundEngine.SetState(stateGroupName, stateName);
        Debug.Log("Lab State");
    }
}
