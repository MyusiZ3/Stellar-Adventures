using UnityEngine;

public class ObjectRotation : MonoBehaviour
{
    public float rotationSpeed = 10f; // Kecepatan rotasi dalam derajat per detik
    public bool rotateClockwise = true; // Pilihan arah rotasi: true untuk kanan, false untuk kiri

    private float rotationDirection; // Arah rotasi

    void Update()
    {
        // Tentukan arah rotasi berdasarkan rotateClockwise
        rotationDirection = rotateClockwise ? -1 : 1; // -1 untuk kanan, 1 untuk kiri

        // Rotasi objek di sekitar sumbu Z (untuk objek 2D)
        transform.Rotate(0, 0, rotationDirection * rotationSpeed * Time.deltaTime);
    }
}