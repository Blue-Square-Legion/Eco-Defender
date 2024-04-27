using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType
{
    Resource,
    Ammo,    
    Weapon
}

[CreateAssetMenu(fileName = "Eco", menuName = "Eco_Item/Item")]
public class ItemSO : ScriptableObject
{
    public ItemType Type;
    public GameObject Prefab;
}