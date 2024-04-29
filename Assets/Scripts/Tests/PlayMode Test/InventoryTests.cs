using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class InventoryTests
{
    Inventory testInv = new Inventory();
    ItemTag testItem = new ItemTag();

    // A Test behaves as an ordinary method
    [Test]
    public void ItemAddedToInventory()
    {
        testInv = new GameObject().AddComponent<Inventory>();
        testItem = new GameObject().AddComponent<ItemTag>();

        testItem.Tag = new ItemSO();
        testItem.Tag.Prefab = new GameObject();

        testInv.AddToInventory(testItem.Tag);

        Assert.AreEqual(1, testInv.Inv.Count);
    }

    // A Test behaves as an ordinary method
    [Test]
    public void ItemRemovedFromInventory()
    {
        testInv = new GameObject().AddComponent<Inventory>();
        testItem = new GameObject().AddComponent<ItemTag>();

        testItem.Tag = new ItemSO();
        testItem.Tag.Prefab = new GameObject();

        testInv.AddToInventory(testItem.Tag);
        testInv.RemoveFromInventory(testItem.Tag);

        Assert.AreEqual(0, testInv.Inv.Count);
    }

    // A Test behaves as an ordinary method
    [Test]
    public void MultipleDifferentItemsAddedToInventory()
    {
        testInv = new GameObject().AddComponent<Inventory>();
        testItem = new GameObject().AddComponent<ItemTag>();

        for (int i = 0; i < 2; i++)
        {
            testItem.Tag = new ItemSO();
            testItem.Tag.Prefab = new GameObject();
            testInv.AddToInventory(testItem.Tag);
            testItem = new GameObject().AddComponent<ItemTag>();
        }

        Assert.AreEqual(2, testInv.Inv.Count);
    }

    // A Test behaves as an ordinary method
    [Test]
    public void InventoryCapacityReached()
    {
        testInv = new GameObject().AddComponent<Inventory>();
        testItem = new GameObject().AddComponent<ItemTag>();

        for (int i = 0; i < 3; i++)
        {
            if(testInv.Inv.Count > 1)
            {
                break;
            }

            testItem.Tag = new ItemSO();
            testItem.Tag.Prefab = new GameObject();
            testInv.AddToInventory(testItem.Tag);
            testItem = new GameObject().AddComponent<ItemTag>();
        }

        Assert.AreEqual(2, testInv.Inv.Count);
    }
}
