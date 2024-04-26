using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	[SerializeField] private int invSize = 5;
	[SerializeField] private GameObject _invCanvas;
	[SerializeField] private GameObject _invSlot;
	[SerializeField] private Transform _itemSpawnPoint;

	private Dictionary<ItemSO, int> _inv;

	public Dictionary<ItemSO, int> Inv { get { return _inv; } }

	public Inventory()
    {
		_inv = new Dictionary<ItemSO, int>(invSize);
	}

	void Start()
	{
		_inv = new Dictionary<ItemSO, int>(invSize);
	}

	public void AddToInventory(ItemSO item, int amount = 1)
	{
		if (_inv.ContainsKey(item))
		{
			_inv[item] += amount;
		}
		else
		{
			_inv.Add(item, 1);
		}
	}

	public void RemoveFromInventory(ItemSO item, int amount = 1, bool spawnedFromGun = false)
	{
		if (_inv[item] > 0)
		{
			if (!spawnedFromGun)
			{
				Instantiate(item.Prefab, _invCanvas.transform.position, _invCanvas.transform.rotation);
			}

			_inv[item] -= amount;

			if (_inv[item] <= 0)
			{
				_inv.Remove(item);
			}
		}
	}

    private void OnEnable()
    {
		foreach (KeyValuePair<ItemSO, int> item in _inv)
		{
			GameObject go = Instantiate(_invSlot, _invCanvas.transform);
			go.GetComponent<InventorySlot>().ItemData = item.Key;
			go.GetComponent<InventorySlot>().Amount = item.Value;
			go.GetComponent<InventorySlot>().ItemSpawnPoint = _itemSpawnPoint;
		}
	}

    private void OnDisable()
    {
        foreach (InventorySlot item in FindObjectsOfType<InventorySlot>())
        {
			Destroy(item.gameObject);
        }
	}
}
