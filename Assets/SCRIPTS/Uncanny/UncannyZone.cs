using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UncannyZone : MonoBehaviour

{
    private bool hasActivated = false; // Flag to track activation state


    void Start()
    {
        // Initially, the zone hasn't activated anything
        hasActivated = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Uncanny") && !hasActivated)
        {
            other.gameObject.SetActive(true);
            hasActivated = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Uncanny"))
        {
            other.gameObject.SetActive(false);
            hasActivated = false;
        }
    }

}
