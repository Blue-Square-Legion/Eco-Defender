using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;


public class Move1 : MonoBehaviour
{
    Vector3 direction;
    /// <summary>
    //public GameObject tad;
    /// </summary
    public float speed = 1.5f;
    public float Limit = 75f;
    public bool restart = false;
    private float random = .5f;
    public float moveSpeed = 1;
    public Rigidbody m_Rb;
    //  public GameObject Player;
    public GameObject Cube;
    void Awake()
    {
        m_Rb = GetComponent<Rigidbody>();

    }
    private void Start()
    {
        random = Random.Range(0f, 1f);
        //  Player = GameObject.FindGameObjectWithTag("Player");
        Cube = GameObject.FindGameObjectWithTag("Cube");
        Cube.transform.position = new Vector3(direction.x, direction.z, 0);
    }
  /*
   void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Cube.transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Cube.transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Cube.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            Cube.transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
        }
        //  Player.transform.position = Vector3.MoveTowards(Player.transform.position,Cube.transform.position, 2);
        //  float angle2 = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        // float angle = Limit * Mathf.Sin(Time.deltaTime * random * speed);
        //  Cube.transform.localRotation = Quaternion.Euler( 0, 0,angle);
        //  Cube.transform.localRotation = Quaternion.Euler(0,angle2, 0);
    }
*/
    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, verticalInput);
        movementDirection.Normalize();

        transform.Translate(movementDirection * speed * Time.deltaTime, Space.World);

        Vector3 playerPos= m_Rb.position;
        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput);
        if (movementDirection != Vector3.zero)
        {


            Quaternion targetRotation = Quaternion.LookRotation(movement);

         //   targetRotation = Quaternion.RotateTowards(targetRotation, 360 *Time.deltaTime);


            m_Rb.MoveRotation(targetRotation);
            m_Rb.MovePosition(playerPos + movement * moveSpeed * speed * Time.fixedDeltaTime);
        }

    }
}