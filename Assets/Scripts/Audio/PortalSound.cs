using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSound : MonoBehaviour
{
    public string portalSound = "Play_PortalOpen";

    public void PlayPortalOpenSound()
    {
        AkSoundEngine.PostEvent(portalSound, gameObject);
    }
}
