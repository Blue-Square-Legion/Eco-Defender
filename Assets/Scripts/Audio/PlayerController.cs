using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AudioScript
{
    public class PlayerController : MonoBehaviour
    {
        public string Grab_Plastic = "Play_Grab_Plastic";

        private Collider handCollider;

        [SerializeField] private float timeTillCollisionEnabled = 2f;

        public void Start()
        {
            handCollider = GetComponent<Collider>();
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
