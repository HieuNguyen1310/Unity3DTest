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
            Debug.Log("MeshRenderer Disabled!");
        }
    }

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
