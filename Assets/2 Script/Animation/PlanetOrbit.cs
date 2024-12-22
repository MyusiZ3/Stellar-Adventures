using UnityEngine;

public class PlanetOrbit : MonoBehaviour
{
    public Transform target;  // Objek target, misalnya Matahari
    public float orbitSpeed = 10f;  // Kecepatan orbit planet

    private void FixedUpdate()
    {
        // Jika target sudah ditentukan, lakukan orbit
        if (target != null)
        {
            OrbitAroundTarget();
        }
    }

    // Fungsi untuk mengorbit target
    private void OrbitAroundTarget()
    {
        // Rotasi planet mengelilingi target menggunakan RotateAround
        transform.RotateAround(target.position, Vector3.up, orbitSpeed * Time.fixedDeltaTime);

        // Arahkan planet agar selalu menghadap target
        transform.LookAt(target);
    }
}
