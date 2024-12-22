using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class LineOrbit : MonoBehaviour
{
    private TrailRenderer trailRenderer;  // TrailRenderer untuk jejak orbit

    [Header("Trail Settings")]
    public float startTrailWidth = 0.1f;  // Lebar awal trail
    public float endTrailWidth = 0.05f;   // Lebar akhir trail
    public float trailTime = 1.5f;        // Durasi trail (berapa lama garis tetap muncul setelah planet lewat)
    public float widthMultiplier = 1.0f; // Pengaturan multiplier untuk lebar garis keseluruhan
    public Color trailStartColor = Color.white;  // Warna awal trail
    public Color trailEndColor = Color.white;    // Warna akhir trail

    private void Start()
    {
        // Mendapatkan TrailRenderer dari GameObject
        trailRenderer = GetComponent<TrailRenderer>();

        // Konfigurasi TrailRenderer
        ConfigureTrailRenderer();
    }

    // Fungsi untuk mengatur TrailRenderer
    private void ConfigureTrailRenderer()
    {
        trailRenderer.startWidth = startTrailWidth;         // Lebar awal trail
        trailRenderer.endWidth = endTrailWidth;             // Lebar akhir trail
        trailRenderer.time = trailTime;                    // Durasi trail
        trailRenderer.widthMultiplier = widthMultiplier;   // Multiplier lebar trail

        // Warna trail
        trailRenderer.startColor = trailStartColor;
        trailRenderer.endColor = trailEndColor;

        // Menggunakan shader default untuk material trail
        trailRenderer.material = new Material(Shader.Find("Sprites/Default"));
    }
}
