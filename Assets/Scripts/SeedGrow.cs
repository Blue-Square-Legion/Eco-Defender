using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedGrow : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera camera;
    void FixedStart()
    {
        transform.LookAt(camera.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
