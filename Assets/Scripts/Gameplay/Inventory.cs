using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	[SerializeField] private int invSize = 5;

	private Dictionary<ItemSO, int> _inv;

	public Dictionary<ItemSO, int> Inv { get { return _inv; } }

	public Inventory()
    {
		_inv = new Dictionary<ItemSO, int>(invSize);
	}

	public static Inventory Instance;

	void Start()
	{
        if (!Instance)
        {
			Instance = this;
        }

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

	public void RemoveFromInventory(ItemSO item, int amount = 1)
	{
		if (_inv[item] > 0)
		{
			_inv[item] -= amount;

			if (_inv[item] <= 0)
			{
				_inv.Remove(item);
			}
		}
	}
}
