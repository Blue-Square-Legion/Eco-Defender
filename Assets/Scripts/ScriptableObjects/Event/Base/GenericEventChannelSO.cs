using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace EventSO
{
    public interface IEventDataSO<T>
    {
        public void Invoke(T data);
        public void AddEventListener(Action<T> Handler);
        public void RemoveEventListener(Action<T> Handler);
    }
    public abstract class GenericEventChannelSO<T> : ScriptableObject, IEventDataSO<T>
    {
        public Action<T> OnEvent;

#if UNITY_EDITOR
        public T _testData;
#endif

        public void Invoke(T data)
        {
            OnEvent?.Invoke(data);
        }
        public void AddEventListener(Action<T> Handler)
        {
            OnEvent += Handler;
        }
        public void RemoveEventListener(Action<T> Handler)
        {
            OnEvent -= Handler;
        }
    }
}

