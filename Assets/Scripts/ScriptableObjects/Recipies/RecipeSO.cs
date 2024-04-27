using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "Recipe")]
public class RecipeSO : ScriptableObject
{
    //Items needed to make the new item
    [Tooltip("Items needed to make the new item")]
    public List<ItemSO> reqItems;

    //The result of the combined items
    [Tooltip("The result of the combined items")]
    public ItemSO result;


    public bool IsMatch(List<ItemSO> items)
    {
        return reqItems.TrueForAll(item => items.Contains(item));
    }
}
