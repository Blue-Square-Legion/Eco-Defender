using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class FPSPlayerInteractionController : MonoBehaviour, IPlayerEquip
{
    public UnityEvent<GameObject> OnHoverEnterUsable, OnHoverExitUsable;
    public UnityEvent OnItemPickup;

    public GameObject PrimaryObject;
    public GameObject holsteredObject;
    private GameObject transferObject;
    [SerializeField] private Transform handSlot;
    [SerializeField] private LayerMask BlockMask, UsableMask;
    [SerializeField] private float radius = 0.2f, distance = 10f;

    private IUsable usable;
    private Transform camera;

    private GameObject hoverObject;

    private void Awake()
    {
        SetObject(PrimaryObject ?? null);

        camera = Camera.main.transform;
    }

    /// <summary>
    /// Sphere cast to get Usable objects. In Fixed update to handle Hover effects
    /// </summary>
    private void FixedUpdate()
    {
        Ray ray = new(camera.transform.position, camera.transform.forward);
        if (Physics.SphereCast(ray, radius, out RaycastHit hit, distance, BlockMask))
        {
            if (hoverObject != hit.collider.gameObject)
            {
                OnHoverExitUsable.Invoke(hoverObject);
                hoverObject = hit.collider.gameObject;

                if ((UsableMask & (1 << hit.transform.gameObject.layer)) != 0)
                {
                    OnHoverEnterUsable.Invoke(hoverObject);
                }
            }
        }
        else
        {
            OnHoverExitUsable.Invoke(hoverObject);
            hoverObject = null;
            return;
        }
    }
    void Update()
    {
        //testing code
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            Debug.Log("keypad1 was pressed");
            if (PrimaryObject != null && holsteredObject == null)
            {
                Debug.Log("primary object was detected");
                Holster(PrimaryObject);
                PrimaryObject = null;
                Debug.Log("primary gun is now holstered");
            }
            else if (holsteredObject != null && PrimaryObject == null)
            {
                Debug.Log("primary object was not detected");
                unHolster(holsteredObject);
                holsteredObject = null;
                Debug.Log("primary gun is now unholstered");
            }
            else if(PrimaryObject != null && holsteredObject != null)
            {
                Debug.Log("Both object slots are full");
                print($"primary is: {PrimaryObject} and holstered object is {holsteredObject}");
                swapGun(PrimaryObject, holsteredObject);
                print($"primary is now: {PrimaryObject} and holstered object is now {holsteredObject}");
            }
            else
            {
                Debug.Log("primary object is not null");
            }
        }
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            //uses Unequip code, but due to changes in Unequip, it messes with the holster boolean value causing duplication.
            Debug.Log("primary object has been unequipped");
            print($"UnEquip: {PrimaryObject}");
            PrimaryObject.GetComponent<IEquip>()?.UnEquip();
            PrimaryObject.transform.SetParent(null, true);

            SetObject(null);
        }
    }



#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        Ray ray = new(camera.transform.position, camera.transform.forward);

        if (Physics.SphereCast(ray, radius, out RaycastHit hit, distance, BlockMask))
        {
            Gizmos.color = ((UsableMask & (1 << hit.transform.gameObject.layer)) != 0) ? Color.green : Color.red;
            Gizmos.DrawWireSphere(ray.GetPoint(hit.distance), radius);
        }
        else
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(ray.GetPoint(distance), radius);
        }
    }
