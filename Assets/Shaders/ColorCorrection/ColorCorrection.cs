using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ColorCorrection : BaseShader{

    public Vector3 exposure = new Vector3(1.0f, 1.0f, 1.0f);

    [Range(-100.0f, 100.0f)]
    public float temperature, tint;

    public Vector3 contrast = new Vector3(1.0f, 1.0f, 1.0f);

    public Vector3 linearMidPoint = new Vector3(0.5f, 0.5f, 0.5f);

    public Vector3 brightness = new Vector3(0.0f, 0.0f, 0.0f);

    [ColorUsageAttribute(false, true)]
    public Color colorFilter;

    public Vector3 saturation = new Vector3(1.0f, 1.0f, 1.0f);

    public override void OnRenderImage(RenderTexture source, RenderTexture destination) {
        shaderMaterial.SetVector("_Exposure", exposure);
        shaderMaterial.SetVector("_Contrast", contrast);
        shaderMaterial.SetVector("_MidPoint", linearMidPoint);
        shaderMaterial.SetVector("_Brightness", brightness);
        shaderMaterial.SetVector("_ColorFilter", colorFilter);
        shaderMaterial.SetVector("_Saturation", saturation);
        shaderMaterial.SetFloat("_Temperature", temperature / 100.0f);
        shaderMaterial.SetFloat("_Tint", tint / 100.0f);

        Graphics.Blit(source, destination, shaderMaterial);
    }
}