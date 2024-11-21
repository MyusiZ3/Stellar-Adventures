using UnityEngine;

public class ParallaxMovement : MonoBehaviour // Harus turunan MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 2f; // Kecepatan gerakan
    public float range = 3f; // Jarak gerakan dari titik awal

    private Vector3 _startPosition;
    private float _direction = 1f;

    void Start()
    {
        // Simpan posisi awal objek
        _startPosition = transform.localPosition;
    }

    void Update()
    {
        // Gerakan bolak-balik berdasarkan range
        transform.localPosition += Vector3.right * _direction * speed * Time.deltaTime;

        // Cek apakah posisi sudah mencapai batas gerakan
        if (Mathf.Abs(transform.localPosition.x - _startPosition.x) >= range)
        {
            _direction *= -1; // Balik arah
        }
    }
}
