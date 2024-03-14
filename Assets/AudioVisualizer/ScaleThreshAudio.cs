using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleThreshAudio : MonoBehaviour
{
    public int _band;
    public bool _useBand64 = false;
    public float _amplify = 1f;
    public Vector3 _startingScale = Vector3.one;
    public Vector3 _newScale = Vector3.one * 5;
    public float threshold = 0.5f;
    public float delay = 0.5f;
    public bool isScaled = false;
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
                isScaled = !isScaled;
                currentTime = 0f;
                StartCoroutine("Transition");
            }
        }
        else
            currentTime += Time.deltaTime;
    }

    private IEnumerator Transition()
    {
        if (isScaled)
        {
            while (currentTime < transitionTime)
            {
                transform.localScale = Vector3.Lerp(_startingScale, _newScale, currentTime / transitionTime);
                yield return null;
            }
            transform.localScale = _newScale;
        }
        else
        {
            while (currentTime < transitionTime)
            {
                transform.localScale = Vector3.Lerp(_newScale, _startingScale, currentTime / transitionTime);
                yield return null;
            }
            transform.localScale = _startingScale;
        }
    }
}
