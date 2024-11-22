using UnityEngine;

public class OverlayController : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    void Start()
    {
        // Ambil CanvasGroup dari objek ini
        canvasGroup = GetComponent<CanvasGroup>();

        // Pastikan alpha awal adalah 0 jika overlay awalnya nonaktif
        if (!gameObject.activeSelf)
        {
            canvasGroup.alpha = 0;
        }
    }

    // Fungsi untuk menampilkan overlay (Fade In)
    public void ShowOverlay(float duration)
    {
        gameObject.SetActive(true); // Aktifkan overlay terlebih dahulu
        StartCoroutine(Fade(0, 1, duration)); // Fade In
    }

    // Fungsi untuk menyembunyikan overlay (Fade Out)
    public void HideOverlay(float duration)
    {
        StartCoroutine(Fade(1, 0, duration)); // Fade Out
    }

    private System.Collections.IEnumerator Fade(float startAlpha, float endAlpha, float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            yield return null;
        }

        canvasGroup.alpha = endAlpha;

        // Nonaktifkan overlay jika alpha mencapai 0
        if (endAlpha == 0)
        {
            gameObject.SetActive(false);
        }
    }
}
