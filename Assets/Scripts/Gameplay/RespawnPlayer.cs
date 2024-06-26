using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class RespawnPlayer : MonoBehaviour
{
    [SerializeField] GameObject player;

    [SerializeField] private Damageable damagable;

    private void Awake()
    {
        if (!player) player = GameObject.FindGameObjectWithTag("Player");

        damagable = player.GetComponent<Damageable>();

        if (damagable == null)
        {
            damagable = player.GetComponentInChildren<Damageable>();
        }

        if (damagable == null)
        {
            Debug.LogError("Could Not Find Damage on Player");
        }

        Respawn();
    }


    public void Respawn()
    {
        player.transform.position = transform.position;
        player.transform.rotation = transform.rotation;

        damagable?.Reset();
    }


#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, 0.5f);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward);
    }
#endif
}
