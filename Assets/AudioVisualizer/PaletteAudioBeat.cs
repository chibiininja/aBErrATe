using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaletteAudioBeat : MonoBehaviour
{
    public int _band;
    public bool _useBand64 = false;
    public float _amplify = 1f;
    public float threshold = 0.5f;
    public float delay = 0.5f;
    public Texture darkPalette;
    public Texture lightPalette;
    public bool isSwapped;

    private PaletteSwapper _paletteSwapper;
    private float currentTime = 0f;

    // Update is called once per frame
    void Update()
    {
        _paletteSwapper = GetComponent<PaletteSwapper>();

        if (currentTime > delay)
        {
            float audioBand = _useBand64 ?
                Mathf.Clamp(AudioVisualizer.instance._audioBand64[_band] * _amplify, 0f, 1f) :
                Mathf.Clamp(AudioVisualizer.instance._audioBand[_band] * _amplify, 0f, 1f);
            if (audioBand > threshold)
            {
                isSwapped = !isSwapped;
                currentTime = 0f;
                Swap();
            }
        }
        else
            currentTime += Time.deltaTime;
    }

    private void Swap()
    {
        if (isSwapped)
        {
            _paletteSwapper.colorPalette = lightPalette;
            _paletteSwapper.invert = true;
        }
        else
        {
            _paletteSwapper.colorPalette = darkPalette;
            _paletteSwapper.invert = false;
        }
    }
}
