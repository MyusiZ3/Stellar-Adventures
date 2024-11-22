using UnityEngine;

public class FadeEffect : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    void Start()
    {
        // Ambil komponen CanvasGroup
        canvasGroup = GetComponent<CanvasGroup>();

        // Pastikan alpha awal adalah 0 jika objek awalnya tidak aktif
        if (!gameObject.activeSelf)
        {
            canvasGroup.alpha = 0;
        }
    }

    // Fungsi untuk fade in
    public void FadeIn(float duration)
    {
        // Aktifkan objek terlebih dahulu
        gameObject.SetActive(true);

        // Mulai fade in
        StartCoroutine(Fade(0, 1, duration));
    }

    // Fungsi untuk fade out
    public void FadeOut(float duration)
    {
        StartCoroutine(Fade(1, 0, duration));
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

        // Nonaktifkan objek jika alpha 0 (opsional)
        if (endAlpha == 0)
        {
            gameObject.SetActive(false);
        }
    }
}
