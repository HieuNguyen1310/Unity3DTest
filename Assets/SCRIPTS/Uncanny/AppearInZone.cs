using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearInZone : MonoBehaviour
{
    private bool isActive = false;

    [SerializeField] List<GameObject> childrenToAppear;

    private void Start()
    {
        foreach (GameObject child in childrenToAppear) 
        {
            child.SetActive(isActive);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("UncannyTrigger"))
        {
            foreach(GameObject child in childrenToAppear)
            {
                isActive = true;
                child.SetActive(isActive);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("UncannyTrigger"))
        {
            foreach (GameObject child in childrenToAppear)
            {
                isActive = false;
                child.SetActive(isActive);
            }
        }
    }
}
