//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private float time;

    [SerializeField] private int Max = 3;

    private void OnEnable()
    {
        StartCoroutine(Spawn());
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            if (transform.childCount < Max)
            {
                Instantiate(prefab, transform.position, Quaternion.identity, transform);
            }
            yield return new WaitForSeconds(time);
        }

    }
}
