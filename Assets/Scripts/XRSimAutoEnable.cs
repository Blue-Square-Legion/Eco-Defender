using UnityEngine;

public class XRSimAutoEnable : MonoBehaviour
{
    [SerializeField, Tooltip("In editor, will always deacitave the XR Sim, in build, will always activate the XR Sim")] private bool _overrideActive;
    [SerializeField] GameObject _xrSim;
    // Start is called before the first frame update
    void Start()
    {
        // TODO: See if we can pickup the device from editor/build 
        //       and automatically figure out if need to activate.
#if UNITY_EDITOR
        _xrSim.SetActive(!_overrideActive);
#else
        _xrSim.SetActive(_overrideActive);
#endif
    }
}
