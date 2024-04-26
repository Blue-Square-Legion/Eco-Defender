using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Gun : XRGrabInteractable
{
    private int currAmmo;
    private Inventory _invRef;

    [Header("Custom Variables")]
    [SerializeField] private TextMeshProUGUI ammoCountUI;
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private ItemSO seedProjectileSO;
    [SerializeField] private GameObject spawnedProjectile;
    [SerializeField] private int maxAmmo = 10;

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
                Destroy(projObj, 1f);
            }
            else
            {
                Debug.DrawLine(spawnPoint.transform.position, spawnPoint.transform.position + (spawnPoint.transform.forward * 50f), Color.black, 20f);
            }
        } else
        {
            Reload();
        }

        ammoCountUI.SetText($"{ currAmmo } / { maxAmmo } \n Seeds in Inventory: { _invRef.Inv[seedProjectileSO] }");
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        _invRef = args.interactorObject.transform.gameObject.GetComponent<CollectorComponent>().InvRef;
        ammoCountUI.SetText($"{ currAmmo } / { maxAmmo } \n Seeds in Inventory: { _invRef.Inv[seedProjectileSO] }");
    }

    public void Reload()
    {
        if (_invRef.Inv[seedProjectileSO] > 0)
        {
            if (_invRef.Inv[seedProjectileSO] < maxAmmo)
            {
                currAmmo = _invRef.Inv[seedProjectileSO];
                _invRef.RemoveFromInventory(seedProjectileSO, currAmmo, true);
            }
            else
            {
                _invRef.RemoveFromInventory(seedProjectileSO, maxAmmo, true);
                currAmmo = maxAmmo;
            }
        }
    }
}
