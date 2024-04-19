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

        testGun.ShootGun();

        Assert.AreEqual(0, testGun.CurrAmmo);
    }

    [Test]
    public void GunReloadedWithSeedCountExceedingMax()
    {
        testGun.CurrAmmo = 0;
        testGun.MaxAmmo = 10;
        testInv.SeedCount = 50;

        testGun.Reload();

        Assert.AreEqual(10, testGun.CurrAmmo);
    }

    [Test]
    public void GunReloadedWithSeedCountLessThanMax()
    {
        testGun.CurrAmmo = 0;
        testGun.MaxAmmo = 10;
        testInv.SeedCount = 8;

        testGun.Reload();

        Assert.AreEqual(8, testGun.CurrAmmo);
    }
}
