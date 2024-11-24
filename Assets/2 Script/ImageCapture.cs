using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageCapture : MonoBehaviour
{
    [Header("Capture Setting")]
    public string Path;
    public Camera ScreenshotCamera; // Kamera khusus untuk screenshot
    public int ResolutionWidth = 1920;
    public int ResolutionHeight = 1080;
    private string filename;

    [Header("UI Objects Setting")]
    public List<GameObject> UIObjects;

    [Header("Screenshot Display Setting")]
    public Image ScreenshotDisplay; // UI Image untuk menampilkan screenshot
    public CanvasGroup ScreenshotCanvasGroup; // CanvasGroup untuk animasi fade
    public float DisplayDuration = 2f; // Durasi tampil screenshot
    public float FadeDuration = 0.5f; // Durasi animasi fade in/out

    public void InvokeCameraCapture()
    {
        StartCoroutine(CaptureScreenshotCoroutine());
    }

    IEnumerator CaptureScreenshotCoroutine()
    {
        // Sembunyikan UI objek sebelum screenshot
        HideObjects();

        // Buat RenderTexture baru
        RenderTexture rt = new RenderTexture(ResolutionWidth, ResolutionHeight, 24);
        ScreenshotCamera.targetTexture = rt;

        // Render kamera ke RenderTexture
        ScreenshotCamera.Render();

        // Salin RenderTexture ke Texture2D
        RenderTexture.active = rt;
        Texture2D screenshot = new Texture2D(ResolutionWidth, ResolutionHeight, TextureFormat.RGB24, false);
        screenshot.ReadPixels(new Rect(0, 0, ResolutionWidth, ResolutionHeight), 0, 0);
        screenshot.Apply();

        ScreenshotCamera.targetTexture = null;
        RenderTexture.active = null;
        Destroy(rt);

        // Simpan gambar ke file
        filename = Path + "/" + DateTime.Now.ToString("MM_dd_yyyy_h_mm_ss") + ".png";
        System.IO.File.WriteAllBytes(filename, screenshot.EncodeToPNG());
        Debug.Log("Screenshot saved: " + filename);

        // Tampilkan screenshot di UI
        ShowScreenshotUI(screenshot);

        // Tampilkan kembali UI objek setelah screenshot
        ShowObjects();

        yield return null;
    }

    void HideObjects()
    {
        SetStatusObjects(false);
    }

    void ShowObjects()
    {
        SetStatusObjects(true);
    }

    void SetStatusObjects(bool aStatus)
    {
        for (int i = 0; i < UIObjects.Count; i++)
        {
            UIObjects[i].SetActive(aStatus);
        }
    }

    void ShowScreenshotUI(Texture2D screenshot)
    {
        // Konversi Texture2D ke Sprite
        Sprite screenshotSprite = Sprite.Create(screenshot, new Rect(0, 0, screenshot.width, screenshot.height), new Vector2(0.5f, 0.5f));
        ScreenshotDisplay.sprite = screenshotSprite;

        // Tampilkan UI Image dengan animasi
        StartCoroutine(FadeScreenshot());
    }

    IEnumerator FadeScreenshot()
    {
        // Reset Alpha ke 0
        ScreenshotCanvasGroup.alpha = 0;
        ScreenshotCanvasGroup.gameObject.SetActive(true);

        // Fade In
        float elapsedTime = 0f;
        while (elapsedTime < FadeDuration)
        {
            ScreenshotCanvasGroup.alpha = Mathf.Lerp(0, 1, elapsedTime / FadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        ScreenshotCanvasGroup.alpha = 1;

        // Tampilkan gambar selama DisplayDuration detik
        yield return new WaitForSeconds(DisplayDuration);

        // Fade Out
        elapsedTime = 0f;
        while (elapsedTime < FadeDuration)
        {
            ScreenshotCanvasGroup.alpha = Mathf.Lerp(1, 0, elapsedTime / FadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        ScreenshotCanvasGroup.alpha = 0;

        // Sembunyikan UI
        ScreenshotCanvasGroup.gameObject.SetActive(false);
    }
}
