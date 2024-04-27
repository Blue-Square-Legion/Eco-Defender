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
    [SerializeField, Range(0, 1), Tooltip("Inside Pollution light intensity")] private float _minIntensity = 0f, _maxIntensity = 1f;

    [SerializeField, Tooltip("Transition blend curve")] private AnimationCurve _curve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    private LightSetting _defaultSetting;

    private float _currentBlend = 0f, _targetBlend = 0f, _insideBlend = 1f;
    private Action<float> _curveAnimFn;
    private bool _inside = false, _enableEffect = true;

    private void Awake()
    {
        // First, try finding the light
        if (!_light) _light = FindObjectOfType<Light>();
        if (_light == null)
        {
            Debug.LogError($"{name} - Light Not set : Disabled Effect");
            _enableEffect = false;
        }

        _defaultSetting = new(_light.color, _light.intensity);
        enabled = false;

    }

    //Use FixedUpdate as psuedo timeline
    private void FixedUpdate()
    {
        _curveAnimFn(Time.fixedDeltaTime);
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


    //Handles Animation curve for psuedo timeline
    private void PlayCurve(Action<float> cb)
    {
        _curveAnimFn = cb;
        enabled = true;
    }

    //Should Improve later. Blends to target based on time & disables component once done.
    private Action<float> MakeBlendFn(float target)
    {
        _targetBlend = target;

        if (_currentBlend < _targetBlend)
        {
            //increase to target
            return (float deltaTime) =>
            {
                _currentBlend += deltaTime / _transitionTime;

                if (_currentBlend >= _targetBlend)
                {
                    enabled = false;
                    _currentBlend = _targetBlend;
                }

                BlendLight(_curve.Evaluate(_currentBlend), _defaultSetting, _targetSetting);
            };
        }
        else
        {
            //decrease to target
            return (float deltaTime) =>
            {
                _currentBlend -= deltaTime / _transitionTime;

                if (_currentBlend <= _targetBlend)
                {
                    enabled = false;
                    _currentBlend = _targetBlend;
                }

                BlendLight(_curve.Evaluate(_currentBlend), _defaultSetting, _targetSetting);
            };
        }
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
        PlayCurve(MakeBlendFn(0));
    }

    public void BlendToTarget()
    {
        //Caps blended change by min / max
        float intesity = Mathf.Lerp(_minIntensity, _maxIntensity, _insideBlend);
        PlayCurve(MakeBlendFn(intesity));
    }

    public void Blend(float target)
    {
        if (_inside && _insideBlend != target)
        {
            _insideBlend = target;
            BlendToTarget();
        }
    }
    #endregion
}

