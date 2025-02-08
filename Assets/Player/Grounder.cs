using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grounder : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Platform")
        {
            //Debug.Log("Enter");
            if (other.TryGetComponent<PlatformThreshAudio>(out PlatformThreshAudio comp))
                comp.player = transform.parent.GetComponent<FPSController>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Platform")
        {
            //Debug.Log("Leave");
            if (other.TryGetComponent<PlatformThreshAudio>(out PlatformThreshAudio comp))
               comp.player = null;
                transform.parent.GetComponent<FPSController>().offset = Vector3.zero;
        }
    }
}
