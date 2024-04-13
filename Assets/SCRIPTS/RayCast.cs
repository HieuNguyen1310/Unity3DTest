using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCast : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    public float rayLength = 20f;

    RaycastHit hitInfo;

    DisappearRaycastTrigger currentDisappearTarget;
    AppearRayCastTrigger currentAppearTarget;

    public delegate void PlayerTriggerState(bool inside);
    public event PlayerTriggerState OnPlayerTriggerStatedChange;

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.TransformDirection(Vector3.forward));

        if (Physics.Raycast(ray, out hitInfo, rayLength, layerMask, QueryTriggerInteraction.Collide))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hitInfo.distance, Color.red);

            // Check for DisappearRayCastTrigger
            DisappearRaycastTrigger disappearTarget = hitInfo.transform.GetComponent<DisappearRaycastTrigger>();
            if (disappearTarget != null)
            {
                ManageDisappearTarget(disappearTarget);
            }

            // Check for AppearRayCastTrigger
            AppearRayCastTrigger appearTarget = hitInfo.transform.GetComponent<AppearRayCastTrigger>();
            if (appearTarget != null)
            {
                ManageAppearTarget(appearTarget);
            }

        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * rayLength, Color.green);

            // Manage when not hitting anything
            if (currentDisappearTarget != null)
            {
                currentDisappearTarget.StartReappearCoroutine();
                currentDisappearTarget = null;
            }
            if (currentAppearTarget != null)
            {
                currentAppearTarget.StartDisableCoroutine();
                currentAppearTarget = null;
            }
        }

        if (OnPlayerTriggerStatedChange != null)
        {
            // Notify if either type of target is being hit
            OnPlayerTriggerStatedChange(currentDisappearTarget != null || currentAppearTarget != null);
        }
    }

    void ManageDisappearTarget(DisappearRaycastTrigger disappearTarget)
    {
        if (disappearTarget != currentDisappearTarget)
        {
            if (currentDisappearTarget != null)
            {
                currentDisappearTarget.StopReappearCoroutine();
            }
            currentDisappearTarget = disappearTarget;
            currentDisappearTarget.meshRenderer.enabled = false; // Hide immediately
            currentDisappearTarget.isMeshRendererEnabled = false;
            currentDisappearTarget.StartReappearCoroutine();
        }
        else // Hitting the same target
        {
            currentDisappearTarget.StartReappearCoroutine(); // Reset the timer
        }
    }

    void ManageAppearTarget(AppearRayCastTrigger appearTarget)
    {
        if (appearTarget != currentAppearTarget)
        {
            if (currentAppearTarget != null)
            {
                currentAppearTarget.StopDisableCoroutine();
            }
            currentAppearTarget = appearTarget;
            currentAppearTarget.Appear();
        }
        else // Hitting the same target
        {
            currentAppearTarget.StartDisableCoroutine(); // Reset the timer
        }
    }
}
