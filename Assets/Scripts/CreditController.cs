using UnityEngine;

public class CreditController : MonoBehaviour
{
    private Camera _camera;
    private Vector3 _rotation, _target, _velocity = Vector3.zero;
    private float _distance;

    [SerializeField] private bool _keepInView = true;

    [Space(10)]
    [SerializeField] private bool _useHeightRange = true;
    [SerializeField] private float _minHeight = 50, _maxHeight = 100;

    [Space(10)]
    [SerializeField] private bool _useDampening = true;
    [SerializeField] private float _dampeningTime = 0.25f;

    private void Awake()
    {
        _camera = Camera.main;
        _rotation = transform.rotation.eulerAngles;
        _distance = transform.position.z;

        enabled = _keepInView;
    }

    private void Update()
    {
        //get forward based on horizontal plane
        _target = _distance * Vector3.ProjectOnPlane(_camera.transform.forward, Vector3.up).normalized;

        if (_useHeightRange) { ClampTarget(ref _target); }

        //Dappens screen movement
        if (_useDampening)
        {
            transform.position = Vector3.SmoothDamp(transform.position, _target, ref _velocity, _dampeningTime);
        }
        else
        {
            transform.position = _target;
        }


        FacePlayer();
    }

    private void ClampTarget(ref Vector3 target)
    {
        target.y = Mathf.Lerp(_minHeight, _maxHeight, _camera.transform.forward.y);
    }

    private void FacePlayer()
    {
        _rotation.y = _camera.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Euler(_rotation);
    }

    public void Stop()
    {
        gameObject.SetActive(false);
    }
}
