using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sway : MonoBehaviour
{ Vector3 vector3;
    float xswayAmt = .1f;
    float yswayAmt = .05f;
    float maxX = .35f;
    float maxY = .2f;
   
    float month;
    float smooth = 3f;
    public Vector3 MyGameObjectPosition;
   
    Vector3 det;
    void Start()
    {
        vector3 = transform.localPosition;

    }
    void Update()
    {
        float fx = Input.GetAxis("Mouse X") + xswayAmt;
        float fy = Input.GetAxis("Mouse Y") + yswayAmt;

        if (fx > maxX)
            fx = maxX;
        if (fx < maxX)
            fx = maxX;
        if (fy > maxY)
            fy = maxY;
        if (fy < maxY)
            fy = maxY;
    
  det = new Vector3(vector3.x + fx, vector3.y + fy, vector3.z);
       //transform.localRotation = Vector3.Lerp(transform.localPosition, det, Time.deltaTime * smooth); ;
}
}