using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    private bool _highlighted;
    private int amount;
    private ItemSO itemData;
    private Transform _itemSpawnPoint;

    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemAmountText;

    public ItemSO ItemData { get { return itemData; } set { itemData = value; } }
    public int Amount { get { return amount; } set { amount = value; } }
    public Transform ItemSpawnPoint { get { return _itemSpawnPoint; } set { _itemSpawnPoint = value; } }

    // Start is called before the first frame update
    void Start()
    {
        itemNameText.SetText(itemData.name);
    }

    // Update is called once per frame
    void Update()
    {
        itemAmountText.SetText(amount.ToString());
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
        Instantiate(itemData.Prefab, _itemSpawnPoint.transform.position, _itemSpawnPoint.transform.rotation);
    }
}
