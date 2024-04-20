using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private PortalManager _portalManager;
    [SerializeField] GameObject startButton;
    [SerializeField] GameObject optionsButton;
    [SerializeField] SceneManagerSO _Scene;

    // Start is called before the first frame update
    void Start()
    {
        startButton.SetActive(true);
        // Make sure the portal animation is not player and in close mode for when a player comes back from level
        _Scene.AsyncUnload();
    }

    public void StartLevel()
    {
        startButton.SetActive(false);
        // Play portal animation
        _portalManager.Open(_Scene);
    }
}
