using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelCleared : MonoBehaviour
{

    public GameObject Player;
    public TextMeshProUGUI LevelText;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
       LevelText = GetComponent<TextMeshProUGUI>();
        LevelText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
  public  void OnCollision(Collider other)
    {
        if (other.gameObject.name == "Player" && Input.GetKey(KeyCode.Space)) ;
        {
            LevelText.enabled=(true);
        }
    }

}