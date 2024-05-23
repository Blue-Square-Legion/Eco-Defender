using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftUI : MonoBehaviour
{
    [SerializeField] Crafting _craftingRef;
    [SerializeField] GameObject _craftButtonPrefab;

    private void Start()
    {
        if (_craftingRef != null)
        {
            _craftingRef = GameObject.FindAnyObjectByType<Crafting>();
        }


        _craftingRef.Recipes.ForEach(recipe =>
        {
            GameObject go = Instantiate(_craftButtonPrefab, transform);

            _craftButtonPrefab.GetComponentInChildren<TMP_Text>().SetText(recipe.result.name);

            go.GetComponent<Button>().onClick.AddListener(() => Craft(recipe));
        });

    }

    private void Craft(RecipeSO recipe)
    {
        bool hasAllItems = recipe.reqItems.TrueForAll(item => Inventory.Instance.HasItem(item));
        if (!hasAllItems) return;

        recipe.reqItems.ForEach(item => Inventory.Instance.RemoveFromInventory(item));
        Inventory.Instance.AddToInventory(recipe.result, 1);
    }
}
