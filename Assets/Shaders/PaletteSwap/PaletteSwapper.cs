using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaletteSwapper : BaseShader {

    public Texture colorPalette;
    public bool invert = false;

    public override void OnRenderImage(RenderTexture source, RenderTexture destination) {
        shaderMaterial.SetTexture("_ColorPalette", colorPalette);
        shaderMaterial.SetInt("_Invert", invert ? 1 : 0);
        Graphics.Blit(source, destination, shaderMaterial);
    }
}
