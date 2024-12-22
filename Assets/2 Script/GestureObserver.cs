using UnityEngine;

public class GestureObserver : MonoBehaviour
{
    [Header("Main Settings")]
    public float rotationSpeed = 1.0f; // Kecepatan rotasi objek
    public float zoomFactor = 1f; // Faktor zoom
    public bool slowRotate = false; // Opsi rotasi lambat

    // Opsi untuk mengaktifkan atau menonaktifkan rotasi dan zoom
    public bool enableRotation = true;
    public bool enableZoom = true;

    private bool isRotating = false; // Apakah objek sedang dalam proses rotasi
    private Vector2 lastTouchPosition; // Posisi sentuhan sebelumnya
    private Vector2 lastTouchPosition1; // Posisi sentuhan pertama
    private Vector2 lastTouchPosition2; // Posisi sentuhan kedua
    private float initialDistance; // Jarak awal antara dua sentuhan

    private float currentRotationX = 0f; // Rotasi objek pada sumbu X (vertikal)
    private float currentRotationY = 0f; // Rotasi objek pada sumbu Y (horizontal)

    private Vector3 initialPosition; // Menyimpan posisi awal objek
    private Vector3 initialScale; // Menyimpan skala awal objek
    private Quaternion initialRotation; // Menyimpan rotasi awal objek

    void Start()
    {
        // Menyimpan posisi, skala, dan rotasi awal objek saat pertama kali dimulai
        initialPosition = transform.position;
        initialScale = transform.localScale;
        initialRotation = transform.rotation;
    }

    void Update()
    {
        // Mendeteksi input sentuhan
        if (enableRotation && Input.touchCount == 1)
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
                    float deltaPositionY = touch.position.y - lastTouchPosition.y;

                    // Menghitung rotasi berdasarkan perubahan posisi
                    currentRotationY += deltaPositionX * -rotationSpeed * Time.deltaTime; // Horizontal
                    currentRotationX += deltaPositionY * rotationSpeed * Time.deltaTime;  // Vertikal

                    // Batasi rotasi vertikal agar tidak terbalik
                    currentRotationX = Mathf.Clamp(currentRotationX, -90f, 90f);

                    // Memutar objek horizontal (Y) dan vertikal (X)
                    transform.rotation = Quaternion.Euler(currentRotationX, currentRotationY, 0);

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
        else if (enableZoom && Input.touchCount == 2)
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
            if (slowRotate && enableRotation)
            {
                // Putar objek horizontal secara perlahan
                transform.Rotate(Vector3.up, Time.deltaTime * rotationSpeed);
            }
        }
    }

    // Fungsi untuk reset posisi objek
    public void ResetPosition()
    {
        // Menyimpan posisi global (world position)
        Vector3 worldPosition = transform.position;
        Quaternion worldRotation = transform.rotation;

        // Reset posisi, rotasi, dan skala objek ke nilai awal
        transform.position = initialPosition;  // Mengatur posisi global
        transform.rotation = initialRotation;  // Mengatur rotasi global
        transform.localScale = initialScale;   // Mengatur skala objek

        // Jika perlu, Anda bisa mengembalikan posisi dan rotasi global objek, tetapi
        // hanya jika itu diperlukan oleh logic Anda.
        // transform.position = worldPosition;
        // transform.rotation = worldRotation;
    }


}
