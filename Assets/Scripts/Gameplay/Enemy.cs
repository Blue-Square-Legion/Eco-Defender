using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamagable
{
    private NavMeshAgent agent;

    [SerializeField] private Transform target;
    [SerializeField] private float time = 0.5f;

    [SerializeField] private float _maxHealth = 2;
    private float _health;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        StartCoroutine(UpdateDestination());
        _health = _maxHealth;

        target = Camera.main.transform;
    }

    public void Damage(float damage = 1)
    {
        _health -= damage;

        if (_health <= 0) { Destroy(gameObject); }
    }

    private void OnTriggerEnter(Collider other)
    {
        print(other.name);

        if (other.tag == "DamageCollider")
        {
            other.gameObject.SendMessage("Damage", 1);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "DamageCollider")
        {
            other.gameObject.SendMessage("Damage", 1);
        }
    }


    IEnumerator UpdateDestination()
    {
        while (true)
        {
            yield return new WaitForSeconds(time);
            agent.SetDestination(target.position);
        }
    }

}
