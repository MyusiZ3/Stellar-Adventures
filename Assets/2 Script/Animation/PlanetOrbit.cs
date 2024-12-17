using UnityEngine;

public class PlanetOrbit : MonoBehaviour
{
    public Transform target;  // Objek target, misalnya Sun
    public float orbitSpeed = 10f;  // Kecepatan orbit planet
    private Vector3 offset;  // Jarak relatif awal dari planet ke target
    private TrailRenderer trailRenderer;  // TrailRenderer untuk jejak orbit

    // Pengaturan Trail
    public float startTrailWidth = 0.1f;  // Lebar awal trail
    public float endTrailWidth = 0.05f;   // Lebar akhir trail
    public float trailTime = 1.5f;         // Durasi trail (berapa lama garis tetap muncul setelah planet lewat)
    public float widthMultiplier = 1.0f;   // Pengaturan multiplier untuk lebar garis keseluruhan

    // Start is called before the first frame update
    void Start()
    {
        // Pastikan target sudah ditetapkan
        if (target != null)
        {
            // Menghitung offset berdasarkan posisi awal planet dan target
            offset = transform.position - target.position;
        }

        // Menambahkan TrailRenderer ke planet
        trailRenderer = gameObject.AddComponent<TrailRenderer>();

        // Pengaturan TrailRenderer
        trailRenderer.startWidth = startTrailWidth;  // Lebar awal trail
        trailRenderer.endWidth = endTrailWidth;      // Lebar akhir trail
        trailRenderer.material = new Material(Shader.Find("Sprites/Default"));  // Gunakan shader default
        trailRenderer.startColor = Color.white;      // Warna trail (garis putih)
        trailRenderer.endColor = Color.white;        // Warna trail (garis putih)
        trailRenderer.time = trailTime;              // Durasi trail
        trailRenderer.widthMultiplier = widthMultiplier;  // Mengatur multiplier lebar trail keseluruhan
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            // Hitung posisi planet di orbit dengan kecepatan rotasi
            transform.RotateAround(target.position, Vector3.up, orbitSpeed * Time.deltaTime);

            // Set posisi planet berdasarkan jarak offset yang sudah dihitung
            Vector3 direction = transform.position - target.position;
            direction.Normalize();
            transform.position = target.position + direction * offset.magnitude;

            // Arahkan planet agar selalu menghadap ke target (misalnya Sun)
            transform.LookAt(target);
        }
    }
}
