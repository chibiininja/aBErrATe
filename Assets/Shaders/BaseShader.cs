using UnityEngine;

public class BaseShader : MonoBehaviour
{
    public Shader shader;

    [HideInInspector]
    protected Material shaderMaterial;
    [HideInInspector]
    public Camera cam;

    protected void OnEnable()
    {
        shaderMaterial = new Material(shader);
        shaderMaterial.hideFlags = HideFlags.HideAndDontSave;
    }

    protected void OnDisable()
    {
        shaderMaterial = null;
    }

    protected void Start()
    {
        cam = GetComponent<Camera>();
        cam.depthTextureMode = DepthTextureMode.Depth;
    }

    public virtual void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, shaderMaterial);
    }
}
