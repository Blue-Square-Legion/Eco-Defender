using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    private bool _highlighted;
    private Transform _itemSpawnPoint;

    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemAmountText;

    public KeyValuePair<ItemSO, int> itemData;

    public Transform ItemSpawnPoint { get { return _itemSpawnPoint; } set { _itemSpawnPoint = value; } }

    // Start is called before the first frame update
    void Start()
    {
        itemNameText.SetText(itemData.Key.name);
        itemAmountText.SetText(Inventory.Instance.Inv[itemData.Key].ToString());
    }

    public void HighlightItem()
    {
        if (!_highlighted)
        {
            // Highlight material
            _highlighted = true;
        }
        else
        {
            // Unhighlight material
            _highlighted = false;
        }
    }

    public void SpawnItem()
    {
        Instantiate(itemData.Key.Prefab, _itemSpawnPoint.transform.position, _itemSpawnPoint.transform.rotation);

        Inventory.Instance.RemoveFromInventory(itemData.Key, 1);

        if (!Inventory.Instance.Inv.ContainsKey(itemData.Key))
        {
            if (Inventory.Instance.Inv.Count == 0)
            {
                InventoryUI.Instance.noItemsText.enabled = true;
            }

            Destroy(gameObject);
        }
    }
}
