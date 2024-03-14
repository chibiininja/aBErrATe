using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightThreshAudio : MonoBehaviour
{
    public int _band;
    public bool _useBand64 = false;
    public float _amplify = 1f;
    public float _startingIntensity = 0f;
    public float _newIntensity = 1f;
    public float threshold = 0.2f;
    public float delay = 1.8f;
    public bool isLit = false;
    public float transitionTime = .2f;

    private float currentTime = 0f;

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
                isLit = !isLit;
                currentTime = 0f;
                StartCoroutine("Transition");
            }
        }
        else
            currentTime += Time.deltaTime;
    }

    private IEnumerator Transition()
    {
        if (isLit)
        {
            while (currentTime < transitionTime)
            {
                GetComponent<Light>().intensity = Mathf.Lerp(_startingIntensity, _newIntensity, currentTime / transitionTime);
                yield return null;
            }
            GetComponent<Light>().intensity = _newIntensity;
        }
        else
        {
            while (currentTime < transitionTime)
            {
                GetComponent<Light>().intensity = Mathf.Lerp(_newIntensity, _startingIntensity, currentTime / transitionTime);
                yield return null;
            }
            GetComponent<Light>().intensity = _startingIntensity;
        }
    }
}
