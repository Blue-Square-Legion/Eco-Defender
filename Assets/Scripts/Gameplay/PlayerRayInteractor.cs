using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerRayInteractor : XRRayInteractor
{
    [Header("Custom Data")]
    public UnityEvent OnGunHeld;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        if(args.interactableObject.transform.gameObject.GetComponent<Gun>())
        {
            OnGunHeld.Invoke();
        }
    }
}
