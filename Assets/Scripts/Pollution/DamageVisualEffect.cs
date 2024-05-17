using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Uses Locomotion Provider to Connect to Tunneling Vignette.
/// Configure Visuals on Tunneling Vigneete.
/// </summary>
public class DamageVisualEffect : LocomotionProvider, IParticleTrigger, IDamagable
{
    [SerializeField] private float _debounceTime = 0.5f;
    public UnityEvent OnDamageStart, OnDamageEnd;

    private Debounce _debounce;

    protected override void Awake()
    {
        _debounce = new(_debounceTime);

        _debounce.OnStart += Damage_Start;
        _debounce.OnEnd += Damage_End;
    }

    private void Damage_Start()
    {
        locomotionPhase = LocomotionPhase.Moving;
        OnDamageStart.Invoke();
    }

    private void Damage_End()
    {
        locomotionPhase = LocomotionPhase.Done;
        OnDamageEnd.Invoke();
    }

    public void OnParticleEnter()
    {
        _debounce.Start();
    }

    public void Damage(float damage)
    {
        _debounce.Start();
    }
}