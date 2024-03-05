using UnityEngine;
using UnityEngine.Rendering;
using System.Collections;
using System.Collections.Generic;

public class selectiveSystem
{
    static selectiveSystem m_Instance; // singleton
    static public selectiveSystem instance {
        get {
            if (m_Instance == null)
                m_Instance = new selectiveSystem();
            return m_Instance;
        }
    }

    internal HashSet<selectiveObj> m_SelectiveObjs = new HashSet<selectiveObj>();

    public void Add(selectiveObj o)
    {
        Remove(o);
        m_SelectiveObjs.Add(o);
        Debug.Log("added effect " + o.gameObject.name);
    }

    public void Remove(selectiveObj o)
    {
        m_SelectiveObjs.Remove(o);
        Debug.Log("removed effect " + o.gameObject.name);
    }
}

[ExecuteInEditMode]
public class selectiveRenderer : MonoBehaviour
{
    private CommandBuffer m_SelectiveBuffer;
    private Dictionary<Camera, CommandBuffer> m_Cameras = new Dictionary<Camera, CommandBuffer>();

    private void Cleanup()
    {
        foreach(var cam in m_Cameras)
        {
            if(cam.Key)
                cam.Key.RemoveCommandBuffer(CameraEvent.BeforeLighting, cam.Value);
        }
        m_Cameras.Clear();
    }

    public void OnDisable()
    {
        Cleanup();
    }

    public void OnEnable()
    {
        Cleanup();
    }

    public void OnWillRenderObject()
    {
        var render = gameObject.activeInHierarchy && enabled;
        if(!render)
        {
            Cleanup();
            return;
        }

        var cam = Camera.current;
        if(!cam)
            return;

        if(m_Cameras.ContainsKey(cam))
            return;
            
        // create new command buffer
        m_SelectiveBuffer = new CommandBuffer();
        m_SelectiveBuffer.name = "Selective map buffer";
        m_Cameras[cam] = m_SelectiveBuffer;

        var selectSystem = selectiveSystem.instance;

        // create render texture for selective map
        int tempID = Shader.PropertyToID("_Temp1");
        m_SelectiveBuffer.GetTemporaryRT(tempID, -1, -1, 24, FilterMode.Point);
        m_SelectiveBuffer.SetRenderTarget(tempID);
        m_SelectiveBuffer.ClearRenderTarget(true, true, Color.black); // clear before drawing to it each frame!!

        // draw all selective objects to it
        foreach(selectiveObj o in selectSystem.m_SelectiveObjs)
        {
            Renderer r = o.GetComponent<Renderer>();
            Material selectiveMat = o.material;
            if(r && selectiveMat)
                m_SelectiveBuffer.DrawRenderer(r, selectiveMat);
        }

        // set render texture as globally accessable 'selective map' texture
        m_SelectiveBuffer.SetGlobalTexture("_SelectiveMap", tempID);

        // add this command buffer to the pipeline
        cam.AddCommandBuffer(CameraEvent.BeforeLighting, m_SelectiveBuffer);
    }
}