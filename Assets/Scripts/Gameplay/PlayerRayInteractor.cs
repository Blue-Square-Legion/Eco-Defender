using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerRayInteractor : XRRayInteractor
{
    [Header("Custom Data")]
    public UnityEvent OnGunHeld;
    public UnityEvent OnGunReleased;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        if(args.interactableObject.transform.gameObject.GetComponent<Gun>())
        {
            OnGunHeld.Invoke();
            Debug.Log("Gun held");
        }
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        if (args.interactableObject.transform.gameObject.GetComponent<Gun>())
        {
            OnGunReleased.Invoke();
            Debug.Log("Gun released");
        }
    }

    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        base.OnHoverEntered(args);

        InventorySlot _invSlotRef = args.interactableObject.transform.gameObject.GetComponent<InventorySlot>();

        if (_invSlotRef)
        {
            _invSlotRef.HighlightItem();
        }
    }

    protected override void OnHoverExited(HoverExitEventArgs args)
    {
        base.OnHoverExited(args);

        InventorySlot _invSlotRef = args.interactableObject.transform.gameObject.GetComponent<InventorySlot>();

        if (_invSlotRef)
        {
            _invSlotRef.HighlightItem();
        }
    }
}
