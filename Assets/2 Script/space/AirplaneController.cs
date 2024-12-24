using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AirplaneController : MonoBehaviour
{
    private float horizontalInput, verticalInput, throttleInput;
    private float currentRollAngle, currentPitchAngle, currentYawAngle;
    private bool isBraking;

    // Settings
    [SerializeField] private float rollSpeed = 100f; // Speed for rolling (left/right)
    [SerializeField] private float pitchSpeed = 50f; // Speed for pitching (up/down)
    [SerializeField] private float yawSpeed = 30f; // Speed for yawing (turning left/right)
    [SerializeField] private float throttleSpeed = 5f; // Speed for throttle (speed control)
    [SerializeField] private float maxThrottle = 100f; // Maximum throttle (forward speed)
    [SerializeField] private float minThrottle = -10f; // Minimum throttle (reverse speed)
    [SerializeField] private float speedAcceleration = 10f; // Acceleration speed
    [SerializeField] private float speedDeceleration = 20f; // Deceleration speed
    [SerializeField] private float maxSpeed = 200f; // Max speed for the airplane (forward movement)

    // Airplane control flags
    private bool isThrottleIncreasing, isThrottleDecreasing;

    // UI Buttons
    public Button throttleUpButton;
    public Button throttleDownButton;
    public Button rollLeftButton;
    public Button rollRightButton;
    public Button pitchUpButton;
    public Button pitchDownButton;
    public Button brakeButton;

    // Camera setup for FPP
    public Camera airplaneCamera; // Camera in the cabin
    private Vector3 cameraOffset; // Offset to position the camera inside the cockpit

    private float currentSpeed = 0f; // Current speed of the airplane

    // Rotate object with gesture
    [SerializeField] private float rotateSpeed = 1f; // Speed of rotation with touch gesture
    private bool isRotating = false; // Flag to check if rotation is active
    private Vector2 lastTouchPosition; // Last touch position for gesture

    private void Start() {
        // Set initial camera position
        cameraOffset = airplaneCamera.transform.position - transform.position;

        // Add listeners to buttons
        throttleUpButton.onClick.AddListener(() => SetThrottleInput(1)); // Throttle Up
        throttleDownButton.onClick.AddListener(() => SetThrottleInput(-1)); // Throttle Down
        rollLeftButton.onClick.AddListener(() => SetHorizontalInput(-1)); // Roll Left
        rollRightButton.onClick.AddListener(() => SetHorizontalInput(1)); // Roll Right
        pitchUpButton.onClick.AddListener(() => SetVerticalInput(1)); // Pitch Up
        pitchDownButton.onClick.AddListener(() => SetVerticalInput(-1)); // Pitch Down
        brakeButton.onClick.AddListener(() => SetBraking(true)); // Activate Braking
    }

    private void FixedUpdate() {
        HandleThrottle();
        HandleRotation();
        HandleMovement();
        HandleCamera(); // Update camera position to follow the airplane
        HandleGestureRotation(); // Handle rotation based on touch gestures
    }

    // Called by UI buttons to update inputs
    public void SetThrottleInput(float input) {
        throttleInput = input; // -1 for reverse, 1 for forward, 0 for neutral
    }

    public void SetHorizontalInput(float input) {
        horizontalInput = input; // -1 for left, 1 for right
    }

    public void SetVerticalInput(float input) {
        verticalInput = input; // -1 for down, 1 for up
    }

    public void SetBraking(bool braking) {
        isBraking = braking; // true for braking, false otherwise
    }

    private void HandleThrottle() {
        // Update throttle and apply acceleration or deceleration
        if (throttleInput > 0) {
            currentSpeed = Mathf.Min(currentSpeed + speedAcceleration * Time.deltaTime, maxSpeed); // Accelerate forward
        } 
        else if (throttleInput < 0) {
            currentSpeed = Mathf.Max(currentSpeed - speedDeceleration * Time.deltaTime, minThrottle); // Accelerate in reverse
        }

        // Apply the speed to the airplane's forward movement
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
    }

    private void HandleRotation() {
        currentRollAngle = horizontalInput * rollSpeed * Time.deltaTime; // Roll based on horizontal input
        currentPitchAngle = verticalInput * pitchSpeed * Time.deltaTime; // Pitch based on vertical input
        currentYawAngle = 0; // No yaw control here, you can add it later if needed

        // Apply rotation to airplane
        transform.Rotate(currentPitchAngle, currentYawAngle, currentRollAngle);
    }

    private void HandleMovement() {
        // Apply braking: You can implement specific logic for slowing down the airplane
        if (isBraking) {
            currentSpeed = Mathf.Max(currentSpeed - speedDeceleration * Time.deltaTime, 0); // Brake by reducing throttle
        }
    }

    // Handle camera movement to follow the airplane, ensuring it stays inside the cockpit
    private void HandleCamera() {
        if (airplaneCamera != null) {
            // Menjaga posisi kamera tetap berada di dalam kabin
            Vector3 targetPosition = transform.position + cameraOffset;

            // Menetapkan posisi kamera tetap berada pada offset relatif terhadap pesawat
            airplaneCamera.transform.position = targetPosition;

            // Menjaga rotasi kamera agar tetap mengikuti rotasi pesawat
            airplaneCamera.transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0); // Menjaga hanya rotasi Y (putaran horizontal)
        }
    }

    // Handle rotation based on gesture input (touch swipe)
    private void HandleGestureRotation() {
        if (isRotating && Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0); // Get the first touch

            if (touch.phase == TouchPhase.Began) {
                lastTouchPosition = touch.position; // Store the starting position of the touch
            }

            if (touch.phase == TouchPhase.Moved) {
                // Calculate how much the touch has moved since the last frame
                Vector2 touchDelta = touch.position - lastTouchPosition;

                // Rotate the object based on horizontal touch movement (x-axis swipe)
                float rotationAmount = touchDelta.x * rotateSpeed * Time.deltaTime;
                transform.Rotate(Vector3.up, rotationAmount); // Rotate around the Y-axis (up)

                // Update the last touch position for the next frame
                lastTouchPosition = touch.position;
            }
        }
    }

    // Toggle rotation active or inactive
    public void ToggleRotation(bool isActive) {
        isRotating = isActive;
    }

    // Reset brake when button is released
    public void ResetBraking() {
        isBraking = false;
    }
}
