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

    public GameObject PrimaryObject;
    [SerializeField] private Transform handSlot;
    [SerializeField] private LayerMask BlockMask, UsableMask;
    [SerializeField] private float radius = 2f, distance = 100f;

    private IUsable usable;
    private Transform camera;

    [SerializeField] private GameObject hoverObject;

    private void Awake()
    {
        SetObject(PrimaryObject ?? null);

        camera = Camera.main.transform;
    }

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

    public void OnShoot()
    {
        usable?.Use();
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
            PointerEventData pointerData = new(EventSystem.current);

            pointerData.position = Input.mousePosition;
            List<RaycastResult> results = new();
            EventSystem.current.RaycastAll(pointerData, results);

            foreach (RaycastResult res in results)
            {
                print(res);
                if (res.gameObject.TryGetComponent<IPointerClickHandler>(out IPointerClickHandler handler))
                {
                    pointerData.button = PointerEventData.InputButton.Left;
                    handler.OnPointerClick(pointerData);
                    return;
                }
            }

            return;
        }

        if (hoverObject.TryGetComponent<IEquip>(out IEquip equip))
        {
            Equip(hoverObject);
        }
        else if (hoverObject.transform.parent && hoverObject.transform.parent.TryGetComponent<IEquip>(out equip))
        {
            Equip(hoverObject.transform.parent.gameObject);
        }
        else if (hoverObject.TryGetComponent<IUsable>(out IUsable usable))
        {
            print($"Use:{gameObject.name}");
            usable?.Use();
        }
        else
        {
            PointerEventData pointerData = new(EventSystem.current);

            pointerData.position = Input.mousePosition;
            List<RaycastResult> results = new();
            EventSystem.current.RaycastAll(pointerData, results);

            foreach (RaycastResult res in results)
            {
                print(res);
                if (res.gameObject.TryGetComponent<IPointerClickHandler>(out IPointerClickHandler handler))
                {
                    pointerData.button = PointerEventData.InputButton.Left;
                    handler.OnPointerClick(pointerData);
                    return;
                }
            }

            return;
        }
    }

    public void Equip(GameObject obj)
    {
        print($"Equip: {obj}");
        if (PrimaryObject) UnEquip(PrimaryObject);

        obj.GetComponent<IEquip>().Equip(true);

        obj.transform.position = handSlot.position;
        obj.transform.rotation = handSlot.rotation;
        obj.transform.SetParent(handSlot, true);

        SetObject(obj);
    }

    public void UnEquip(GameObject obj)
    {
        print($"UnEquip: {obj}");
        obj.GetComponent<IEquip>().UnEquip();
        obj.transform.SetParent(null, true);

        SetObject(null);
    }
}
