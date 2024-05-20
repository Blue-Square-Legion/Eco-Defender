using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class CharMove : MonoBehaviour
{
    public float speed = 2f;
    public GameObject Pen;
    private void Start()
    {
        Pen = GameObject.FindGameObjectWithTag("Pen");
    }
    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        movementDirection.Normalize();
        transform.Translate(movementDirection * speed * Time.deltaTime, Space.World);

        if (movementDirection == Vector3.zero)
        {
            return;
        }
        else
        {
            
            Pen.transform.right=(movementDirection);
        }
    }
}
