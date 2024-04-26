using UnityEngine;

[CreateAssetMenu(fileName = "ParticleTracker", menuName = "Misc/ParitcleTracker")]
public class ParticleTriggerTracker : ScriptableObject
{
    public ParticleSystem ParticleSystem;

    public void AddCollider(Collider collider)
    {
        ParticleSystem.trigger.AddCollider(collider);
    }

    public void RemoveCollider(Collider collider)
    {
        ParticleSystem.trigger.RemoveCollider(collider);
    }
}

