using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using AudioScript;

public class PlayerRayInteractor : XRRayInteractor
{
    [SerializeField] private Inventory _inv;
    [SerializeField] private PlayerController playerController;

    [Header("Custom Data")]
    public UnityEvent OnGunHeld;
    public UnityEvent OnGunReleased;

    public Inventory Inv { get { return _inv; } }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (!playerController.InventoryOn)
        {
            base.OnSelectEntered(args);

            if (args.interactableObject.transform.gameObject.GetComponent<Gun>())
            {
                OnGunHeld.Invoke();
                Debug.Log("Gun held");
            }
        }
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        if (!playerController.InventoryOn)
        {
            base.OnSelectExited(args);

            if (args.interactableObject.transform.gameObject.GetComponent<Gun>())
            {
                OnGunReleased.Invoke();
                Debug.Log("Gun released");
            }
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
