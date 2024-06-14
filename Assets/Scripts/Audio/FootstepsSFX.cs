using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.Events;

public class FootstepsSFX : MonoBehaviour
{

    [SerializeField]private FirstPersonController FPS;
    [SerializeField]private CharacterController playerController;

    public string OnMoveStartEvent = "Play_Slime_Footstep_Loop";
    public string OnMoveEndEvent = "Stop_Slime_Footstep_Loop";
    
    private bool playerWasMoving = false;

    public UnityEvent PlayFootsteps;
    public UnityEvent StopPlayingFootsteps;

    RaycastHit floorMatCheck;

    // Start is called before the first frame update
    void Start()
    {
        if (FPS == null) 
        {
            FPS = GetComponent<FirstPersonController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        PlayerFootSteps();
    }
    private void PlayerFootSteps() 
    {
        bool isCurMoving = IsPlayerMoving();
       
        if (playerWasMoving != isCurMoving)
        {
            playerWasMoving = isCurMoving;
            if (isCurMoving)
            {
                CheckFloorMaterial();
            }
            else
            {
                OnPlayerStop();
            }
        }
    }

    private bool IsPlayerMoving()
    {
        return FPS.Grounded && playerController.velocity != Vector3.zero;
    }

    private void CheckFloorMaterial() 
    {
        Physics.Raycast(gameObject.transform.position, Vector3.down, out floorMatCheck, Mathf.Infinity, FPS.GroundLayers);

        if (floorMatCheck.collider.gameObject.layer.Equals(LayerMask.NameToLayer("Floor - Metal")))
            Debug.Log("You're on metal");
        else if (floorMatCheck.collider.gameObject.layer.Equals(LayerMask.NameToLayer("Floor - Dirt")))
            Debug.Log("You're on dirt");
        else if (floorMatCheck.collider.gameObject.layer.Equals(LayerMask.NameToLayer("Floor - Concrete")))
            Debug.Log("You're on concrete");
        else
            OnPlayerStart();
    }

    private void OnPlayerStart() 
    {
        AkSoundEngine.PostEvent(OnMoveStartEvent, gameObject);
        print("PlayerMoveStar");
        PlayFootsteps?.Invoke();
    }

    private void OnPlayerStop() 
    {
        AkSoundEngine.PostEvent(OnMoveEndEvent, gameObject);
        print("PlayerMoEnd");
        StopPlayingFootsteps?.Invoke();
    }
}
