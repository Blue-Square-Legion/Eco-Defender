using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using EventSO;

public class CollectorComponent : MonoBehaviour
{
    [SerializeField] private XRBaseInteractor _controller;
    [SerializeField] private Inventory _invRef;
    [SerializeField] private InteractionLayerMask _mask;

    public Inventory InvRef
    {
        get { return _invRef; }
    }

    private void OnEnable()
    {
        _controller.selectEntered.AddListener(OnSelectEntered);
    }

    private void OnDisable()
    {
        _controller.selectEntered.RemoveListener(OnSelectEntered);
    }

    //Sends Event to Collectable to handle collection.
    private void OnSelectEntered(SelectEnterEventArgs Event)
    {
        InteractionLayerMask mask = Event.interactableObject.interactionLayers;

        if ((mask & _mask) != 0)
        {
            Event.interactableObject.transform.gameObject.BroadcastMessage("OnCollected");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<ItemTag>().Tag.Type == ItemType.Ammo)
        {
            _invRef.SeedCount++;
            Destroy(other.gameObject);
        }
    }
}
