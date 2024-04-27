using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace AudioScript
{
    public class PlayerController : MonoBehaviour
    {
        public string Grab_Plastic = "Play_Grab_Plastic";
        public string hoverSoundEvent = "Play_EcoDef_Hover";
        public string DropSoundEvent = "Play_EcoDef_Drop";

        private Collider handCollider;


        [SerializeField] private float timeTillCollisionEnabled = 2f;

        private string _grabTag;
        private static readonly string GRABBED = "Grabbed";

        public void Start()
        {
            handCollider = GetComponent<Collider>();
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
