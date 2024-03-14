using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAmplitude : MonoBehaviour
{
    public float _startingIntensity = 1f;
    public float _newIntensity = 1.5f;

    private float _intensityDifference;
    private Light _light;

    // Update is called once per frame
    void Update()
    {
        _intensityDifference = _newIntensity - _startingIntensity;
        GetComponent<Light>().intensity = AudioVisualizer.instance._amplitudeBuffer * _intensityDifference + _startingIntensity;
    }
}
