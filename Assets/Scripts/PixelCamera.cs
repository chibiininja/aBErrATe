using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PixelCamera : MonoBehaviour
{
    enum PixelScreenMode { Resize, Scale }

    [SerializeField]
    private struct ScreenSize
    {
        public int width;
        public int height;
    }

    [Header("Screen scaling settings")]
    [SerializeField]
    PixelScreenMode mode;
    [SerializeField]
    ScreenSize targetScreenSize = new ScreenSize { width = 256, height = 144 };
    [SerializeField, Range(1,128), Delayed]
    int screenScaleFactor = 1;

    [SerializeField]
    Camera renderCamera;
    RenderTexture renderTexture;
    int screenWidth, screenHeight;

    [Header("Display")]
    [SerializeField] 
    RawImage display;

    [Header("Post Processing")]
    [SerializeField]
    Material material;

    private void Init()
    {
        renderCamera ??= GetComponent<Camera>();
        screenWidth = Screen.width;
        screenHeight = Screen.height;

        if (screenScaleFactor < 1) screenScaleFactor = 1;
        if (targetScreenSize.width < 1) targetScreenSize.width = 1;
        if (targetScreenSize.height < 1) targetScreenSize.height = 1;

        int width = mode == PixelScreenMode.Resize ? (int)targetScreenSize.width : screenWidth / (int)screenScaleFactor;
        int height = mode == PixelScreenMode.Resize ? (int)targetScreenSize.height : screenHeight / (int)screenScaleFactor;

        renderTexture = new RenderTexture(width, height, 24)
        {
            filterMode = FilterMode.Point,
            antiAliasing = 1
        };

        renderCamera.targetTexture = renderTexture;
        Graphics.Blit(renderTexture, material);
        display.texture = renderTexture;
    }

    private void Update()
    {
        if (CheckScreenResize()) Init();
    }

    private void OnValidate()
    {
        if (CheckScreenResize()) Init();
        screenScaleFactor = Mathf.ClosestPowerOfTwo(screenScaleFactor);
    }

    private bool CheckScreenResize()
    {
        return Screen.width != screenWidth || Screen.height != screenHeight;
    }
}
