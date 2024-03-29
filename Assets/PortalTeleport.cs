using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PortalTeleport : MonoBehaviour
{
    public Transform player;
    public Transform receiver;

    private bool playerIsOverlapping = false;
    void Update()
    {
        if (playerIsOverlapping) 
        {
            Vector3  portalToPlayer = player.position - transform.position;
            float dotProduct = Vector3.Dot(transform.up, portalToPlayer);

            //If this true, player has moved across the portal
            if (dotProduct < 0f) 
            {
                // Teleport 
                float rotationDiff = -Quaternion.Angle(transform.rotation, receiver.rotation);
                rotationDiff += 180;
                player.Rotate(Vector3.up, rotationDiff);

                Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;
                player.position = receiver.position + positionOffset;

                playerIsOverlapping = false;
            }
        }

        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") 
        {
            playerIsOverlapping = true;

        }
    }

    void OnTriggerExit(Collider other) 
    {
        if (other.tag == "Player")
        {
            playerIsOverlapping = false;
        }
    }
}
