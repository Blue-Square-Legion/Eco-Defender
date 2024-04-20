using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class LessPollution : MonoBehaviour
{

    public ParticleSystem smog;
    float curTime = 5;
    private void OnParticleCollision(GameObject SMOG)
    {
   
            if (SMOG.CompareTag("Plant")&& curTime <=0) ;
        {
         

                SMOG.GetComponent<ParticleSystem>().Stop();
        
        }
        curTime -= Time.deltaTime;

    }
}