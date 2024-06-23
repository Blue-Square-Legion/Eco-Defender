using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public Transform targetObject;//cube
    public GameObject view;
    //bool lookAt=false;
    private float smoothFactor = .5f;
    Vector3 cameraOffset;
    void Start()
    {
        cameraOffset = view.transform.position - targetObject.transform.position;
    }


    void LateUpdate()
    {
        Vector3 newPosition = targetObject.transform.position + cameraOffset;
        transform.position = Vector3.Lerp(transform.position, newPosition, smoothFactor);

        //if(lookAt)
        view.transform.LookAt(targetObject);


    }
    //player target camera follows

}