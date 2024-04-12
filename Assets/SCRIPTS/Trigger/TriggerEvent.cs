using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour

{
    [SerializeField] string _tagFilter;

    [SerializeField] UnityEvent _onTriggerEnter;

    [SerializeField] UnityEvent _onTriggerExit;


    void OnTriggerEnter(Collider other)
    {
        if (!string.IsNullOrEmpty(_tagFilter) && !other.gameObject.CompareTag(_tagFilter)) return;
        _onTriggerEnter.Invoke();
    }

    void OnTriggerExit(Collider other)
    {
        if (!string.IsNullOrEmpty(_tagFilter) && !other.gameObject.CompareTag(_tagFilter)) return;
        _onTriggerExit.Invoke();
    }
}
