using UnityEngine;
using UnityEngine.Events;

public class TriggerZone : MonoBehaviour
{
    public UnityEvent OnEnter;
    public UnityEvent OnExit;

    [SerializeField] private string _playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_playerTag))
        {
            OnEnter.Invoke();
            Debug.Log("Portal Entered");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(_playerTag))
        {
            OnExit.Invoke();
            Debug.Log("Portal Exit");
        }
    }
}
