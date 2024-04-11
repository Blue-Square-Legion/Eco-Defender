using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SeedCollision : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject flowerCube;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Wow");
        // Assumes first contact is where to place item
        var point = collision.GetContact(0).point;
        SpawnFlower(point);
        Destroy(this.gameObject);
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
