using UnityEngine;

[ExecuteInEditMode]
public class ChromaticAberration : BaseShader {

    public bool debugMask = false;

    public Vector2 focalOffset = new Vector2(0.0f, 0.0f);
    public Vector2 radius = new Vector2(1.0f, 1.0f);

    [Range(0.01f, 5.0f)]
    public float hardness = 1.0f;
    [Range(0.01f, 5.0f)]
    public float intensity = 1.0f;

    public Vector3 channelOffsets = new Vector3(0.0f, 0.0f, 0.0f);

    public override void OnRenderImage (RenderTexture source, RenderTexture destination) {
        shaderMaterial.SetInt("_DebugMask", debugMask ? 1 : 0);
        shaderMaterial.SetVector("_FocalOffset", focalOffset);
        shaderMaterial.SetVector("_Radius", radius);
        shaderMaterial.SetVector("_ColorOffsets", channelOffsets);
        shaderMaterial.SetFloat("_Hardness", hardness);
        shaderMaterial.SetFloat("_Intensity", intensity);

        Graphics.Blit(source, destination, shaderMaterial);
    }
}