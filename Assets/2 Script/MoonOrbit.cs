using UnityEngine;

public class MoonOrbit : MonoBehaviour
{
    public GameObject orbitTarget; // Objek yang akan diorbit (misalnya Earth)
    public float orbitSpeed = 10f; // Kecepatan orbit dalam derajat per detik
    public float orbitDistance = 8f; // Jarak orbit
    public bool orbitClockwise = true; // Pilihan arah orbit: true untuk kanan, false untuk kiri

    private Vector3 orbitAxis = Vector3.up; // Sumbu rotasi (default: Y-Axis)

    void Update()
    {
        if (orbitTarget == null) return;

        // Atur posisi bulan untuk tetap berada pada jarak orbitDistance
        Vector3 direction = (transform.position - orbitTarget.transform.position).normalized;
        transform.position = orbitTarget.transform.position + direction * orbitDistance;

        // Tentukan arah rotasi berdasarkan orbitClockwise
        float rotationDirection = orbitClockwise ? -1 : 1; // -1 untuk kanan, 1 untuk kiri

        // Rotasi bulan di sekitar target
        transform.RotateAround(orbitTarget.transform.position, orbitAxis, rotationDirection * orbitSpeed * Time.deltaTime);
    }
}
