using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTextureSetup : MonoBehaviour
{   

    public Camera cameraA;
    public Camera cameraB;
    public Camera cameraRoom;
    public Camera cameraExit;

    public Material cameraMatA;
    public Material cameraMatB;
    public Material cameraMatRoom;
    public Material cameraMatExit;
    // Start is called before the first frame update
    void Start()
    {

        if (cameraA.targetTexture != null)
        {
            cameraA.targetTexture.Release();
        }
        cameraA.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cameraMatA.mainTexture = cameraA.targetTexture;


        if (cameraB.targetTexture != null)
        {
            cameraB.targetTexture.Release();
        }
        cameraB.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cameraMatB.mainTexture = cameraB.targetTexture;


        if (cameraRoom.targetTexture != null)
        {
            cameraRoom.targetTexture.Release();
        }
        cameraRoom.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cameraMatRoom.mainTexture = cameraRoom.targetTexture;

        if (cameraExit.targetTexture != null)
        {
            cameraExit.targetTexture.Release();
        }
        cameraExit.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cameraMatExit.mainTexture = cameraExit.targetTexture;
    }

    
}
