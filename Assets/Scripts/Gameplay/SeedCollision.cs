using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SeedCollision : MonoBehaviour
{
    public GameObject flowerCube;
    [SerializeField] GameObject[] plantList;
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
        if (plantList == null) 
        {
            plantList = new GameObject[0];
        }
    }

    /// <summary>
    /// Spawns flower where seed contacts the ground
    /// </summary>
    /// <param name="position">Position of where the flower should be, i.e., contact point</param>
    private void SpawnFlower(Vector3 position)
    {
        // Use random plant from list if there are any
        if(plantList.Length > 0)
        {
            int randIndex = Random.Range(0,plantList.Length);
            Instantiate(plantList[randIndex], position, Quaternion.identity);
        }
        // Otherwise, use default flower cube
        else 
        {
            Instantiate(flowerCube, position, Quaternion.identity);
        }
    }
}
