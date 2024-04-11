using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFaceTracking : MonoBehaviour
{
    public Transform playerTransform;
    public Transform objTransform;

    


    void Update()
    {
        // Calculate direction to player, projected onto XZ plane
        Vector3 directionToPlayer = playerTransform.position - objTransform.position;
        directionToPlayer.y = 0; // Ignore height differences

        // If the direction isn't zero
        if (directionToPlayer != Vector3.zero) 
        {
            // Calculate target rotation as before
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);

            // Apply original offset rotation
            // targetRotation *= Quaternion.Euler(-90, 0, 90); 

            // Rotate around the Y-axis only 
            objTransform.rotation = Quaternion.Euler(90, targetRotation.eulerAngles.y, -90); 

            // Optional: Smooth rotation
            // myTransform.rotation = Quaternion.Slerp(myTransform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }

        Debug.DrawLine(objTransform.position, objTransform.position + directionToPlayer, Color.red);
    }

    // Debug.DrawLine(myTransform.position, myTransform.position + directionToPlayer, Color.red);



}
