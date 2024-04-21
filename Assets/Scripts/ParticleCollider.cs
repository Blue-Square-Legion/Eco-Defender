using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollider : MonoBehaviour
{ GameObject obj = null;
    public ParticleSystem m_System;
    private void OnParticleCollision(GameObject other)
    {
        List<ParticleCollisionEvent> events;
        events = new List<ParticleCollisionEvent>();

        ParticleSystem m_System = other.GetComponent<ParticleSystem>();

        ParticleSystem.Particle[] m_Particles;
        m_Particles = new ParticleSystem.Particle[m_System.main.maxParticles];

        ParticlePhysicsExtensions.GetCollisionEvents(other.GetComponent<ParticleSystem>(), gameObject, events);
        foreach (ParticleCollisionEvent coll in events)
        {

            if (coll.intersection != Vector3.zero)
            {
                int numParticlesAlive = m_System.GetParticles(m_Particles);

                // Check only the particles that are alive
                for (int i = 0; i < numParticlesAlive; i++)
                {

                    //If the collision was close enough to the particle position, destroy it
                    if (Vector3.Magnitude(m_Particles[i].position - coll.intersection) < 0.05f)
                    {
                    m_Particles[i].Equals( obj); //Kills the particle
                      //  m_System.SetParticles(m_Particles); // Update particle system
                        break;
                    }
                }
            }
        }
    }
}