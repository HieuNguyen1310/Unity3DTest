using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveCounterTrue : MonoBehaviour
{
    
    [SerializeField] private int conditionNumber;
    [SerializeField] private List<GameObject> childrenToAppear; // A list to hold multiple children
    [SerializeField] private ColliderCounter counterReference; 

    void Start()
    {
        foreach (GameObject child in childrenToAppear)
        {
            child.SetActive(false); // Ensure all children start inactive
        }
    }

    void Update()
    {
        if (counterReference.counter >= conditionNumber)
        {
            foreach (GameObject child in childrenToAppear)
            {
                child.SetActive(true); // Activate all children in the list
            }
            this.enabled = false; 
        }
    }
}