#endif

    public void SetObject(GameObject obj)
    {
        if (obj == null)
        {
            PrimaryObject = null;
            usable = null;
        }
        else
        {
            PrimaryObject = obj;
            usable = obj?.GetComponent<IUsable>();
        }
    }

    public void OnUse()
    {
        //code from seribeengton
        //if (enabled) { usable?.Use(); }
        if (PrimaryObject != null)
        {
            Debug.Log("primary object is detected for firing");
            if (PrimaryObject.activeSelf)
            {
                Debug.Log("Gun has fired");
                usable?.Use();
            }
            else
            {
                Debug.Log("The Primary object is detected as active, but will not fire");
            }
        }
        else
        {
            Debug.Log("Error has occurred with detected if the object isnt null");
        }
    }

    public void OnAltUse()
    {
        usable?.AltUse();
    }

    public void OnInteract()
    {
        print($"Interact: {hoverObject}");

        if (hoverObject == null)
        {
            HandleUI();
            return;
        }

        //Try Equip first > Use > UI
        if (hoverObject.TryGetComponent<IEquip>(out IEquip equip))
        {
            Equip(hoverObject);
            OnItemPickup.Invoke();
        }
        else if (hoverObject.transform.parent && hoverObject.transform.parent.TryGetComponent<IEquip>(out equip))
        {
            //Gun collider on child i/o parent?
            Equip(hoverObject.transform.parent.gameObject);
            OnItemPickup.Invoke();
        }
        else if (hoverObject.TryGetComponent<IUsable>(out IUsable usable))
        {
            usable?.Use();
            OnItemPickup.Invoke();
        }
        else
        {
            HandleUI();
        }
    }

    /// <summary>
    /// Check for Use on in game UI element
    /// </summary>
    private static void HandleUI()
    {
        PointerEventData pointerData = new(EventSystem.current);

        pointerData.position = Input.mousePosition;
        List<RaycastResult> results = new();
        EventSystem.current.RaycastAll(pointerData, results);

        foreach (RaycastResult res in results)
        {
            if (res.gameObject.TryGetComponent<IPointerClickHandler>(out IPointerClickHandler handler))
            {
                pointerData.button = PointerEventData.InputButton.Left;
                handler.OnPointerClick(pointerData);
                return;
            }
        }
    }

    /// <summary>
    /// Equips player with item
    /// TODO: Hot bar / alternate equipment cycling
    /// </summary>
    /// <param name="obj"></param>
    public void Equip(GameObject obj)
    {
        print($"Equip: {obj}");
        if (PrimaryObject) UnEquip(PrimaryObject);

        obj.GetComponent<IEquip>()?.Equip(true);

        //obj.transform.position = handSlot.position;
        //obj.transform.rotation = handSlot.rotation;
        obj.transform.SetParent(handSlot);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.identity;

        SetObject(obj);
    }

    public void UnEquip(GameObject obj)
    {
        if (holsterBool == true)
        {
            print($"UnEquip: {obj}");
            obj.GetComponent<IEquip>()?.UnEquip();
            obj.transform.SetParent(null, true);

            SetObject(null);
        }
        else
        {
            Holster(obj);
        }
    }

    /* <summary>
     * Allows the currently held gun to be set inactive while following the player and being unrendered, and will allow the second gun to be rendered in place and interactable.
     * 1) create a function to be called that swaps between two guns when the player has two guns
     * 2) create a function that can just store one gun
     * 3) create a holster value that can be filled when picking up a second gun when the first gun has not been stored
     */
    private bool holsterBool = false;
    public void Holster(GameObject obj)
        //set gun as inactive
    {
        holsterBool = true;
        holsteredObject = obj;
        obj.SetActive(false);
       // SetObject(null);
        //set a boolean to say that the holster is "full"
    }
    public void unHolster(GameObject obj)
        //set gun as active
    {
        obj.SetActive (true);
        obj.transform.SetParent(handSlot);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.identity;
        holsterBool = false;
        PrimaryObject = obj;
        SetObject(obj);
        //set a boolean to say that the holster is "empty"
    }
    public void swapGun(GameObject obj1, GameObject obj2)
        //sets gun 1 to inactive and gun 2 as active, overwrites holster bool to remain full.
    {
        print($"Swap: {obj1} with {obj2}");
        if (holsterBool == true)
        {
            print($"transfering: {PrimaryObject} is {obj1} and {transferObject}. holstered object is {holsteredObject} which should be {obj2}");
            unHolster(obj2);
            print($"update: {PrimaryObject} is now the primary object = {obj1} and {obj2}. {transferObject} should not be different from {holsteredObject} {obj2}");
            Holster(obj1);
            print($"update: {obj1} should be {holsteredObject} and {obj2} should be {PrimaryObject}");
            holsterBool = true;
            print($"Weapon: {obj1} is holstered and {obj2} is unholstered");
        }
        else
        {
            print($"The user does not have a second gun");
        }
    }
    public void newGun(GameObject obj1, GameObject obj2)
        //sets current gun to inactive and the picked up gun as active
        // not currently in use, needs to be used with the equip function.
    {
        print($"Store: {obj1} and equip {obj2}");
        Holster(obj1);
        Equip(obj2);
        holsterBool = true;
    }

}
