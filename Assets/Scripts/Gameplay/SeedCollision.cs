using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SeedCollision : MonoBehaviour
{
    public GameObject flowerCube;
    [SerializeField, Tooltip("Layer Item will Spawn")] private LayerMask _hitMask;

    private void OnCollisionEnter(Collision collision)
    {
        //Layer comparison using bitwise comparion
        if((_hitMask & (1 << collision.gameObject.layer)) != 0)
        {
            //Debug.Log("Wow");
            // Assumes first contact is where to place item
            var point = collision.GetContact(0).point;
            SpawnFlower(point);
            Destroy(this.gameObject);
        }       
    }

    /// <summary>
    /// Spawns flower where seed contacts the ground
    /// </summary>
    /// <param name="position">Position of where the flower should be, i.e., contact point</param>
    private void SpawnFlower(Vector3 position)
    {
        Instantiate(flowerCube, position, Quaternion.identity);
    }
}
