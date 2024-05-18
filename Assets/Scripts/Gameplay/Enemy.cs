using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamageable
{
    private NavMeshAgent agent;

    [SerializeField] private float navTickTime = 0.5f;

    [SerializeField] private float _maxHealth = 2;

    [SerializeField] private List<string> playerTag = new() { "Player", "DamageCollider" };
    [SerializeField] private float damage = 2;

    private float _health;

    private Transform _target;
    private Vector3 _spawnPoint;

    private bool _returnToSpawn = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        _health = _maxHealth;

        _target = Camera.main?.transform;
        _spawnPoint = transform.position;
    }

    private void OnEnable()
    {
        StartCoroutine(UpdateDestination());
    }

    public void Damage(float damage = 1)
    {
        _health -= damage;

        if (_health <= 0) { Destroy(gameObject); }
    }


    public void ReturnToSpawn()
    {
        _returnToSpawn = true;
    }

    public void TargetPlayer()
    {
        _returnToSpawn = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (playerTag.Contains(other.tag))
        {
            print($"Enemy Trigger: {other.name}");
            NotifyDamage(other);
            Destroy(gameObject, 1f);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (playerTag.Contains(other.tag))
        {
            print($"Enemy Trigger Stay: {other.name}");

            NotifyDamage(other);
        }
    }

    private void NotifyDamage(Collider other)
    {
        if (other.TryGetComponent<IDamageable>(out IDamageable damagable))
        {
            damagable.Damage(damage);
        }
    }


    IEnumerator UpdateDestination()
    {
        agent.SetDestination(_returnToSpawn ? _spawnPoint : _target.position);
        yield return new WaitForSeconds(navTickTime);
        StartCoroutine(UpdateDestination());
    }

}
