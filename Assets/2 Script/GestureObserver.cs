using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureObserver : MonoBehaviour
{
    [Header("Main Settings")]
    public float rotationSpeed = 1.0f; // Kecepatan rotasi objek
    public float zoomFactor = 1f;
    public bool slowRotate = false;

    private bool isRotating = false; // Apakah objek sedang dalam proses rotasi
    private Vector2 lastTouchPosition; // Posisi sentuhan sebelumnya
    private float initialDistance; // Jarak awal antara dua sentuhan

    void Update()
    {
        // Mendeteksi input sentuhan
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // Menyimpan posisi sentuhan saat ini sebagai posisi sebelumnya
                    lastTouchPosition = touch.position;
                    break;

                case TouchPhase.Moved:
                    // Menghitung perubahan posisi sentuhan
                    float deltaPositionX = touch.position.x - lastTouchPosition.x;

                    // Menghitung rotasi berdasarkan perubahan posisi
                    float rotation = deltaPositionX * -rotationSpeed;

                    // Memutar objek horizontal
                    transform.Rotate(Vector3.up, rotation);

                    // Memperbarui posisi sentuhan terakhir
                    lastTouchPosition = touch.position;

                    // Objek sedang dalam proses rotasi
                    isRotating = true;
                    break;

                case TouchPhase.Ended:
                    // Menandakan bahwa objek selesai dalam proses rotasi
                    isRotating = false;
                    break;
            }
        }
        else if (Input.touchCount == 2)
        {
            // Mendeteksi input pinch (dua sentuhan)
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            switch (touch1.phase)
            {
                case TouchPhase.Began:
                    // Menyimpan jarak awal antara dua sentuhan
                    initialDistance = Vector2.Distance(touch1.position, touch2.position);
                    break;

                case TouchPhase.Moved:
                    // Menghitung jarak saat ini antara dua sentuhan
                    float currentDistance = Vector2.Distance(touch1.position, touch2.position);

                    // Menghitung perubahan jarak
                    float pinchDelta = currentDistance - initialDistance;

                    // Menghitung faktor zoom
                    float localzoomFactor = pinchDelta * 0.01f * zoomFactor; // Nilai ini dapat disesuaikan

                    // Zoom in dan zoom out
                    transform.localScale += new Vector3(localzoomFactor, localzoomFactor, localzoomFactor);

                    // Memperbarui jarak awal
                    initialDistance = currentDistance;
                    break;
            }
        }
        else
        {
            // Menghentikan rotasi saat tidak ada sentuhan
            isRotating = false;
        }
    }

    void LateUpdate()
    {
        // Menangani rotasi berkelanjutan setelah sentuhan berakhir
        if (!isRotating)
        {
            if (slowRotate)
            {
                // Putar objek horizontal secara perlahan
                transform.Rotate(Vector3.up, Time.deltaTime * rotationSpeed);
            }
        }
    }
}
