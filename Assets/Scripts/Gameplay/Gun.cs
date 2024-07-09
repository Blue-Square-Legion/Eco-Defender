using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;

public class Gun : MonoBehaviour, IUsable, IEquip
{
    [SerializeField] private TextMeshProUGUI ammoCountUI;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private ItemSO seedProjectileSO;
    [SerializeField] private GameObject spawnedProjectile;
    [SerializeField] private int maxAmmo = 10;
    [SerializeField] private int startingAmmo = 10;
    [SerializeField] private TextMeshProUGUI ammoTxt;
    [SerializeField, Range(1, 10), Tooltip("How much to multiply seed pod count when reloading")]

    private int countMultipler = 10;
    
    private int currAmmo;
    private int MaxAmmoMultiplyAware => maxAmmo / countMultipler;

    public string gunEmptySound = "Play_Gun_Empty";
    public string Gun_PlantBombSingleShoot = "Play_PlantBombOneShot";
    public string Gun_ReloadSound = "Play_Reload";

    private bool _hasGottenStarterAmmo = false;

    public int CurrAmmo
    {
        get { return currAmmo; }
        set { currAmmo = value; }
    }

    public int MaxAmmo
    {
        get { return maxAmmo; }
        set { maxAmmo = value; }
    }

    public Inventory InvRef
    {
        get { return Inventory.Instance; }
        set { Inventory.Instance = value; }
    }

    public ItemSO SeedProjectile
    {
        get { return seedProjectileSO; }
        set { seedProjectileSO = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        currAmmo = startingAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        if (Inventory.Instance)
        {
            if (Inventory.Instance.Inv.ContainsKey(seedProjectileSO) && ammoCountUI.enabled)
            {
                ammoCountUI.SetText($"{currAmmo} / {maxAmmo} \n Seeds in Inventory: {Inventory.Instance.Inv[seedProjectileSO]}");
            }
            else
            {
                ammoCountUI.SetText($"{currAmmo} / {maxAmmo} \n Seeds in Inventory: 0");
            }
        }
    }


    /*protected override void OnActivated(ActivateEventArgs args)
    {
        base.OnActivated(args);
         
        ShootGun();
    }*/
    
    public void ShootGun()
    {
        if (currAmmo > 0)
        {
            if (seedProjectileSO.Prefab)
            {
                currAmmo--;
                AmmoDisplay.AD.GunFired(currAmmo);
                GameObject projObj = Instantiate(spawnedProjectile, spawnPoint.transform.position + spawnPoint.transform.forward, spawnPoint.transform.rotation);

                AkSoundEngine.PostEvent(Gun_PlantBombSingleShoot, gameObject);
                Destroy(projObj, 1f);
            }
            else
            {
                Debug.DrawLine(spawnPoint.transform.position, spawnPoint.transform.position + (spawnPoint.transform.forward * 50f), Color.black, 20f);
            }
        }
        else
        {
            // Auto reload gun on empty
            Reload();
        }
    }

    public void SetupAmmo()
    {
        /*        print($"Setup Ammo:{_hasGottenStarterAmmo}");
                if (!_hasGottenStarterAmmo)
                {
                    Inventory.Instance.AddToInventory(seedProjectileSO, startingAmmo);
                    Reload();
                    _hasGottenStarterAmmo = true;
                }*/
    }

    public void Reload()
    {
        if (Inventory.Instance.Inv.ContainsKey(seedProjectileSO))
        {
            if (Inventory.Instance.Inv[seedProjectileSO] <= 0)
            {
                // No ammo, so play empty gun
                AkSoundEngine.PostEvent(gunEmptySound, gameObject);
                return;
            }
            else if (Inventory.Instance.Inv[seedProjectileSO] < MaxAmmoMultiplyAware)
            {
                currAmmo = Inventory.Instance.Inv[seedProjectileSO] * countMultipler;                
                Inventory.Instance.RemoveFromInventory(seedProjectileSO, MaxAmmoMultiplyAware);
                AmmoDisplay.AD.AmmoReloaded(currAmmo);
            }
            else
            {
                Inventory.Instance.RemoveFromInventory(seedProjectileSO, MaxAmmoMultiplyAware);
                currAmmo = maxAmmo;
                AmmoDisplay.AD.AmmoReloaded(currAmmo);
            }
            AkSoundEngine.PostEvent(Gun_ReloadSound, gameObject);
        }
        else
        {
            AkSoundEngine.PostEvent(gunEmptySound, gameObject);
        }

    }


    public void Equip(bool isFPS = true)
    {
        if (isFPS) { spawnPoint = Camera.main.transform; }

        SetupAmmo();
        AmmoDisplay.AD.EnableAmmoDisplay(true);
        AmmoDisplay.AD.SetMaxAmmo(CurrAmmo,maxAmmo);
        TogglePhysics(false);
        Debug.Log("Gun was picked up");
    }

    public void UnEquip()
    {
        TogglePhysics(true);
    }

    public void Use()
    {
        Debug.Log($"{gameObject.name} is being fired");
        ShootGun();
    }

    public void AltUse()
    {
        Reload();
    }

    public void TogglePhysics(bool isEnabled)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        //rb.useGravity = isEnabled;//recommend using these settings so that the gun will stay following the player's pozition/rotation once picked up.
        //rb.isKinematic = isEnabled;
        rb.isKinematic = !isEnabled;
        rb.detectCollisions = isEnabled;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"I am colliding with {collision.gameObject.name}");
    }
}
