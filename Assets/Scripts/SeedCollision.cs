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
        SpawnFlower();
        Destroy(this.gameObject);
    }

    private void SpawnFlower()
    {
        Instantiate(flowerCube, transform.position, transform.rotation);
    }
}
