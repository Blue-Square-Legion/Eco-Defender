using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private ItemSO _defaultItem;


    public void Spawn()
    {
        Spawn(_defaultItem);
    }

    public void Spawn(ItemSO item)
    {
        if (item?.Prefab)
        {
            Instantiate(item.Prefab, transform.position, Quaternion.identity);
        }        
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 0.1f);
        Gizmos.DrawLine(transform.position, transform.position - (transform.up * 1));
    }

#endif

}
