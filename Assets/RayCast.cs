using System.Collections;
using System.Collections.Generic;
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

        if (Physics.Raycast(ray, out hitInfo, rayLength, layerMask, QueryTriggerInteraction.Collide))
        {
            Debug.Log("hit!");
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hitInfo.distance, Color.red);
        }
        else
        {
            Debug.Log("Nothing");
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * rayLength, Color.green);
        }
    }
}
