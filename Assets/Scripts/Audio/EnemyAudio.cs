using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    public void OnMoveStart()

    {
        print("OnMoveStar");
       

    }
    public void OnMoveEnd()

    {
       print("OnMoEnd");


    }

    public  void  OnDamaged()

    {
      print("OnDamaged");


    }
    public void OnDeath()
    {
        print("OnDeath");
    }





}
