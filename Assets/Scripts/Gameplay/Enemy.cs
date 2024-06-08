using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
public class Enemy : MonoBehaviour, IDamageable
{
    public enum AIStates
    {
        Passive,
        Aggressive
    }

    [SerializeField] private float navTickTime = 0.5f;

    [SerializeField] private float _maxHealth = 2;
    [SerializeField] private float damage = 2;

    [SerializeField] private List<string> playerTag = new() { "Player", "DamageCollider" };

    [SerializeField] private AIStates currState = AIStates.Aggressive;

    public UnityEvent OnMoveStart, OnMoveEnd, OnDeath;
    public UnityEvent<float> OnDamaged;

    private float _health;

    private Transform _target;
    private Vector3 _spawnPoint;
    private NavMeshAgent agent;
    private Vector3 destination;

    private bool _wasMoving = false;

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

        if (_health <= 0)
        {
            OnDeath.Invoke();
            agent.ResetPath();
            ChangeState(AIStates.Passive);
        }
        else
        {
            OnDamaged.Invoke(_health / _maxHealth);
        }
    }


    public void ReturnToSpawn()
    {
        ChangeState(AIStates.Passive);
    }

    public void TargetPlayer()
    {
        if(_health > 0)
        {
            ChangeState(AIStates.Aggressive);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (playerTag.Contains(other.tag))
        {
            print($"Enemy Trigger: {other.name}");
            NotifyDamage(other);
            //Destroy(gameObject, 1f);  //Originally destroyed self on contact to prevent pinning player.
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
        switch (currState)
        {
            case AIStates.Passive:
                // Add wander code here
                if (HasReachedDestination())
                {
                    var randomDirection = UnityEngine.Random.insideUnitSphere * 10;
                    randomDirection += _spawnPoint;

                    NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, 10, 1);
                    var finalPosition = hit.position;

                    //agent.SetDestination(finalPosition);
                    destination = finalPosition;
                }
                agent.SetDestination(destination);
                break;
            case AIStates.Aggressive:
                destination = _target.position;
                agent.SetDestination(destination);
                break;
            default:
                break;
        }

        bool isMoving = !HasReachedDestination();

        if (_wasMoving != isMoving)
        {
            _wasMoving = isMoving;
            if (isMoving)
                OnMoveStart.Invoke();
            else
                OnMoveEnd.Invoke();
        }

        yield return new WaitForSeconds(navTickTime);
        StartCoroutine(UpdateDestination());
    }

    bool HasReachedDestination()
    {
        return !agent.pathPending
            && (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
            && agent.remainingDistance <= agent.stoppingDistance;
    }

    public void ChangeState(AIStates newState)
    {
        currState = newState;
    }
}
