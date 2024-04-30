using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class Gun : XRGrabInteractable
{
    private int currAmmo;
    [SerializeField] private Inventory _invRef;

    [Header("Custom Variables")]
    [SerializeField] private TextMeshProUGUI ammoCountUI;
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private ItemSO seedProjectileSO;
    [SerializeField] private GameObject spawnedProjectile;
    [SerializeField] private int maxAmmo = 10;
    [SerializeField] private int startingAmmo = 10;

    public string gunEmptySound = "Play_Gun_Empty";
    public string Gun_PlantBombSingleShoot = "Play_PlantBombOneShot";
    public string Gun_ReloadSound = "Play_Reload";

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

    public Inventory InvRef { 
        get { return _invRef; }
        set { _invRef = value; }
    }

    public ItemSO SeedProjectile
    {
        get { return seedProjectileSO; }
        set { seedProjectileSO = value; }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(_invRef)
        {
            if (_invRef.Inv.ContainsKey(seedProjectileSO))
            {
                ammoCountUI.SetText($"{ currAmmo } / { maxAmmo } \n Seeds in Inventory: { _invRef.Inv[seedProjectileSO] }");
            } else
            {
                ammoCountUI.SetText($"{ currAmmo } / { maxAmmo } \n Seeds in Inventory: 0");
            }
        }
    }

    protected override void OnActivated(ActivateEventArgs args)
    {
        base.OnActivated(args);

        ShootGun();
    }

    public void ShootGun()
    {
        if (currAmmo > 0)
        {
            if (seedProjectileSO.Prefab)
            {
                currAmmo--;
                GameObject projObj = Instantiate(spawnedProjectile, spawnPoint.transform.position, spawnPoint.transform.rotation);
                
                AkSoundEngine.PostEvent(Gun_PlantBombSingleShoot, gameObject);
                Destroy(projObj, 1f);
            }
            else
            {
                Debug.DrawLine(spawnPoint.transform.position, spawnPoint.transform.position + (spawnPoint.transform.forward * 50f), Color.black, 20f);
            }
        } else
        {
            // Auto reload gun on empty
            Reload();
        }
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        _invRef = args.interactorObject.transform.gameObject.GetComponent<PlayerRayInteractor>().Inv;
        if(!_invRef.Inv.ContainsKey(seedProjectileSO))
        {
            _invRef.AddToInventory(seedProjectileSO, startingAmmo);
        }
    }

    public void Reload()
    {
        if (_invRef.Inv.ContainsKey(seedProjectileSO))
        {
            if (_invRef.Inv[seedProjectileSO] <= 0)
            {
                // No ammo, so play empty gun
                AkSoundEngine.PostEvent(gunEmptySound, gameObject);
                return;
            }
            else if (_invRef.Inv[seedProjectileSO] < maxAmmo)
            {
                currAmmo = _invRef.Inv[seedProjectileSO];
                _invRef.RemoveFromInventory(seedProjectileSO, currAmmo);
            }
            else
            {
                _invRef.RemoveFromInventory(seedProjectileSO, maxAmmo);
                currAmmo = maxAmmo;
            }
            AkSoundEngine.PostEvent(Gun_ReloadSound, gameObject); 
        }
        else
        {
            AkSoundEngine.PostEvent(gunEmptySound, gameObject);
        }

    }
}
