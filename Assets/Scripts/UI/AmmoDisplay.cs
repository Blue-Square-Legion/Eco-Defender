using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoDisplay : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI ammoTxt;
    [SerializeField] private Sprite availableAmmo;
    [SerializeField] private Sprite spentAmmo;

    [SerializeField] private List<Image> Ammo;

    private int ammoCount;
    private int maxAmmo;
    public static AmmoDisplay AD;

    private void Awake()
    {
        if (AD == null)
        {
            AD = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (ammoTxt == null)
            ammoTxt = gameObject.GetComponent<TMPro.TextMeshProUGUI>();
    }

    public void EnableAmmoDisplay(bool ammoDisplay)
    {
        ammoTxt.enabled = ammoDisplay;

        foreach (Image bulletIcon in Ammo)
            bulletIcon.enabled = ammoDisplay;
        ammoTxt.SetText($"{ammoCount} / {maxAmmo}");
    }

    public void EnableAmmoDisplay(bool ammoDisplay, int maxGunAmmo, int curGunAmmo)
    {
        ammoTxt.enabled = ammoDisplay;
        maxAmmo = maxGunAmmo;
        ammoCount = curGunAmmo;
    }

    public void SetMaxAmmo(int gunsMaxAmmo) 
    {
        maxAmmo = gunsMaxAmmo;
        ammoTxt.SetText($"{ammoCount} / {maxAmmo}");
    }

    public void SetMaxAmmo(int currAmmo, int gunsMaxAmmo) 
    {
        maxAmmo = gunsMaxAmmo;
        ammoCount = currAmmo;
        ammoTxt.SetText($"{ammoCount} / {maxAmmo}");
    }

    public void AmmoReloaded(int ammoAdded) {
        ammoCount = ammoAdded;
        ammoTxt.SetText($"{ammoCount} / {maxAmmo}");
        StartCoroutine("AmmoIconReset", ammoAdded);
        
    }

    public void GunFired(int currAmmo)
    {
        Ammo[currAmmo].sprite = spentAmmo;
        ammoTxt.SetText($"{currAmmo} / {maxAmmo}");
    }

    private IEnumerator AmmoIconReset(int ammoAdded) 
    {
        for (int i = 0; i < ammoAdded; i++)
            Ammo[i].sprite = availableAmmo;
        yield return null;
        StopCoroutine("AmmoIconReset");
    }

    //Doesnt give ammo display count at start
}
