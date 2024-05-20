using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Pendulum : MonoBehaviour


{
    public GameObject cylinder;
    Quaternion startT, endT;
    [SerializeField, Range(0.0f, 5.0f)]
    float angle = 90f;
[SerializeField, Range(0.0f, 5.0f)]
    float speed = 2f;

    [SerializeField, Range(0.0f, 5.0f)]
    float startTime = 0.0f;

void Start()
    {
        startT = pendulumrotation(angle);
        endT = pendulumrotation(angle);
    }
    void FixedUpdate()
    {
        startTime += Time.deltaTime;
        cylinder.transform.rotation = Quaternion.Lerp(startT, endT, (Mathf.Sin(startTime * speed + Mathf.PI / 2) + 1f) / 2f);
    }


    void resettime()
    {
        startTime = 0f;
    }

    Quaternion pendulumrotation(float angle)
    {
        var pendulumrotation = transform.rotation;
        var angleZ = pendulumrotation.eulerAngles.z - angle;
        if (angleZ > 100)
            angleZ += 100;
        pendulumrotation.eulerAngles = new Vector3(pendulumrotation.eulerAngles.x, pendulumrotation.eulerAngles.z, angleZ);
        return pendulumrotation;


    }
}






