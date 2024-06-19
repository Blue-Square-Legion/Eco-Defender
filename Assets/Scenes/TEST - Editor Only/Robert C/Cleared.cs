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
public class Cleared : MonoBehaviour
{
    //   public Light spotlight;
    bool _check1 = false;
   
    bool isPaused;
    public GameObject player;
  public TextMeshProUGUI Text;
    public GameObject Text1;
    public GameObject canvas;
    public Renderer cubeRenderer;
    public GameObject cube;
    public Material material1;
    public float FiveSec = 5;
    Vector3 newPosition;
    private Color newCubeColor;
    private ActivateEvent activated;
    // Start is called before the first frame updateD



    private void Start()
    {
        // spotlight = GetComponent<Light>();
        player = GameObject.FindGameObjectWithTag("Player");
        Text = GetComponent<TextMeshProUGUI>();
        canvas.SetActive(false);
      Text1.SetActive(false);

    }



    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            //_check1 = true;
            canvas.SetActive(true);
            Text1.SetActive(true);
            // spotlight.intensity = 100;
            //  newCubeColor = new Color(1f, 1f, 1f, 1f);
            //  cubeRenderer.material.color = material1.color;

        }

        /*   newPosition = Player.transform.position;
         *   if(_check1){
           if (FiveSec > 0)
           {
               PauseGame();
               FiveSec -= 1 * Time.deltaTime;

           }
        }
        */
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

