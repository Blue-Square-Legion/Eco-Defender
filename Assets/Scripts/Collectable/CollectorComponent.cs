using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class CollectorComponent : MonoBehaviour
{
    [SerializeField] private Inventory _invRef;
    [SerializeField] private GameObject popupMessage;
    [SerializeField] private float messageDuration = 0.3f;

    private LinkedList<string> _popupMessages;

    public Inventory InvRef
    {
        get { return _invRef; }
    }

    private void Start()
    {
        _popupMessages = new LinkedList<string>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<ItemPickUp>())
        {
            _invRef.AddToInventory(other.gameObject.GetComponent<ItemPickUp>().ItemSO);

            if (_invRef.Inv.ContainsKey(other.GetComponent<ItemPickUp>().ItemSO))
            {
                _popupMessages.AddLast("You got " + _invRef.Inv[other.GetComponent<ItemPickUp>().ItemSO] + " " + other.GetComponent<ItemPickUp>().ItemSO.name + "s.");
            }
            else
            {
                _popupMessages.AddLast("You got " + (other.GetComponent<ItemPickUp>().Quantity > 1 ? other.GetComponent<ItemPickUp>().Quantity + other.GetComponent<ItemPickUp>().ItemSO.name + "s." : "one " + other.GetComponent<ItemPickUp>().ItemSO.name));
            }
            
            StartCoroutine(ShowMessage());

            Destroy(other.gameObject);
        }
    }

    IEnumerator ShowMessage()
    {
        TextMeshProUGUI pM = popupMessage.GetComponent<TextMeshProUGUI>();

        if (_popupMessages.First != null)
        {
            pM.SetText(_popupMessages.First.Value);
        }

        yield return new WaitForSeconds(pM.text.Split(' ').Length * messageDuration);

        if (_popupMessages.First != null)
        {
            _popupMessages.RemoveFirst();
            StartCoroutine(ShowMessage());
        } else
        {
            pM.SetText("");
        }
    }
}
