using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAudio : MonoBehaviour
{
    public int _band;
    public bool _useBuffer;
    public bool _useBand64 = false;
    public float _amplify = 1f;
    public Vector3 _startingScale = Vector3.one;
    public Vector3 _newScale = Vector3.one * 5;

    private Vector3 _scaleDifference;

    void Start()
    {
        _scaleDifference = _newScale - _startingScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (_useBuffer)
        {
            if (_useBand64)
            {
                float audioBandBuffer = Mathf.Clamp(AudioVisualizer.instance._audioBandBuffer64[_band] * _amplify, 0f, 1f);
                transform.localScale = new Vector3(audioBandBuffer * _scaleDifference.x + _startingScale.x, 
                                                   audioBandBuffer * _scaleDifference.y + _startingScale.y,
                                                   audioBandBuffer * _scaleDifference.z + _startingScale.z);
            }
            else
            {
                float audioBandBuffer = Mathf.Clamp(AudioVisualizer.instance._audioBandBuffer[_band] * _amplify, 0f, 1f);
                transform.localScale = new Vector3(audioBandBuffer * _scaleDifference.x + _startingScale.x,
                                                   audioBandBuffer * _scaleDifference.y + _startingScale.y,
                                                   audioBandBuffer * _scaleDifference.z + _startingScale.z);
            }
        }
        else
        {
            if (_useBand64)
            {
                float audioBand = Mathf.Clamp(AudioVisualizer.instance._audioBand64[_band] * _amplify, 0f, 1f);
                transform.localScale = new Vector3(audioBand * _scaleDifference.x + _startingScale.x,
                                                   audioBand * _scaleDifference.y + _startingScale.y,
                                                   audioBand * _scaleDifference.z + _startingScale.z);
            }
            else
            {
                float audioBand = Mathf.Clamp(AudioVisualizer.instance._audioBand[_band] * _amplify, 0f, 1f);
                transform.localScale = new Vector3(audioBand * _scaleDifference.x + _startingScale.x,
                                                   audioBand * _scaleDifference.y + _startingScale.y,
                                                   audioBand * _scaleDifference.z + _startingScale.z);
            }
        }
        
    }
}
