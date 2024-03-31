using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public float appearCond = 2.0f;

    public ColliderCounter colliderCounter;

    public GameObject appearObject;

    // private float _counter;


    void Start()
    {
        appearObject.SetActive(false);
    }


    void Update()
    {
        
        if (colliderCounter.counter > appearCond)
        {
            appearObject.SetActive(true);
        }
    }
}
