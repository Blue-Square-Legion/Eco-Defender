using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPlayer : MonoBehaviour
{
    [SerializeField] GameObject player;

    private void Awake()
    {
        if (!player) player = GameObject.FindGameObjectWithTag("Player");

        Respawn();
    }


    public void Respawn()
    {
        player.transform.position = transform.position;
        player.transform.rotation = transform.rotation;

        if (player.TryGetComponent<Damagable>(out Damagable damage))
        {
            damage.Reset();
        }
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
