using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Dev_KeyBind : MonoBehaviour
{
    public UnityEvent Event;
    public InputActionReference InputAction;

    private void Awake()
    {
        InputAction.action.performed += (_) => Event.Invoke();
    }

}
