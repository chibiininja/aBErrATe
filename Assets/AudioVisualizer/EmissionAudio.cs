using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmissionAudio : MonoBehaviour
{
    public int _band;
    public bool _useBuffer;
    public bool _useBand64 = false;
    public float _amplify = 1f;
    public Color _color = new Color(1f, 1f, 1f);
    private Material _material;

    // Start is called before the first frame update
    void Start()
    {
        _material = new Material(Shader.Find("Standard"));
        _material.hideFlags = HideFlags.HideAndDontSave;
        _material.SetColor("_Color", _color);
        _material.EnableKeyword("_EMISSION");
        GetComponent<MeshRenderer>().material = _material;
    }

    // Update is called once per frame
    void Update()
    {
        if (_useBuffer)
        {
            if (_useBand64)
            {
                float audioBandBuffer = Mathf.Clamp(AudioVisualizer.instance._audioBandBuffer64[_band] * _amplify, 0f, 1f);
                Color _emissionColor = new Color(audioBandBuffer, audioBandBuffer, audioBandBuffer);
                _material.SetColor("_EmissionColor", _emissionColor);
            }
            else
            {
                float audioBandBuffer = Mathf.Clamp(AudioVisualizer.instance._audioBandBuffer[_band] * _amplify, 0f, 1f);
                Color _emissionColor = new Color(audioBandBuffer, audioBandBuffer, audioBandBuffer);
                _material.SetColor("_EmissionColor", _emissionColor);
            }
        }
        else
        {
            if (_useBand64)
            {
                float audioBand = Mathf.Clamp(AudioVisualizer.instance._audioBand64[_band] * _amplify, 0f, 1f);
                Color _emissionColor = new Color(audioBand, audioBand, audioBand);
                _material.SetColor("_EmissionColor", _emissionColor);
            }
            else
            {
                float audioBand = Mathf.Clamp(AudioVisualizer.instance._audioBand[_band] * _amplify, 0f, 1f);
                Color _emissionColor = new Color(audioBand, audioBand, audioBand);
                _material.SetColor("_EmissionColor", _emissionColor);
            }
        }
        
    }
}
