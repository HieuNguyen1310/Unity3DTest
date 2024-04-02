using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public float appearCond = 2.0f;

    public ColliderCounter colliderCounter;



    private MeshRenderer meshRenderer;

    private MeshCollider meshCollider;

    void Start() 
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;

        meshCollider = GetComponent<MeshCollider>();
        meshCollider.enabled = false;
    }
    
    void Update()
    {
        if (colliderCounter.counter >= appearCond)
        {
            meshRenderer.enabled = true;
            meshCollider.enabled = true;
        }
    }
}
