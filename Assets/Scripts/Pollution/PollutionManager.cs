using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PollutionManager : MonoBehaviour
{
    [SerializeField] List<ParticleTriggerManager> particleSystems = new List<ParticleTriggerManager>();
    [SerializeField, Min(0.01f), Tooltip("Rate (sec) OnPollutionValue is triggered")] float _updateRate = 0.1f;

    [Space(5)]
    public UnityEvent OnPollutionEnd;
    public UnityEvent<float> OnPollutionValue;

    public string stateGroupName = "MusicStates";
    public string stateName = "OutsideClean";


    
        

    //Pollution Particle Counts
    public float ParticleMaxCount { get; private set; }
    public float ParticleCount { get { return GetParticleCount(); } }
    public float PollutionPercent { get { return GetParticleCount() / ParticleMaxCount; } }

    //Active Particle System counts.
    public int ActiveCount { get; private set; }
    public int ActiveTotalCount { get { return particleSystems.Count; } }

    private void Start()
    {
        ParticleMaxCount = particleSystems.Aggregate<ParticleTriggerManager, float>(0, (acc, part) => acc + part.MaxParticleCount);
        ActiveCount = ActiveTotalCount;

        StartCoroutine(UpdatePollution());
    }

    private void OnEnable()
    {
        particleSystems.ForEach(part => part.OnParticleEnd.AddListener(HandleParticleEnd));
    }

    private void OnDisable()
    {
        particleSystems.ForEach(part => part.OnParticleEnd.RemoveListener(HandleParticleEnd));
    }

    private IEnumerator UpdatePollution()
    {
        while (ActiveCount > 0)
        {
            OnPollutionValue.Invoke(PollutionPercent);
            yield return new WaitForSeconds(_updateRate);
        }
        yield return null;
    }

    private void HandleParticleEnd()
    {
        ActiveCount--;
        if (ActiveCount <= 0)
        {
            AkSoundEngine.SetState(stateGroupName, stateName);
            Debug.Log("OutsideClean State");
            OnPollutionEnd.Invoke();
            enabled = false;
        }
    }

    public float GetParticleCount()
    {
        return particleSystems.Aggregate<ParticleTriggerManager, float>(0, (acc, part) => acc + part.ParticleCount);
    }
}
