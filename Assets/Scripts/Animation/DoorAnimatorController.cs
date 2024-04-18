using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimatorController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private string _openName = "Open";
    [SerializeField] private string _closeName = "Close";

    public float ClipLength { get { return _animator.GetCurrentAnimatorStateInfo(0).length; } }

    public void Open()
    {
        _animator.Play(_openName, 0);
    }

    public void Close()
    {
        _animator.Play(_closeName, 0);
    }
}
