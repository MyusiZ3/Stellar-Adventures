using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 10f; // Kecepatan pergerakan kamera
    public float rotationSpeed = 100f; // Kecepatan rotasi kamera
    public float zoomSpeed = 10f; // Kecepatan zoom
    public float minZoom = 5f; // Batas zoom minimum
    public float maxZoom = 50f; // Batas zoom maksimum

    public float verticalSpeed = 10f; // Kecepatan naik/turun kamera

    public VariableJoystick variableJoystick; // Referensi joystick
    public Transform cameraTransform; // Transform kamera untuk pergerakan dan rotasi

    private Vector3 lastMousePosition; // Posisi terakhir mouse untuk mendeteksi gerakan
    private float rotationX = 0f; // Variabel rotasi horizontal
    private float rotationY = 0f; // Variabel rotasi vertikal

    // Batas panel untuk rotasi (bisa berupa UI panel atau area lainnya)
    public RectTransform rotationPanel; // Misal, panel UI yang membatasi rotasi

    void Update()
    {
        // Kontrol kamera untuk PC dan mobile
        if (Application.isMobilePlatform)
        {
            // Kontrol untuk perangkat mobile
            MoveCameraMobile();
            RotateCameraMobile();
            ZoomCameraMobile();
        }
        else
        {
            // Kontrol untuk PC (mouse dan joystick)
            MoveCameraPC();
            RotateCameraPC();
            ZoomCameraPC();
        }
    }

    #region PC Controls
    // Fungsi untuk rotasi kamera berdasarkan input mouse di PC
    private void RotateCameraPC()
    {
        if (Input.GetMouseButton(0)) // Jika mouse ditekan
        {
            Vector3 delta = Input.mousePosition - lastMousePosition; // Hitung pergerakan mouse
            rotationX += delta.x * rotationSpeed * Time.deltaTime; // Rotasi horizontal
            rotationY -= delta.y * rotationSpeed * Time.deltaTime; // Rotasi vertikal

            // Batasi rotasi vertikal agar tidak terlalu besar
            rotationY = Mathf.Clamp(rotationY, -80f, 80f);

            // Terapkan rotasi
            cameraTransform.rotation = Quaternion.Euler(rotationY, rotationX, 0f);
        }

        lastMousePosition = Input.mousePosition; // Perbarui posisi mouse
    }

    // Fungsi untuk zoom kamera menggunakan scroll wheel di PC
    private void ZoomCameraPC()
    {
        float zoomInput = Input.GetAxis("Mouse ScrollWheel");
        Camera.main.fieldOfView -= zoomInput * zoomSpeed; // Ubah field of view kamera

        // Batasi zoom agar tidak terlalu dekat atau terlalu jauh
        Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, minZoom, maxZoom);
    }

    // Fungsi untuk pergerakan kamera di PC (menggunakan joystick)
    private void MoveCameraPC()
    {
        float moveHorizontal = variableJoystick.Horizontal;
        float moveVertical = variableJoystick.Vertical;

        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical) * moveSpeed * Time.deltaTime;
        cameraTransform.Translate(movement, Space.World);  // Hanya menggerakkan posisi kamera
    }
    #endregion

    #region Mobile Controls
    // Fungsi untuk rotasi kamera berdasarkan sentuhan di perangkat mobile
    private void RotateCameraMobile()
    {
        if (Input.touchCount == 1) // Jika hanya ada 1 sentuhan di layar
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                rotationX += touch.deltaPosition.x * rotationSpeed * Time.deltaTime;
                rotationY -= touch.deltaPosition.y * rotationSpeed * Time.deltaTime;

                // Batasi rotasi vertikal agar tidak terlalu besar
                rotationY = Mathf.Clamp(rotationY, -80f, 80f);

                // Terapkan rotasi
                cameraTransform.rotation = Quaternion.Euler(rotationY, rotationX, 0f);
            }
        }
    }

    // Fungsi untuk zoom kamera dengan pinch zoom (2 sentuhan)
    private void ZoomCameraMobile()
    {
        if (Input.touchCount == 2) // Jika ada 2 sentuhan (untuk zoom)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);
            float previousDistance = (touch1.position - touch2.position).magnitude;
            float currentDistance = (touch1.position - touch2.position).magnitude;

            float zoomChange = previousDistance - currentDistance;
            Camera.main.fieldOfView += zoomChange * zoomSpeed * Time.deltaTime;
            Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, minZoom, maxZoom);
        }
    }

    // Fungsi untuk pergerakan kamera di mobile (menggunakan joystick)
    private void MoveCameraMobile()
    {
        float moveHorizontal = variableJoystick.Horizontal;
        float moveVertical = variableJoystick.Vertical;

        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical) * moveSpeed * Time.deltaTime;
        cameraTransform.Translate(movement, Space.World);  // Hanya menggerakkan posisi kamera
    }
    #endregion

    #region UI Controls (Button to Move Camera Up/Down)
    // Fungsi untuk naikkan kamera
    public void MoveCameraUp()
    {
        cameraTransform.Translate(Vector3.up * verticalSpeed * Time.deltaTime, Space.World);
    }

    // Fungsi untuk turunkan kamera
    public void MoveCameraDown()
    {
        cameraTransform.Translate(Vector3.down * verticalSpeed * Time.deltaTime, Space.World);
    }
    #endregion
}
