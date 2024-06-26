using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.Events;

public class FootstepsSFX : MonoBehaviour
{

    [SerializeField] private FirstPersonController FPS;
    [SerializeField] private CharacterController playerController;
    [SerializeField] private string curFloorMat;

    public string OnMoveStartEvent;
    public string OnMoveEndEvent;
    


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
                OnPlayerStart();
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
        Physics.Raycast(gameObject.transform.position, Vector3.down, out floorMatCheck, Mathf.Infinity, FPS.GroundLayers);///I was thinking the sounds could be named something like "Play_Player_Footsteps_[InsertFloorMatHere]_Loop" 
        //string newFloorMat;

        if (floorMatCheck.collider.gameObject.layer.Equals(LayerMask.NameToLayer("Floor - Metal")))
        {
            Debug.Log("You're on metal");
            //newFloorMat = "Metal";
        }
        else if (floorMatCheck.collider.gameObject.layer.Equals(LayerMask.NameToLayer("Floor - Dirt")))
        {
            Debug.Log("You're on dirt");
            //newFloorMat = "Dirt";
        }
        else if (floorMatCheck.collider.gameObject.layer.Equals(LayerMask.NameToLayer("Floor - Concrete")))
        {
            Debug.Log("You're on concrete");
            //newFloorMat = "Concrete";
        }
        else
        {
            Debug.Log("You're on other floor");
            //newFloorMat = "Default"
        }

        ///if(newFloorMat!=curFloorMat)
        ///{
        ///     OnPlayerStop();//stops current footstep loop
        ///     OnMoveStartEvent = $"Play_Player_Footsteps_[newFloorMat]_Loop";
        ///     OnMoveEndEvent = $"Stop_Player_Footsteps_[newFloorMat]_Loop";
        ///     curFloorMat = newFloorMat;
        ///}     

    }

    private void OnPlayerStart() 
    {
        AkSoundEngine.PostEvent(OnMoveStartEvent, gameObject);
        print("PlayerMoveStart");
        PlayFootsteps?.Invoke();
    }

    private void OnPlayerStop() 
    {
        AkSoundEngine.PostEvent(OnMoveEndEvent, gameObject);
        print("PlayerMoveEnd");
        StopPlayingFootsteps?.Invoke();
    }
}