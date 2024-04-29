using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateCount : MonoBehaviour
{
    //1 = info
    //2 = OutsidePolluted
    //3 = Lab
    //4 = OutsideClean
    public int stateCount = 0;
    // Start is called before the first frame update
    void Start()
    {
       
        Debug.Log("stateCount = " + stateCount);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
