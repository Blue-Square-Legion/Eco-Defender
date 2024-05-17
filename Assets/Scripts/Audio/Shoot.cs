using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AudioScript
{
    public class Shoot : MonoBehaviour
    {
        public string Gun_PlantBombSingleShoot = "Play_PlantBombOneShot";
        public string Gun_Empty = "Play_Gun_Empty";
        public void OnShoot()
        {
            print("Shoot");
            if (!string.IsNullOrEmpty(Gun_PlantBombSingleShoot))
            {
                // Trigger the Wwise event
             //   AkSoundEngine.PostEvent(Gun_PlantBombSingleShoot, gameObject);
            }
            else
            {
                Debug.LogWarning("Wwise Event name is not set!");
            }

        }

        public void OnEmpty()
        {
            //TODO Implementation for empty shot
            if (!string.IsNullOrEmpty(Gun_Empty))
            {
                // Trigger the Wwise event
              //  AkSoundEngine.PostEvent(Gun_Empty, gameObject);
            }
            else
            {
                Debug.LogWarning("Wwise Event name is not set!");
            }
        }
    }
}

