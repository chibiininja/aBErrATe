using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grounder : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Platform")
        {
            Debug.Log("Enter");
            other.GetComponent<PlatformThreshAudio>().player = transform.parent.GetComponent<FPSController>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Platform")
        {
            Debug.Log("Leave");
            other.GetComponent<PlatformThreshAudio>().player = null;
            transform.parent.GetComponent<FPSController>().offset = Vector3.zero;
        }
    }
}
