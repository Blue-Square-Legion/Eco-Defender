using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class WaterGun : MonoBehaviour
{
    public ParticleSystem waterStream;
    public int waterLevel = 100;
    
    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
    
    // Start is called before the first frame update
    void Start()
    {
        if (waterStream == null)
        {
            waterStream.Stop();
            Debug.LogError("Particle System not assigned.");
        }
        
        var emission = waterStream.emission;
        emission.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        SprayGun();
    }
    
    public void SprayGun()
    {
        if(waterLevel > 0)
        {
            var emission = waterStream.emission;
            emission.enabled = true;
            waterLevel--;
        }
        else
        {
            var emission = waterStream.emission;
            emission.enabled = false;
        }
    }
    
    void OnParticleCollision(GameObject other){
        int numCollisionEvents = waterStream.GetCollisionEvents(other, collisionEvents);
        Debug.Log("Colliding with " + other.name + " - Collision events: " + numCollisionEvents);
    }
}
