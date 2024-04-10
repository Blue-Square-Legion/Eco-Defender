using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using EventSO;

public interface ICollectable
{
    public void OnCollected();
}


public class Collectable : MonoBehaviour
{
    public ItemSO Tag;
    [SerializeField] private EventItemSO _eventChannel;
    [Space(5)]
    public UnityEvent<ItemSO> OnCollectedEvent;

    public void OnCollected()
    {
        _eventChannel.Invoke(Tag);
        OnCollectedEvent.Invoke(Tag);
    }
}
