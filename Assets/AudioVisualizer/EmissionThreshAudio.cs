using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmissionThreshAudio : MonoBehaviour
{
    public int _band;
    public bool _useBand64 = false;
    public float _amplify = 1f;
    public Color _color = new Color(1f, 1f, 1f);
    private Material _material;

    public Color _startingIntensity = new Color(0f, 0f, 0f);
    public Color _newIntensity = new Color(1f, 1f, 1f);
    public float threshold = 0.2f;
    public float delay = 1.8f;
    public float transitionTime = .2f;

    private float currentTime = 0f;

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
        if (currentTime > delay)
        {
            float audioBand = _useBand64 ?
                Mathf.Clamp(AudioVisualizer.instance._audioBand64[_band] * _amplify, 0f, 1f) :
                Mathf.Clamp(AudioVisualizer.instance._audioBand[_band] * _amplify, 0f, 1f);
            if (audioBand > threshold)
            {
                currentTime = 0f;
                StartCoroutine("Transition");
            }
        }
        else
            currentTime += Time.deltaTime;
    }

    private IEnumerator Transition()
    {
        while (currentTime < transitionTime)
        {
            Color _emissionColor = Color.Lerp(_newIntensity, _startingIntensity, currentTime / transitionTime);
            _material.SetColor("_EmissionColor", _emissionColor);
            yield return null;
        }
        _material.SetColor("_EmissionColor", _startingIntensity); ;
    }
}