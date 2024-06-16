using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class Cleared : MonoBehaviour
{ 
 //   public Light spotlight;
    bool _check1 = false;
    public Renderer cubeRenderer;
   
    public GameObject Player;
    public TextMeshProUGUI Text;
    public GameObject Text1;
    public GameObject canvas;
    private Color newCubeColor;
    public GameObject cube;
    public Material material1, material2;
    // Start is called before the first frame updateD
    private void Start()
    {
        cubeRenderer = cube.GetComponent<Renderer>();
       // spotlight = GetComponent<Light>();
        Player = GameObject.FindGameObjectWithTag("Player");
        Text= GetComponent<TextMeshProUGUI>();
        canvas.SetActive(false);
      
    }

    public void OnTriggerEnter(Collider other)
    {
        canvas.SetActive(true);
        // spotlight.intensity = 100;
        newCubeColor = new Color(1f, 1f, 1f, 1f);
        cubeRenderer.material.color = material1.color;

    }



    // Update is called once per frame
    private void Update()     
    {

      
    }
   
}   
