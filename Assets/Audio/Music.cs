using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    Portal myPortal;
    Portal linkedPortal;
    AudioSource musicTrack;
    [SerializeField]
    private float myRadius = 5f;
    [SerializeField]
    private float linkedRadius = 5f;
    [SerializeField]
    private float offset = 1f;
    [SerializeField, Range(0f, 1f)]
    private float localVolumeAmplifier;
    
    // Start is called before the first frame update
    void Start()
    {
        myPortal = GetComponentInParent<Portal>();
        linkedPortal = GetComponentInParent<Portal>().linkedPortal;
        musicTrack = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float myDistance = (Camera.main.transform.position - myPortal.transform.position).magnitude;
        float linkedDistance = (Camera.main.transform.position - linkedPortal.transform.position).magnitude;
        if (myDistance < myRadius)
        {
            musicTrack.volume = Mathf.Lerp(0.5f, 1.0f, myDistance / myRadius) * localVolumeAmplifier * AudioManager.instance.musicVolumeAmplifier;
        }
        else if (myDistance < myRadius + offset && myDistance >= myRadius)
        {
            musicTrack.volume = 1f * localVolumeAmplifier * AudioManager.instance.musicVolumeAmplifier;
        }

        if (linkedDistance < linkedRadius)
        {
            musicTrack.volume = Mathf.Lerp(0.5f, 0f, linkedDistance / linkedRadius) * localVolumeAmplifier * AudioManager.instance.musicVolumeAmplifier;
        }
        else if (linkedDistance < linkedRadius + offset && linkedDistance >= linkedRadius)
        {
            musicTrack.volume = 0f;
        }
    }
}
