using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TouchThenDisappear : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    // public GameObject gameObject;

    void OnTriggerEnter(Collider other) 
    {
        if (other.name == "Player")
        {
            meshRenderer.enabled = false;
        }
    }

    void OnTriggerStay(Collider other) 
    {
        if (other.name == "Player")
        {
            meshRenderer.enabled = false;
        }
    }

    void OnTriggerExit(Collider other) 
    {
        if (other.name == "Player")
        {
            gameObject.SetActive(false);
        }
    }
}
