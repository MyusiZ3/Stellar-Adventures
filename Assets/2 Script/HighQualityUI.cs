using UnityEngine;

public class HighQualityUI : MonoBehaviour
{
    void Start()
    {
        // Pastikan canvas memiliki skala pixel tinggi
        Canvas canvas = GetComponent<Canvas>();
        if (canvas != null)
        {
            canvas.pixelPerfect = true; // Aktifkan pixel perfect
        }

        // Atur Anti-Aliasing khusus untuk UI
        QualitySettings.antiAliasing = 4; // Gunakan 4x Anti-Aliasing
    }
}
