using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoDisplay : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI ammoTxt;
    [SerializeField] private Sprite availableAmmo;
    [SerializeField] private Sprite spentAmmo;

    [SerializeField] private List<Sprite> Ammo;

    // Start is called before the first frame update
    void Start()
    {
        if (ammoTxt == null)
            ammoTxt = gameObject.GetComponent<TMPro.TextMeshProUGUI>();
    }

    private void SetDisplay() {
        
    }
}
