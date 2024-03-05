using UnityEngine;

[ExecuteInEditMode]
public class selectiveObj : MonoBehaviour
{
    public Material material;

    public void OnEnable()
    {
        selectiveSystem.instance.Add(this);
    }

    public void Start()
    {
        selectiveSystem.instance.Add(this);
    }

    public void OnDisable()
    {
        selectiveSystem.instance.Remove(this);
    }

}