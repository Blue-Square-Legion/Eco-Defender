using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;


public class PlayerControl : MonoBehaviour
{
    public Camera cam;
    public NavMeshAgent agent;
    public GameObject prefab;
    public GameObject rope;
    public GameObject ball;
    float StartTime;
    public float speed = 2f;
    public float Limit = 25f;
    public bool restart = false;
    private float random = 0;
    private RaycastHit hit;
    public LayerMask WhatIsMouse;
    bool EnemyInRange;

    void Awake()
    {

        random = UnityEngine.Random.Range(0f, 1f);
    }



    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);


            if (Physics.Raycast(ray, out hit))
            {
             
                    agent.SetDestination(transform.position);

                agent.SetDestination(hit.point); ;
            }

        }

        StartTime += Time.deltaTime;
        float angle = Limit * Mathf.Sin(StartTime * random * speed);
        rope.transform.localRotation = Quaternion.Euler(angle, hit.point.y, hit.point.z);
    }
    public void OnTriggerEnter(Collider prefab)
    {
        speed = 0;
    }
}