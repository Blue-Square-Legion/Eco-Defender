using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Gun : XRGrabInteractable
{
    private int currAmmo;
    [SerializeField] private Inventory _invRef;

    [Header("Custom Variables")]
    [SerializeField] private TextMeshProUGUI ammoCountUI;
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private GameObject projectile;
    [SerializeField] private int maxAmmo = 10;

    public string gunEmptySound = "Play_Gun_Empty";
    public string Gun_PlantBombSingleShoot = "Play_PlantBombOneShot";

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ammoCountUI.SetText($"{ currAmmo } / { maxAmmo }");
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
            if (projectile)
            {
                currAmmo--;
                AkSoundEngine.PostEvent(Gun_PlantBombSingleShoot, gameObject);
                GameObject projObj = Instantiate(projectile, spawnPoint.transform.position, spawnPoint.transform.rotation);
                Destroy(projObj, 1f);
            }
            else
            {
                Debug.DrawLine(spawnPoint.transform.position, spawnPoint.transform.position + (spawnPoint.transform.forward * 50f), Color.black, 20f);
            }
        } else
        {
            // Gun is empty, put empty sound here
            AkSoundEngine.PostEvent(gunEmptySound, gameObject);
        }
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        _invRef = args.interactorObject.transform.gameObject.GetComponent<CollectorComponent>().InvRef;
    }

    public void Reload()
    {
        if (_invRef.SeedCount > 0)
        {
            if (_invRef.SeedCount < maxAmmo)
            {
                currAmmo = _invRef.SeedCount;
                _invRef.SeedCount = 0;
            }
            else
            {
                _invRef.SeedCount -= maxAmmo;
                currAmmo = maxAmmo;
            }
        }
    }
}
