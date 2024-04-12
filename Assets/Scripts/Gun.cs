using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Gun : XRGrabInteractable
{
    [Header("Custom Variables")]
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private GameObject projectile;

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

        if (projectile)
        {
            GameObject projObj = Instantiate(projectile, spawnPoint.transform.position, spawnPoint.transform.rotation);
            Destroy(projObj, 1f);
        }
        else
        {
            Debug.DrawLine(spawnPoint.transform.position, spawnPoint.transform.position + (spawnPoint.transform.forward * 50f), Color.black, 20f);
        }

    }
}
