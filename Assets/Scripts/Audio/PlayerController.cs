using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AudioScript
{
    public class PlayerController : MonoBehaviour
    {
        public string Grab_Plastic = "Play_Grab_Plastic";
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
        }

        public void OnHover()
        {
            print("Hover");
        }
    }

}
