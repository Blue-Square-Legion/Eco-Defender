using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class SphereGizmos : MonoBehaviour
{
#if UNITY_EDITOR
    [SerializeField] private Color _color = Color.green;

    private float _radius;

    private void OnValidate()
    {
        _radius = GetComponent<SphereCollider>().radius;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = _color;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }

#endif
}
