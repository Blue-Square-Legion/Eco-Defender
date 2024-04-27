using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using EventSO;

public class CollectorComponent : MonoBehaviour
{
    [SerializeField] private Inventory _invRef;

    public Inventory InvRef
    {
        get { return _invRef; }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<ItemTag>())
        {
            _invRef.AddToInventory(other.gameObject.GetComponent<ItemTag>().Tag);

            if (InventoryUI.Instance.gameObject)
            {
                InventoryUI.Instance.OnEnable();
            }
            
            Destroy(other.gameObject);
        }
    }
}
