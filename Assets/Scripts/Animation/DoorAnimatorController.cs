using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DoorAnimatorController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private string _openName = "Open";
    [SerializeField] private string _closeName = "Close";

    public float ClipLength { get { return _animator.GetCurrentAnimatorStateInfo(0).length; } }

    private void Awake()
    {
        if(_animator == null)
        {
            _animator = GetComponent<Animator>();
        }
    }

    public void Open()
    {
        print("Open");
        _animator.Play(_openName, 0);
    }

    public void Close()
    {
        print("Close");
        _animator.Play(_closeName, 0);
    }
}
