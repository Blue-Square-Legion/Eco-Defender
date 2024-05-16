using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private float time;

    [SerializeField] private int Max = 3;

    // Start is called before the first frame update1
    void Start()
    {
        StartCoroutine(Spawn());
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            if (transform.hierarchyCount <= Max)
            {
                Instantiate(prefab, transform.position, Quaternion.identity, transform);
            }
            yield return new WaitForSeconds(time);
        }

    }
}
