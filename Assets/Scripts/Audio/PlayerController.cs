using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

namespace AudioScript
{
    public class PlayerController : MonoBehaviour
    {
        public string Grab_Plastic = "Play_Grab_Plastic";

        private bool toggleInventory;
        private float defaultRay;
        private Collider handCollider;

        [SerializeField] private GameObject inventoryCanvas;
        [SerializeField] private GameObject InvRayInteractorRef;
        [SerializeField] private GameObject PlayerRayInteractorRef;
        [SerializeField] private float timeTillCollisionEnabled = 2f;
        [SerializeField] private InputActionReference Inventory;

        public bool InventoryOn => toggleInventory;

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

        public void OnGrab()
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

            handCollider.enabled = false;
        }

        public void OnRelease()
        {
            StartCoroutine(ReleaseTimer());            
        }

        public void OnHover()
        {
            print("Hover");
        }

        public IEnumerator ReleaseTimer()
        {
            yield return new WaitForSeconds(timeTillCollisionEnabled);

            handCollider.enabled = true;
        }
    }

}
