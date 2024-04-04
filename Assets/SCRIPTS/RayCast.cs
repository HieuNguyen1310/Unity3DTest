using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class RayCast : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;

    public float rayLength = 20f;

    RaycastHit hitInfo;

    AppearRayCastTrigger currentTarget;

    public delegate void PlayerTriggerState(bool inside);
    public event PlayerTriggerState OnPlayerTriggerStatedChange;

    // Update is called once per frame
    void Update()
    {   

        Ray ray = new Ray(transform.position, transform.TransformDirection(Vector3.forward));

        if (Physics.Raycast(ray, out hitInfo, rayLength, layerMask, QueryTriggerInteraction.Collide ))
        {
            Debug.Log("hit! " + hitInfo.transform.name);
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hitInfo.distance, Color.red);

            AppearRayCastTrigger target = hitInfo.transform.GetComponent<AppearRayCastTrigger>();
              if (target != null)
            {
                Debug.Log("Target accessed: " + target.name);
                // Check if it's a new target or the current one we're keeping active
                if (target != currentTarget)
                {
                    if (currentTarget != null)
                    {
                        currentTarget.StopDisableCoroutine();
                    }
                    currentTarget = target;
                    currentTarget.Appear();
                }

                // If it's the current target and still being hit, reset the coroutine
                if (currentTarget == target)
                {
                    currentTarget.StartDisableCoroutine();
                }
            } 

        }
        else
        {
            // Debug.Log("Nothing");
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * rayLength, Color.green);
            if (currentTarget != null) 
            {
                currentTarget.StartDisableCoroutine();
            }
            currentTarget = null;
        }

        if (OnPlayerTriggerStatedChange != null)
        {
            OnPlayerTriggerStatedChange(currentTarget != null);
        }
    }

   
}
