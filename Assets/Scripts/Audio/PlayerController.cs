using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

namespace AudioScript
{
    public class PlayerController : MonoBehaviour
    {
        public string Grab_Plastic = "Play_Grab_Plastic";
        public string hoverSoundEvent = "Play_EcoDef_Hover";
        public string DropSoundEvent = "Play_EcoDef_Drop";

        private bool toggleInventory;
        private float defaultRay;
        private Collider handCollider;

        [SerializeField] private GameObject inventoryCanvas;
        [SerializeField] private GameObject InvRayInteractorRef;
        [SerializeField] private GameObject PlayerRayInteractorRef;
        
        [SerializeField] private float timeTillCollisionEnabled = 2f;
        [SerializeField] private InputActionReference Inventory;

        public bool InventoryOn => toggleInventory;

        private string _grabTag;
        private static readonly string GRABBED = "Grabbed";

        public void Start()
        {
            handCollider = GetComponent<Collider>();
            defaultRay = PlayerRayInteractorRef.GetComponent<XRRayInteractor>().maxRaycastDistance;
            toggleInventory = false;
        }

        public void ToggleInventory(InputAction.CallbackContext obj)
        {
            if (toggleInventory)
            {
                InvRayInteractorRef.SetActive(false);
                PlayerRayInteractorRef.GetComponent<XRRayInteractor>().maxRaycastDistance = defaultRay;
                PlayerRayInteractorRef.SetActive(true);
                toggleInventory = false;
            } else
            {
                InvRayInteractorRef.SetActive(true);
                PlayerRayInteractorRef.GetComponent<XRRayInteractor>().maxRaycastDistance = 0;
                PlayerRayInteractorRef.SetActive(false);
                toggleInventory = true;
            }

            inventoryCanvas.SetActive(toggleInventory);
        }

        private void OnEnable()
        {
            Inventory.action.performed += ToggleInventory;
        }

        private void OnDisable()
        {
            Inventory.action.performed -= ToggleInventory;
        }

        public void OnGrab(SelectEnterEventArgs args)
        {
            print("Grab");
            if (!string.IsNullOrEmpty(Grab_Plastic))
            {
                // Trigger the Wwise event
                AkSoundEngine.PostEvent(Grab_Plastic, gameObject);
            }
            else
            {
                Debug.LogWarning("Wwise Event name is not set!");
            }

            _grabTag = args.interactableObject.transform.tag;
            args.interactableObject.transform.tag = "Grabbed";
            handCollider.enabled = false;
        }

        public void OnRelease(SelectExitEventArgs args)
        {
            args.interactableObject.transform.tag = _grabTag;

            StartCoroutine(ReleaseTimer());
            AkSoundEngine.PostEvent(DropSoundEvent, gameObject);
        }

        public void OnHover(HoverEnterEventArgs args)
        {
            print(args.interactableObject.transform.tag);
            if (!args.interactableObject.transform.CompareTag(GRABBED))
            {
                print("Hover");
                AkSoundEngine.PostEvent(hoverSoundEvent, gameObject);
            }
        }

        public IEnumerator ReleaseTimer()
        {
            yield return new WaitForSeconds(timeTillCollisionEnabled);

            handCollider.enabled = true;
        }
    }

}
