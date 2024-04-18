using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject startButton;
    [SerializeField] GameObject optionsButton;

    // Start is called before the first frame update
    void Start()
    {
        startButton.SetActive(true);
        // Make sure the portal animation is not player and in close mode for when a player comes back from level
    }

    public void StartLevel()
    {
        startButton.SetActive(false);
        // Play portal animation

    }


}
