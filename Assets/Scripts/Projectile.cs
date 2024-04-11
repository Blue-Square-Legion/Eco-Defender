using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;


[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [SerializeField, Tooltip("Starting Force on spawn. Also Adjust mass")] private float _startForce = 200f;

    public UnityEvent OnHit;

    private Rigidbody _rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.AddForce(transform.forward * _startForce);
    }

    private void OnCollisionEnter(Collision collision)
    {
        OnHit.Invoke();
    }
}
