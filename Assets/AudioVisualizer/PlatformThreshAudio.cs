using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformThreshAudio : MonoBehaviour
{
    public int _band;
    public bool _useBand64 = false;
    public float _amplify = 1f;
    public Vector3 _startingPos = Vector3.one;
    public Vector3 _newPos = Vector3.one * 5;
    public float threshold = 0.5f;
    public float delay = 0.5f;
    public bool isMoved = false;
    public float transitionTime = .2f;
    public FPSController player;

    private float currentTime = 0f;
    private Vector3 previousPosition;
    [HideInInspector]
    public Vector3 offset;

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
                isMoved = !isMoved;
                currentTime = 0f;
                StartCoroutine("Transition");
            }
        }
        else
        {
            currentTime += Time.deltaTime;
        }

        offset = transform.localPosition - previousPosition;
        if (player != null)
        {
            player.offset = offset;
        }
        previousPosition = transform.localPosition;
    }

    private IEnumerator Transition()
    {
        if (isMoved)
        {
            while (currentTime < transitionTime)
            {
                transform.localPosition = Vector3.LerpUnclamped(_startingPos, _newPos, Mathf.SmoothStep(0f, 1f, currentTime / transitionTime));
                yield return null;
            }
            transform.localPosition = _newPos;
        }
        else
        {
            while (currentTime < transitionTime)
            {
                transform.localPosition = Vector3.LerpUnclamped(_newPos, _startingPos, Mathf.SmoothStep(0f, 1f, currentTime / transitionTime));
                yield return null;
            }
            transform.localPosition = _startingPos;
        }
    }
}
