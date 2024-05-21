using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    public string OnMoveStartEvent = "Play_Slime_Footstep_Loop";
    public string OnMoveEndEvent = "Stop_Slime_Footstep_Loop";
    public string OnDamagedEvent = "Play_Slime_BodyHit";
    public string OnDeathEvent = "Play_Slime_Dead";
    public void OnMoveStart()
    {
        AkSoundEngine.PostEvent(OnMoveStartEvent, gameObject);
        print("OnMoveStar");
    }
    public void OnMoveEnd()
    {
        AkSoundEngine.PostEvent(OnMoveEndEvent, gameObject);
        print("OnMoEnd");
    }
    public void OnDamaged(float health)
    {
        AkSoundEngine.PostEvent(OnDamagedEvent, gameObject);
        print($"OnDamaged {health}");
    }
    public void OnDeath()
    {
        AkSoundEngine.PostEvent(OnDeathEvent, gameObject);
        print("OnDeath");
    }

}
