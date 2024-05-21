using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public string clickHoverEvent = "Play_EcoDef_Hover";
    public string grabEvent = "Play_Grab_Plastic";
    public void OnPlayerHealth(float health)
    {
        print($"Player Health Changed: {health}");
    }

    public void OnHoverEnter()
    {
        AkSoundEngine.PostEvent(clickHoverEvent, gameObject);
    }

    public void OnHoverExit()
    {
    }

    public void OnPickupItem()
    {
        AkSoundEngine.PostEvent(grabEvent, gameObject);
    }
}
