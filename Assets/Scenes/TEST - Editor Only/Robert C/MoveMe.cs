using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMe : MonoBehaviour
    {
    public Rigidbody m_Rb;
    private Vector3 playerPos;
    private float moveSpeed = 1.6f;
    void Awake()
    
    {
        m_Rb = GetComponent<Rigidbody>();
     //   m_CameraPos = followCamera.transform.position - transform.position; 
    }

     void FixedUpdate()
    {
    float horizontalInput=Input.GetAxis("Horizontal");
    float verticalInput = Input.GetAxis("Vertical");

    Vector3 movementDirection = new Vector3(horizontalInput , 0,verticalInput);
    movementDirection.Normalize();

    transform.Translate(movementDirection * moveSpeed * Time.deltaTime,Space.World);

    // Vector3 playerPos= m_Rb.position;
  
    if (movementDirection != Vector3.zero)
    {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.forward);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 5f * Time.deltaTime);



            //Quaternion targetRotation = Quaternion.LookRotation(movement);

            // targetRotation = Quaternion.RotateTowards(targetRotation, 360 * Time.fixedDeltaTime);

            //   m_Rb.MoveRotation(targetRotation);
            //   m_Rb.MovePosition(playerPos + movement * moveSpeed * moveSpeed * Time.fixedDeltaTime);
        }
    }
}