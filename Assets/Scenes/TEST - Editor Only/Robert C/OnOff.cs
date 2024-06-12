using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

public class OnOff : MonoBehaviour    
{
   
    public GameObject Cube;
    public GameObject Sphere;
    // Start is called before the first frame update
    void Start()
    {
        
        Cube.SetActive(true);
        Sphere.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit  hitInfo;
            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity))
                Activate();

        }
    }
  public void  Activate()
    {

     
        Cube.SetActive(false);
        Sphere.SetActive(true);
    }
}