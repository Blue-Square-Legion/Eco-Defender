using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsidePolutedState : MonoBehaviour
{
    public string stateGroupName = "MusicStates"; 
    public string stateName = "OutsidePoluted";



    // Function to set the state in Wwise
    public void SetOutsidePolutedState()
    {    

        // Set the state in Wwise
        AkSoundEngine.SetState(stateGroupName, stateName);
        Debug.Log("OutsidePoluted State");
    }

}
