using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHoverSound : MonoBehaviour
{
    public string clickHoverEvent = "Play_EcoDef_Hover";

    private void OnMouseEnter()
    {
        PlayHoverSound();
    }
    public void PlayHoverSound()
    {
        AkSoundEngine.PostEvent(clickHoverEvent, gameObject);
    }
}

