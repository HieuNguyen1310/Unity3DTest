using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearRayCastTrigger : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;
    }
    public void Appear()
    {
        meshRenderer.enabled = true;
    }
}
