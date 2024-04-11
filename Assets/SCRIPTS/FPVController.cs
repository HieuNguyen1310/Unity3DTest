using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float movementSpeed = 7.0f; // Speed of the character movement

    public float sprintSpeed = 15.0f; // Sprint Speed

    public float mouseSensitivity = 100.0f; // Sensitivity of the mouse movement
    public Transform playerCamera; // Reference to the camera transform
    public float cameraPitchLimit = 90.0f; // Limit to how far up or down the camera can look

    // Camera bobbing variables
    public float bobbingFrequency = 2f; // How fast the bobbing occurs
    public float bobbingAmount = 0.05f; // The amount of bobbing up and down
    private float bobbingTimer = 0.0f;

  // Camera tilt variables
    public float cameraTiltAmount = 5.0f;
    public float tiltSpeed = 2.0f;

    private Quaternion targetTilt;
    private float currentTilt = 0f;

    // Idle breathing variables
    public float idleBreathingFrequency = 1.0f; 
    public float idleBreathingAmount = 0.02f;
    private float idleBreathingTimer = 0.0f;


    private CharacterController controller;
    private float cameraPitch = 0.0f; // Current pitch of the camera
    private Vector3 originalCameraPosition;

    public float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = .45f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    

    void Start()
    {
        controller = GetComponent<CharacterController>(); // Get the CharacterController component
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center of the screen
        Cursor.visible = false; // Make the cursor invisible
        originalCameraPosition = playerCamera.localPosition; // Store the original local position of the camera
       

        Debug.Log("Initial Camera Local Euler Angles: " + playerCamera.localEulerAngles);
        playerCamera.localEulerAngles = Vector3.zero; // Reset camera rotation to start straight

      
    }

    void Update()
    {
        UpdateMovement(); // Update the character's movement
        UpdateLook(); // Update the camera's look direction
        // UpdateCameraBobbing(); // Update the camera bobbing effect continuously
        UpdateCameraEffects();
    }

      void UpdateCameraEffects() 
    {
        UpdateCameraTilt();
        UpdateCameraBobbing(); // Modify to include idle breathing
    }

    void UpdateMovement()
    {

          isGrounded = Physics.CheckSphere(groundCheck.position,groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            // Debug.Log(velocity.y);
        }

        float horizontal = Input.GetAxis("Horizontal"); // Get input from the horizontal axis (A/D or Left/Right arrow keys)
        float vertical = Input.GetAxis("Vertical"); // Get input from the vertical axis (W/S or Up/Down arrow keys)

        bool isSprinting = Input.GetKey(KeyCode.LeftShift);
        float targetSpeed = isSprinting ? sprintSpeed : movementSpeed;

        Vector3 moveDirection = transform.right * horizontal + transform.forward * vertical; // Calculate the movement direction
        
        controller.Move(moveDirection * targetSpeed * Time.deltaTime); // Move the character controller
        
        velocity.y += gravity * Time.deltaTime;
        // Debug.Log(velocity.y);
        
        controller.Move(velocity * Time.deltaTime);
    }

    // void Sprint ()
    // {
        
    // }


   
    void UpdateLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime; // Get mouse movement on the X-axis
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime; // Get mouse movement on the Y-axis

        cameraPitch -= mouseY; // Update the camera pitch based on mouse Y-axis movement
        cameraPitch = Mathf.Clamp(cameraPitch, -cameraPitchLimit, cameraPitchLimit); // Clamp the camera pitch to the specified limit

        
        playerCamera.localEulerAngles = Vector3.right * cameraPitch; // Apply the pitch rotation to the camera


        // Rotate the character based on mouse X-axis movement
        transform.Rotate(Vector3.up * mouseX); 

    }

      void UpdateCameraTilt()
    {
        // Calculate tilt based on relative rotation speed
        float mouseX = Input.GetAxis("Mouse X");
        float tiltTarget = mouseX * cameraTiltAmount * tiltSpeed; 

        // Smoothly change current tilt towards target tilt 
        currentTilt = Mathf.Lerp(currentTilt, tiltTarget, tiltSpeed * Time.deltaTime);

        // Apply rotation 
        playerCamera.rotation = Quaternion.Euler(playerCamera.rotation.eulerAngles.x, playerCamera.rotation.eulerAngles.y, currentTilt);

        // Gradual tilt reset
        if (Mathf.Abs(mouseX) < 0.1f) { 
            currentTilt = Mathf.Lerp(currentTilt, 0.0f, tiltSpeed * Time.deltaTime); // Reset towards zero
        }
    }

    // void UpdateCameraBobbing()
    // {
    //     if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f || Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f) // Check if there's significant movement input
    //     {
    //         bobbingTimer += Time.deltaTime * bobbingFrequency;
    //     }
    //     else
    //     {
    //         // Reset the timer if the player is not moving to smooth out the transition
    //         bobbingTimer = 0;
            
    //     }

    //     float bobbingOffset = Mathf.Sin(bobbingTimer) * bobbingAmount;
    //     playerCamera.localPosition = originalCameraPosition + new Vector3(0, bobbingOffset, 0); // Apply the bobbing effect to the camera's local position
    // }

        void UpdateCameraBobbing()
        {
            float bobbingOffset = 0.0f;
            float movementIntensity = Mathf.Max(Mathf.Abs(Input.GetAxis("Horizontal")), Mathf.Abs(Input.GetAxis("Vertical")));

            // Movement bobbing with smooth fade-out
            if (movementIntensity > 0.1f) 
            {
                bobbingTimer += Time.deltaTime * bobbingFrequency * movementIntensity; // Adjust speed with intensity
                bobbingOffset = Mathf.Sin(bobbingTimer) * bobbingAmount * movementIntensity;
            }
            else 
            {
                bobbingTimer -= Time.deltaTime * bobbingFrequency; // Decrease bobbingTimer when not moving
                // bobbingTimer = Mathf.Clamp01(bobbingTimer); // Keep the timer between 0 and 1
                bobbingOffset = Mathf.Sin(bobbingTimer) * bobbingAmount * Mathf.Pow(bobbingTimer, 2.0f); // Fade based on timer (0-1 range)
            }

            // Movement bobbing
            if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f || Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f) 
            {
                bobbingTimer += Time.deltaTime * bobbingFrequency;
                bobbingOffset = Mathf.Sin(bobbingTimer) * bobbingAmount;
            }
            else 
            {
                bobbingTimer = 0f; // Reset when idle
                bobbingOffset = 0f; // No movement-based bobbing
            }

            // Idle breathing
            idleBreathingTimer += Time.deltaTime * idleBreathingFrequency;
            float idleBobbingOffset = Mathf.Sin(idleBreathingTimer) * idleBreathingAmount;

            // Combined effect
            float totalBobbingOffset = bobbingOffset + idleBobbingOffset;

            playerCamera.localPosition = originalCameraPosition + new Vector3(0, totalBobbingOffset, 0);
        }

 
}
