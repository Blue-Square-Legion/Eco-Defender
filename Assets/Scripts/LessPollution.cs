using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
//robert chaney

public class LessPollution : MonoBehaviour
{
    public GameObject Plant;
    public ParticleSystem S;
    public float curTime = 0;
    private void OnParticleCollision(GameObject S)
    {
   
            if (Plant.CompareTag("Plant")&& curTime >9) 
        {
         

                S.GetComponent<ParticleSystem>().Stop();
        
        }
        

    }
    private void Update()
    {
        curTime += Time.deltaTime;
    }
}