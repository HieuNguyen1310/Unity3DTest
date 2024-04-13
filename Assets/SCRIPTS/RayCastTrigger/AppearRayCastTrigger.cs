using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearRayCastTrigger : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    private bool isMeshRendererEnable = false;

    [SerializeField] LayerMask layerMask;

    public float delayTime = 5.0f;

    private Coroutine disableCoroutine;

    private RayCast rayCastScritps;
    private bool playerInTrigger = false;
    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;

        // Check Raycast and Trigger enter
        rayCastScritps = FindAnyObjectByType<RayCast>();
        if (rayCastScritps != null)
        {
            rayCastScritps.OnPlayerTriggerStatedChange += SetPlayerInTrigger;
        }
    }

    public void SetPlayerInTrigger(bool inside)
    {
        playerInTrigger = inside;
    }

    public void Appear()
    {   
        if (!isMeshRendererEnable && !playerInTrigger) 
        {
            // StopCoroutine(DisableAfterDelay(delay));
        meshRenderer.enabled = true;
        isMeshRendererEnable = true;
        // StartCoroutine(DisableAfterDelay(delayTime));
        StartDisableCoroutine();

        Debug.Log("MeshRenderer Enabled!");
        }
    }

    

    public void StartDisableCoroutine()
    {
         if (disableCoroutine != null)
        {
            StopCoroutine(disableCoroutine);
        }
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
