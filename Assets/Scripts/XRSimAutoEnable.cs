using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRSimAutoEnable : MonoBehaviour
{
    [SerializeField, Tooltip("In editor, will always deacitave the XR Sim, in build, will always activate the XR Sim")] private bool _overrideActive;
    [SerializeField] GameObject _xrSim;

    [SerializeField] private XRBaseControllerInteractor.InputTriggerType _editorOverride = XRBaseControllerInteractor.InputTriggerType.Toggle;
    // Start is called before the first frame update
    void Start()
    {
        // TODO: See if we can pickup the device from editor/build 
        //       and automatically figure out if need to activate.
        if(_xrSim)
        {
#if UNITY_EDITOR
        _xrSim.SetActive(!_overrideActive);

        OverrideSelect(_editorOverride);

#else
        _xrSim.SetActive(_overrideActive);
#endif
        }
        else 
        {
            Debug.LogWarning("No XR Sim assigned!");
        }
    }


    private void OverrideSelect(XRBaseControllerInteractor.InputTriggerType value)
    {
        //XRBaseControllerInteractor are inactive at start? 
        XRBaseControllerInteractor[] list = gameObject.GetComponentsInChildren<XRBaseControllerInteractor>(true);
        print(list.Length);
        foreach (XRBaseControllerInteractor item in list)
        {
            item.selectActionTrigger = value;
        }
    }
}
