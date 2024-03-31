using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class RayCast : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;

    public float rayLength = 20f;

    RaycastHit hitInfo;

    

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
                target.Appear();
            }   

        }
        else
        {
            Debug.Log("Nothing");
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * rayLength, Color.green);
        }
    }
}
