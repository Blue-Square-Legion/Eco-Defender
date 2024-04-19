using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedGrow : MonoBehaviour
{
    // Start is called before the first frame update
    //public Camera camera;
    public float time = 0f;
    float finalScale = 2.0f;
    void FixedStart()
    {
        //transform.LookAt(camera.transform);
    }

    // Update is called once per frame
    void Update()
    {
        //Time.deltaTime 
        if(time < 3)
         {
            //transform.localScale = new Vector3(1.5f,1.5f,1.5f);
            //transform.
            var newScale = Mathf.Lerp(1.0f, finalScale, time);
            transform.localScale = new Vector3(newScale, 1, newScale);
            time += Time.deltaTime;
          }
        
    }
}
