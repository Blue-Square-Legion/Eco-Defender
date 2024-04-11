using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace EventSO
{
    public class EventListener : MonoBehaviour
    {
        public EventSO Event;

        public UnityEvent UnityEvent;

        private void OnEnable()
        {
            Event.AddEventListener(UnityEvent.Invoke);
        }

        private void OnDisable()
        {
            Event.RemoveEventListener(UnityEvent.Invoke);
        }
    }
}