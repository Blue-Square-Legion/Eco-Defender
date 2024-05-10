using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Move1 : MonoBehaviour
{

    public float moveSpeed = 1;

    public GameObject Cube;
    private void Start()
    {
        Cube = GameObject.FindGameObjectWithTag("Cube");
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
         Cube.transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Cube.transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Cube.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            Cube.transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
        }
    }
}