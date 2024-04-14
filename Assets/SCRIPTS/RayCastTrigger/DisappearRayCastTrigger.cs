using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearRaycastTrigger : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public bool isMeshRendererEnabled = true; // Start visible

    [SerializeField] LayerMask layerMask;
    public float reappearDelayTime = 5.0f;

    private Coroutine reappearCoroutine;
    private RayCast rayCastScripts;
    private bool playerInTrigger = false;

    public float disAppearCounter = 0f;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();

        // Check Raycast and Trigger enter
        rayCastScripts = FindObjectOfType<RayCast>(); // Assuming you still have a 'RayCast' component
        if (rayCastScripts != null)
        {
            rayCastScripts.OnPlayerTriggerStatedChange += SetPlayerInTrigger;
        }

        Debug.Log("DisappearRaycastTrigger script: Start function");
    }

    public void SetPlayerInTrigger(bool inside)
    {
        playerInTrigger = inside;
    }

    private void Update()
    {
        if (isMeshRendererEnabled && playerInTrigger) // Update replaces Appear()
        {
            meshRenderer.enabled = false;
            isMeshRendererEnabled = false;
            StartReappearCoroutine();
            disAppearCounter += 1;


            //NotifyParentForDeactivate();
            Debug.Log("MeshRenderer Disabled!");
        }
    }

    //private void NotifyParentForDeactivate()
    //{
    //    transform.parent.GetComponent<AppearInZone>()?.RequestChildDeactivation();
    //}

    public void StartReappearCoroutine()
    {
        if (reappearCoroutine != null)
        {
            StopCoroutine(reappearCoroutine);
        }
        reappearCoroutine = StartCoroutine(ReappearAfterDelay(reappearDelayTime));
    }

    public void StopReappearCoroutine()
    {
        if (reappearCoroutine != null) // Added for clarity
        {
            StopCoroutine(reappearCoroutine);
            reappearCoroutine = null;
        }
    }

    IEnumerator ReappearAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Only reappear if player is NOT in trigger anymore
        if (!playerInTrigger)
        {
            meshRenderer.enabled = true;
            isMeshRendererEnabled = true;
        }
    }
}
