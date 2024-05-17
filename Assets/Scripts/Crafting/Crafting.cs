using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class Crafting : MonoBehaviour
{
    [SerializeField] private List<XRSocketInteractor> _recipeSocket;
    [SerializeField] private GameObject _spawnLocation;

    [SerializeField] private List<RecipeSO> _recipes;

    [Space(10)]
    public UnityEvent OnCraftSuccess, OnCraftFailed, OnCraftEmpty;

    private List<ItemSO> _items;

    public string CraftEmpty = "Play_CraftEmpty";
    public string CraftSuccess = "Play_CraftSuccess";

    public List<RecipeSO> Recipes => _recipes;

    public void Craft()
    {
        _items = _recipeSocket.ConvertAll<ItemSO>(item =>
            item.GetOldestInteractableSelected()?
                .transform.GetComponent<ItemTag>()?.Tag
        );

        if (_items.TrueForAll(item => item == null))
        {
            OnCraftEmpty.Invoke();
            AkSoundEngine.PostEvent(CraftEmpty, gameObject);
            return;
        }

        //iterate to find first match (TODO: Can be improved later)
        RecipeSO found = _recipes.Find(item => item.IsMatch(_items));

        if (found)
        {
            ClearSockets();
            SpawnItem(found.result);
            OnCraftSuccess.Invoke();
            AkSoundEngine.PostEvent(CraftSuccess, gameObject);
        }
        else
        {
            OnCraftFailed.Invoke();
            AkSoundEngine.PostEvent(CraftEmpty, gameObject);
        }
    }

    private void ClearSockets()
    {
        _recipeSocket.ForEach(item => Destroy(item.GetOldestInteractableSelected().transform.gameObject));
    }

    private void SpawnItem(ItemSO Item)
    {
        Instantiate(Item.Prefab, _spawnLocation.transform.position, Quaternion.identity);
    }
}
