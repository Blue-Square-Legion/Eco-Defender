using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PendulumR : MonoBehaviour
{
    public GameObject rope;
    public GameObject ball;
    float StartTime;
    public float speed = 1.5f;
    public float Limit = 35f;
    public bool restart = false;
    private float random = 0;
    void Awake()
    {

        random = UnityEngine.Random.Range(0f, 1f);
    }


    void Update()
    {
        StartTime += Time.deltaTime;
        float angle = Limit * Mathf.Sin(StartTime * random * speed);
        rope.transform.localRotation = Quaternion.Euler(angle, 0, 0);

    }
}

