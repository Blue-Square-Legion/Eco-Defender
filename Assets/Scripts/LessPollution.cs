using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LessPollution : MonoBehaviour
{ public ParticleSystem partSys;
    public ParticleSystem smog;
    private float currentTime =5f;
    // Start is called before the first frame update
    private void OnCollisionEnter3D(Collider plant)
    {
        if (currentTime <= 5)
        {
            currentTime -= Time.deltaTime;
            smog.startLifetime = .1f;
            smog.Stop();
        }
    }
   
}
