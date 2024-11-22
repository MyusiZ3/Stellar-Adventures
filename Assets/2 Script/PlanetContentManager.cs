using UnityEngine;

public class PlanetContentManager : MonoBehaviour
{
    public GameObject[] planetContents; // Array berisi panel konten planet
    private GameObject activeContent;   // Panel konten yang sedang aktif

    // Fungsi untuk menampilkan konten planet tertentu
    public void ShowContent(int planetIndex)
    {
        // Sembunyikan konten yang sedang aktif (jika ada)
        if (activeContent != null)
        {
            activeContent.SetActive(false);
        }

        // Aktifkan konten planet yang dipilih
        activeContent = planetContents[planetIndex];
        activeContent.SetActive(true);
    }
}
// Buat empt object dlu trs di manage di dalemnya