using System;
using UnityEngine;

/// <summary>
/// Light Controller for pollution system. Uses Trigger area.
/// </summary>

[RequireComponent(typeof(Collider))]
public class LightController : MonoBehaviour
{
    [Serializable]
    public struct LightSetting
    {
        public Color color;
        public float intensity;

        public LightSetting(Color color, float intensity) : this()
        {
            this.color = color;
            this.intensity = intensity;
        }
    }

    [SerializeField] private Light _light;
    [SerializeField] private LayerMask _layerMask;

    [Space(5)]
    [SerializeField] private LightSetting _targetSetting = new(Color.white, 1f);
    [SerializeField, Min(0.1f)] private float _transitionTime = 1f;

    [SerializeField] private AnimationCurve _curve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    private LightSetting _defaultSetting;

    private float _time = 0f;
    private Action<float> _curveAnimFn;
    private bool _inside = false, _enableEffect = true;
    private Action _transisitionEnd;

    private void Awake()
    {
        _defaultSetting = new(_light.color, _light.intensity);
        enabled = false;

        if (_light == null)
        {
            Debug.LogError($"{name} - Light Not set : Disabled Effect");
            _enableEffect = false;
        }
    }

    //Use FixedUpdate as psuedo timeline
    private void FixedUpdate()
    {
        _time += Time.fixedDeltaTime;

        if (_time >= _transitionTime || _time <= 0)
        {
            _time = Mathf.Clamp01(_time);
            _curveAnimFn(_curve.Evaluate(_time));
            _transisitionEnd?.Invoke();
            enabled = false;
        }

        _curveAnimFn(_curve.Evaluate(_time));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_inside || !_enableEffect) return;
        if (((1 << other.gameObject.layer) & _layerMask) == 0) return;

        _inside = true;
        BlendToTarget();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!_inside || !_enableEffect) return;
        if (((1 << other.gameObject.layer) & _layerMask) == 0) return;

        _inside = false;
        BlendToDefault();
    }

    #region Light Setter
    private void SetLight(LightSetting lightSetting)
    {
        _light.color = lightSetting.color;
        _light.intensity = lightSetting.intensity;
    }


    //Handles Animation curve for timeline
    private void PlayCurve(Action<float> cb)
    {
        _time = 0;
        _curveAnimFn = cb;
        enabled = true;
    }

    private Action<float> MakeBlendFn(LightSetting start, LightSetting end)
    {
        return (float time) => BlendLight(time, start, end);
    }

    private void BlendLight(float alpha, LightSetting start, LightSetting end)
    {
        _light.color = Color.Lerp(start.color, end.color, alpha);
        _light.intensity = Mathf.Lerp(start.intensity, end.intensity, alpha);
    }
    #endregion

    #region Public Accessor Methods
    public void EndLightEffect()
    {
        _enableEffect = false;
        if (_inside)
        {
            BlendToDefault();
        }
        else
        {
            SetLight(_defaultSetting);
        }
    }

    public void StartLightEffect()
    {
        _enableEffect = true;
        _inside = false;
    }

    public void BlendToDefault()
    {
        PlayCurve(MakeBlendFn(_targetSetting, _defaultSetting));
    }

    public void BlendToTarget()
    {
        PlayCurve(MakeBlendFn(_defaultSetting, _targetSetting));
    }
    #endregion
}

