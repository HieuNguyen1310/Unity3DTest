using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveCounterFalse : MonoBehaviour
{
    [SerializeField] private int conditionNumber;
    [SerializeField] private GameObject objectToDisappear; // Make sure to reference this in the inspector
    [SerializeField] private ColliderCounter counterReference; // Reference to your counter script

    void Update()
    {
        if (counterReference.counter >= conditionNumber)
        {
            objectToDisappear.SetActive(false); // Deactivate the object
            // Optionally destroy the object if needed: Destroy(objectToDisappear);
            this.enabled = false; // Disable this script after it does its job
        }
    }
}
