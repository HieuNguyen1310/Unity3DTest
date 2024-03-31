using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterPassThrough : MonoBehaviour
{
    public ColliderCounter colliderCounter;
    private void OnTriggerEnter(Collider other)
    {
        if ( other.name == "Player")
        {
            colliderCounter.Count();
        }
    }
}
