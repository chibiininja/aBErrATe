using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AberrationAudioBeat : MonoBehaviour
{
    public int _band;
    public bool _useBand64 = false;
    public float _amplify = 1f;
    public float threshold = 0.5f;
    public float delay = 0.5f;
    public float transitionFocalTime = .1f;
    public float transitionChannelTime = .2f;

    private ChromaticAberration _chromaticAberration;
    private float currentTime = 0f;
    private Vector2 _focalOffset = new Vector2(1f, 1f);

    // Update is called once per frame
    void Update()
    {
        _chromaticAberration = Camera.main.gameObject.GetComponent<ChromaticAberration>();

        if (currentTime > delay)
        {
            float audioBand = _useBand64 ?
                Mathf.Clamp(AudioVisualizer.instance._audioBand64[_band] * _amplify, 0f, 1f) :
                Mathf.Clamp(AudioVisualizer.instance._audioBand[_band] * _amplify, 0f, 1f);
            if (audioBand > threshold)
            {
                currentTime = 0f;
                StartCoroutine("TransitionFocal");
                StartCoroutine("TransitionChannel");
            }
        }
        else
            currentTime += Time.deltaTime;
    }

    private IEnumerator TransitionChannel()
    {
        float oldRed = _chromaticAberration.channelOffsets.x;
        float oldBlue = _chromaticAberration.channelOffsets.y;
        float oldGreen = _chromaticAberration.channelOffsets.z;

        while (currentTime < transitionChannelTime)
        {
            _chromaticAberration.channelOffsets = new Vector3(
                Mathf.Lerp(oldRed, oldGreen, currentTime / transitionChannelTime),
                Mathf.Lerp(oldBlue, oldRed, currentTime / transitionChannelTime),
                Mathf.Lerp(oldGreen, oldBlue, currentTime / transitionChannelTime)
                );
            yield return null;
        }
        _chromaticAberration.channelOffsets = new Vector3(oldGreen, oldRed, oldBlue);
    }

    private IEnumerator TransitionFocal()
    {
        Vector2 oldFocal = _chromaticAberration.focalOffset;
        Vector2 newFocal = Random.insideUnitCircle.normalized;

        while (currentTime < transitionFocalTime)
        {
            _chromaticAberration.focalOffset = Vector2.Lerp(oldFocal, newFocal, currentTime / transitionFocalTime);
            yield return null;
        }
        _chromaticAberration.focalOffset = newFocal;
    }
}
