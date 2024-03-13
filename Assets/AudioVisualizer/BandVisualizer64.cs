using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BandVisualizer64 : MonoBehaviour
{
    public GameObject tile;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                GameObject prefab = Instantiate(tile, transform);
                prefab.transform.localPosition = new Vector3((i - 4) * 0.25f + 0.125f, (j - 4) * 0.25f + 0.125f, 0);
                EmissionAudio emissionAudio = prefab.AddComponent<EmissionAudio>();
                emissionAudio._band = j + i * 8;
                emissionAudio._useBuffer = true;
                emissionAudio._useBand64 = true;
                ScaleAudio scaleAudio = prefab.AddComponent<ScaleAudio>();
                scaleAudio._band = j + i * 8;
                scaleAudio._useBuffer = true;
                scaleAudio._useBand64 = true;
                Vector3 tileLocalScale = prefab.transform.localScale;
                scaleAudio._startingScale = tileLocalScale;
                scaleAudio._newScale = new Vector3(tileLocalScale.x, tileLocalScale.y, tileLocalScale.z * 5);
            }
        }
    }
}
