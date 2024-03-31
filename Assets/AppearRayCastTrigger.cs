using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearRayCastTrigger : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private bool isMeshRendererEnable = false;

    [SerializeField] LayerMask layerMask;

    public float delayTime = 5.0f;

    private Coroutine disableCoroutine;
    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;
    }
    // public void Appear()
    // {   
    //     if (!isMeshRendererEnable) 
    //     {
    //         // StopCoroutine(DisableAfterDelay(delay));
    //     meshRenderer.enabled = true;
    //     isMeshRendererEnable = true;
    //     // StartCoroutine(DisableAfterDelay(delayTime));
    //     StartDisableCoroutine();
    //     }
    // }

    

    public void StartDisableCoroutine()
    {
        disableCoroutine = StartCoroutine(DisableAfterDelay(delayTime));
    }

    public void StopDisableCoroutine()
    {
        StopCoroutine(disableCoroutine);
        disableCoroutine = null;
    }

    IEnumerator DisableAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        // Only disable if the raycast is no longer hitting this object
        if (!Physics.Raycast(transform.position, Vector3.forward, delayTime, layerMask))
        {
            meshRenderer.enabled = false;
            isMeshRendererEnable = false;
        }
    }
}
