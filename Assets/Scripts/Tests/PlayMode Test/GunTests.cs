using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.XR.Interaction.Toolkit;

public class GunTests
{
    Gun testGun = new Gun();
    Inventory testInv = new Inventory();

    // A Test behaves as an ordinary method
    [Test]
    public void GunHasEnoughAmmo()
    {
        testGun.CurrAmmo = 10;
        testGun.MaxAmmo = 10;

        Assert.Greater(testGun.CurrAmmo, 0);
    }

    [Test]
    public void GunAmmoDepleted()
    {
        testGun.CurrAmmo = 1;
        testGun.MaxAmmo = 10;

        testGun.CurrAmmo--;

        Assert.AreEqual(0, testGun.CurrAmmo);
    }

    [Test]
    public void GunReloadedWithSeedCountExceedingMax()
    {
        testInv = new GameObject().AddComponent<Inventory>();
        testGun = new GameObject().AddComponent<Gun>();

        testGun.CurrAmmo = 0;
        testGun.MaxAmmo = 10;

        testGun.SeedProjectile = new GameObject().AddComponent<ItemTag>().Tag;
        testGun.SeedProjectile = new ItemSO();
        testGun.SeedProjectile.Prefab = new GameObject();

        ItemSO ammo = testGun.SeedProjectile;

        for (int i = 0; i < 50; i++)
        {
            testInv.AddToInventory(ammo);
        }

        testGun.InvRef = testInv;

        testGun.Reload();

        Assert.AreEqual(10, testGun.CurrAmmo);
    }

    [Test]
    public void GunReloadedWithSeedCountLessThanMax()
    {
        testGun.CurrAmmo = 0;
        testGun.MaxAmmo = 10;

        testInv = new GameObject().AddComponent<Inventory>();
        testGun = new GameObject().AddComponent<Gun>();

        testGun.SeedProjectile = new GameObject().AddComponent<ItemTag>().Tag;
        testGun.SeedProjectile = new ItemSO();
        testGun.SeedProjectile.Prefab = new GameObject();

        ItemSO ammo = testGun.SeedProjectile;

        for (int i = 0; i < 8; i++)
        {
            testInv.AddToInventory(ammo);
        }

        testGun.InvRef = testInv;

        testGun.Reload();

        Assert.AreEqual(8, testGun.CurrAmmo);
    }
}
