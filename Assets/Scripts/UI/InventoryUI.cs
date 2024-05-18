using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Inventory _inv;
    [SerializeField] private GameObject _invSlot;
    [SerializeField] private Transform _itemSpawnPoint;

    public TextMeshProUGUI noItemsText;

    public static InventoryUI Instance;


    public void OnEnable()
    {
        if (!Instance)
        {
            Instance = this;
        }

        RedrawInventory();
        Inventory.Instance.OnInventoryChange.RemoveListener(UpdateInventory);
        Inventory.Instance.OnInventoryChange.AddListener(UpdateInventory);
    }

    private void UpdateInventory(ItemSO arg0)
    {
        ClearInventory();
        RedrawInventory();
    }

    private void RedrawInventory()
    {
        foreach (KeyValuePair<ItemSO, int> item in _inv.Inv)
        {
            GameObject go = Instantiate(_invSlot, transform);
            go.GetComponent<InventorySlot>().itemData = item;
            go.GetComponent<InventorySlot>().ItemSpawnPoint = _itemSpawnPoint;
        }

        if (_inv.Inv.Count != 0)
        {
            noItemsText.enabled = false;
        }
        else
        {
            noItemsText.enabled = true;
        }
    }

    private void OnDisable()
    {
        Inventory.Instance.OnInventoryChange.RemoveListener(UpdateInventory);
        ClearInventory();
    }

    private void ClearInventory()
    {
        foreach (InventorySlot item in GetComponentsInChildren<InventorySlot>())
        {
            Destroy(item.gameObject);
        }
    }
}
