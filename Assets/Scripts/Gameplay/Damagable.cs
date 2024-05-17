using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damagable : MonoBehaviour, IDamagable
{
    [SerializeField] private float maxHP = 10;

    [Header("Damage CD")]
    [SerializeField] private float invulnTime = 0.5f;

    [Header("Regen")]
    [SerializeField] private bool canRegen = true;
    [SerializeField, Tooltip("How much healed per Regen tick")] private float regenAmount = 0.5f;
    [SerializeField, Tooltip("Regen tick time in Sec")] private float regenTime = 1f;

    [Header("Events")]
    public UnityEvent<float> OnDamaged;
    public UnityEvent<float> OnHPChangePercent;
    public UnityEvent OnDeath;

    public float HP { get; protected set; }

    public bool isDamagable = true, isDead = false;

    private void Awake()
    {
        HP = maxHP;

        StartCoroutine(RegenHealth());
    }

    IEnumerator RegenHealth()
    {
        while (canRegen && !isDead)
        {
            Heal(regenAmount);
            yield return new WaitForSeconds(regenTime);
        }
    }

    IEnumerator InvulnTimer()
    {
        isDamagable = false;
        yield return new WaitForSeconds(invulnTime);
        isDamagable = true;
    }

    public void Damage(float damage)
    {
        if (!isDamagable || isDead) return;

        OnDamaged.Invoke(damage);
        HP = Mathf.Max(HP - damage, 0);

        OnHPChangePercent.Invoke(HP / maxHP);
        StartCoroutine(InvulnTimer());

        if (HP < 0)
        {
            OnDeath.Invoke();
            isDead = true;
        }
    }

    public void Heal(float heal)
    {
        if (isDead) return;

        HP += heal;

        if (HP > maxHP)
        {
            HP = maxHP;
        }
        else
        {
            OnHPChangePercent.Invoke(HP / maxHP);
        }
    }

    public void Reset()
    {
        HP = maxHP;
        isDamagable = true;
        isDead = false;
        enabled = true;
    }
}
