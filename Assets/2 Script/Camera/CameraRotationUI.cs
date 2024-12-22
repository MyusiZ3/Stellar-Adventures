using UnityEngine;
using UnityEngine.UI;

public class CameraRotationUI : MonoBehaviour
{
    public RectTransform rotationArea; // Panel UI tempat touch input untuk rotasi
    public Camera targetCamera; // Kamera yang akan diputar
    public float rotationSpeed = 5f; // Kecepatan rotasi kamera
    private Vector2 initialTouchPos;

    void Update()
    {
        if (Input.touchCount == 1) // Jika ada 1 sentuhan
        {
            Touch touch = Input.GetTouch(0);
            if (rotationArea.rect.Contains(touch.position)) // Cek apakah sentuhan dalam area rotasi
            {
                // Menyimpan posisi sentuhan pertama saat mulai menyentuh
                if (touch.phase == TouchPhase.Began)
                {
                    initialTouchPos = touch.position;
                }

                // Hitung pergerakan sentuhan
                if (touch.phase == TouchPhase.Moved)
                {
                    Vector2 touchDelta = touch.position - initialTouchPos;

                    // Rotasi horizontal (kiri/kanan) berdasarkan pergerakan horizontal sentuhan
                    float rotateX = touchDelta.x * rotationSpeed * Time.deltaTime;
                    targetCamera.transform.Rotate(Vector3.up, rotateX, Space.World);

                    // Rotasi vertikal (atas/bawah) berdasarkan pergerakan vertikal sentuhan
                    float rotateY = touchDelta.y * rotationSpeed * Time.deltaTime;
                    targetCamera.transform.Rotate(Vector3.right, -rotateY, Space.Self);

                    initialTouchPos = touch.position; // Update posisi sentuhan
                }
            }
        }
    }
}
