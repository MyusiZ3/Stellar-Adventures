using UnityEngine;
using UnityEngine.UI;

public class Spaceship : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f; // Default movement speed
    public float speedMultiplier = 1f; // Speed multiplier for adjustment
    public float lookSpeed = 2f; // Camera look sensitivity

    private Vector3 direction = Vector3.zero; // Direction of movement

    [Header("UI Buttons")]
    public Button moveForwardButton;
    public Button moveBackwardButton;
    public Button moveLeftButton;
    public Button moveRightButton;
    public Button stopButton;
    public Button increaseSpeedButton;
    public Button decreaseSpeedButton;

    [Header("Touch Settings")]
    public float touchSensitivity = 0.1f; // Sensitivity for camera rotation

    private Vector2 touchStartPosition; // Initial touch position
    private bool isTouching = false; // Is touch active

    private Vector3 rotation; // To track camera orientation
    private Camera playerCamera;  // Camera in the cockpit (inside the spaceship)

    void Start()
    {
        // Set the camera if not assigned
        playerCamera = Camera.main;

        // Assign button events
        moveForwardButton.onClick.AddListener(() => OnMoveForwardPressed());
        moveBackwardButton.onClick.AddListener(() => OnMoveBackwardPressed());
        moveLeftButton.onClick.AddListener(() => OnMoveLeftPressed());
        moveRightButton.onClick.AddListener(() => OnMoveRightPressed());
        stopButton.onClick.AddListener(OnMoveReleased);

        increaseSpeedButton.onClick.AddListener(OnIncreaseSpeedPressed);
        decreaseSpeedButton.onClick.AddListener(OnDecreaseSpeedPressed);
    }

    void Update()
    {
        HandleTouchInput();
        MoveSpaceship();
    }

    // Methods to control movement based on button press
    public void OnMoveForwardPressed() => direction = Vector3.forward;
    public void OnMoveBackwardPressed() => direction = Vector3.back;
    public void OnMoveLeftPressed() => direction = Vector3.left;
    public void OnMoveRightPressed() => direction = Vector3.right;
    public void OnMoveReleased() => direction = Vector3.zero;

    // Core movement logic: Move spaceship relative to camera rotation (First-Person perspective)
    private void MoveSpaceship()
    {
        if (direction != Vector3.zero)
        {
            // Apply movement relative to camera rotation
            Vector3 forward = playerCamera.transform.forward;
            forward.y = 0; // Ignore vertical movement in the camera's forward direction

            Vector3 right = playerCamera.transform.right;
            right.y = 0; // Ignore vertical movement in the camera's right direction

            // Normalize direction vectors for consistent movement speed
            forward.Normalize();
            right.Normalize();

            // Calculate movement direction based on user input (relative to the camera)
            Vector3 movement = (direction.z * forward + direction.x * right) * speed * speedMultiplier * Time.deltaTime;

            // Apply movement in world space
            transform.Translate(movement, Space.World); // Move spaceship in world space relative to camera's direction
        }
    }

    // Adjust speed using UI buttons
    public void OnIncreaseSpeedPressed()
    {
        speedMultiplier += 0.1f;
    }

    public void OnDecreaseSpeedPressed()
    {
        speedMultiplier = Mathf.Max(0.1f, speedMultiplier - 0.1f);
    }

    // Handle touch gestures for camera rotation (First-Person control)
    private void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                isTouching = true;
                touchStartPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved && isTouching)
            {
                Vector2 touchDelta = touch.position - touchStartPosition;

                // Apply rotation based on touch delta
                rotation.y += touchDelta.x * touchSensitivity;
                rotation.x -= touchDelta.y * touchSensitivity;

                // Clamp rotation for smooth movement (vertical look limit)
                rotation.x = Mathf.Clamp(rotation.x, -45f, 45f);

                // Apply rotation to camera (this rotates the cockpit and the spaceship as well)
                playerCamera.transform.localRotation = Quaternion.Euler(rotation.x, rotation.y, 0);

                touchStartPosition = touch.position; // Reset for continuous rotation
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                isTouching = false;
            }
        }
    }
}
