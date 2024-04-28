using UnityEngine;
using UnityEngine.UI;


public class UI : MonoBehaviour
{
    public string clickSoundEvent = "Play_EcoDef_Button_Click";

    public void PlayClickSound()
    {
        AkSoundEngine.PostEvent(clickSoundEvent, gameObject);
    }
}
