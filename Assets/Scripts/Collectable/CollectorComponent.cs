using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using EventSO;

public class CollectorComponent : MonoBehaviour
{
    [SerializeField] private XRBaseInteractor _controller;
    [SerializeField] private InteractionLayerMask _mask;

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
}
