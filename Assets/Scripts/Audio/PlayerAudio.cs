using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public void OnPlayerHealth(float health)
    {
        print($"Player Health Changed: {health}");
    }
}