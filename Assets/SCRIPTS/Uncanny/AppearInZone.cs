using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearInZone : MonoBehaviour
{
    public GameObject _childObject;

    public Transform uncannyTrigger;

    public Collider triggerCollider;

    public bool _useColliderRadius = true;

    private float triggerRadius;

    // Start is called before the first frame update
    void Start()
    {
        _childObject.SetActive(false);

        if (_useColliderRadius && triggerCollider != null )
        {
            {
                // Get the radius based on collider type
                if (triggerCollider is SphereCollider)
                {
                    triggerRadius = ((SphereCollider)triggerCollider).radius * triggerCollider.transform.localScale.x; // Scale also considered 
                }
                else if (triggerCollider is BoxCollider)
                {
                    triggerRadius = triggerCollider.bounds.extents.magnitude; // Approximate radius for boxes
                }
                else
                {
                    Debug.LogWarning("Unsupported collider type. Using default radius.");
                }
            }
        }
    }

    // Update is called once per frame

    void Update()
    {
        // Assuming you have a reference to the "UncannyTrigger" object:
        float distance = Vector3.Distance(transform.position, uncannyTrigger.position);

        if (distance <= triggerRadius)
        {
            _childObject.SetActive(true);
            //Debug.Log("IN");
        }
        else
        {
            _childObject.SetActive(false);
            //Debug.Log("OUT");
        }
    }
    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("UncannyTrigger"))
    //    {
    //        _childObject.SetActive(true);
    //        Debug.Log("IN");
    //    }
    //}

    //void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("UncannyTrigger"))
    //    {
    //        _childObject.SetActive(false);
    //        Debug.Log("OUT");
    //    }
    //}

    //public void RequestChildDeactivation()
    //{
    //    _childObject.SetActive(false);
    //    Debug.Log("Received deactivation signal from child");
    //}
}
