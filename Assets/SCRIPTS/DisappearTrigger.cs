using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public float disappearCond = 2.0f;

    public ColliderCounter colliderCounter;



    private MeshRenderer meshRenderer;

    private MeshCollider meshCollider;

    void Start() 
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = true;

        meshCollider = GetComponent<MeshCollider>();
        meshCollider.enabled = true;
    }
    
    void Update()
    {
        if (colliderCounter.counter >= disappearCond)
        {
            meshRenderer.enabled = false;
            meshCollider.enabled = false;
        }
    }
}
