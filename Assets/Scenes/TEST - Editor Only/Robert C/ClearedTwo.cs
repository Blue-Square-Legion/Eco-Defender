using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.XR.Interaction.Toolkit.Utilities;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.VisualScripting;
using UnityEngine.Analytics;
public class ClearedTwo : MonoBehaviour
{
    //   public Light spotlight;
    bool _check1 = false;
    public Renderer cubeRenderer;
    bool isPaused;
    public GameObject Player;
    public TextMeshProUGUI Text;
    public GameObject Text1;
    public GameObject canvas;
    private Color newCubeColor;
    public GameObject cube;
    public Material material1, material2;
    public float FiveSec = 5;
    Vector3 newPosition;
   
    private ActivateEvent activated;
    // Start is called before the first frame updateD



    private void Start()
    {
        cubeRenderer = cube.GetComponent<Renderer>();
        // spotlight = GetComponent<Light>();
        Player = GameObject.FindGameObjectWithTag("Player");
        Text = GetComponent<TextMeshProUGUI>();
        canvas.SetActive(false);
      //  canvas.SetActive(true);
        //Text1.SetActive(true);
        Text1.SetActive(false);
    }



   public void OnTriggerEnter(Collider other)
       // public void OnCollisionEnter(Collision collision)
    {if (other.gameObject.CompareTag("Player"))
        {
            
        Text1.SetActive(true);
            canvas.SetActive(true);
            // spotlight.intensity = 100;
            newCubeColor = new Color(1f, 1f, 1f, 1f);
            cubeRenderer.material.color = material1.color;
            _check1 = false;
        }
      // newPosition = Player.transform.position;

      if (FiveSec > 0 && _check1)
        {
            PauseGame();
            FiveSec -= 1 * Time.deltaTime;
         //   Player.transform.position = newPosition;  
        }
     
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    private void Update()
    {

    }
}

