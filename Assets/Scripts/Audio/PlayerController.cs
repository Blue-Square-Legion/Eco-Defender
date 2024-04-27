using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AudioScript
{
    public class PlayerController : MonoBehaviour
    {
        public string Grab_Plastic = "Play_Grab_Plastic";

        private Collider handCollider;

        [SerializeField] private GameObject inventoryCanvas;
        [SerializeField] private float timeTillCollisionEnabled = 2f;

        private XRIDefaultInputActions inputActionOpenInv;

        public void Start()
        {
            handCollider = GetComponent<Collider>();

            inputActionOpenInv = new XRIDefaultInputActions();

            inputActionOpenInv.XRILeftHandInteraction.OpenInventory.started += ToggleInventory;
            inputActionOpenInv.XRIRightHandInteraction.OpenInventory.started += ToggleInventory;
        }

        public void ToggleInventory(InputAction.CallbackContext obj)
        {
            if (inventoryCanvas.activeSelf)
            {
                inventoryCanvas.SetActive(false);
            } else
            {
                inventoryCanvas.SetActive(true);
            }
        }

        private void OnEnable()
        {
            if (inputActionOpenInv != null)
            {
                inputActionOpenInv.Enable();
            }
        }

        private void OnDisable()
        {
            if (inputActionOpenInv != null)
            {
                inputActionOpenInv.Disable();
            }
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
