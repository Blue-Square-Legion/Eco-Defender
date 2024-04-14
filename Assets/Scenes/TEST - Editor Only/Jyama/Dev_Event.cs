using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DevTool
{
    public class Dev_Event : MonoBehaviour
    {
        public KeyCode key = KeyCode.R;

        public UnityEvent Event;

        private void Update()
        {
            if (Input.GetKeyDown(key))
            {
                Event.Invoke();
            }
        }
    }
}

