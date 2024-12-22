using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraSwitch : MonoBehaviour
{
    public Camera mainCamera;  // Kamera utama
    public Camera mercuryCamera;  // Kamera khusus untuk Mercury
    public Camera venusCamera;    // Kamera khusus untuk Venus
    public Camera earthCamera;    // Kamera khusus untuk Earth
    public Camera marsCamera;     // Kamera khusus untuk Mars
    public Camera jupiterCamera;  // Kamera untuk Jupiter
    public Camera saturnCamera;   // Kamera untuk Saturn
    public Camera uranusCamera;   // Kamera untuk Uranus
    public Camera neptuneCamera;  // Kamera untuk Neptune
    public Camera sunCamera;      // Kamera untuk Sun

    public float transitionDuration = 1f;  // Durasi transisi perpindahan kamera
    public Image blackScreen;  // Gambar hitam untuk efek transisi kabur

    private Camera currentCamera;  // Kamera yang sedang aktif
    private bool isTransitioning = false; // Flag untuk mengecek apakah sedang dalam transisi

    private void Start()
    {
        InitializeCameras();
        currentCamera = mainCamera;  // Pastikan kamera awal adalah mainCamera
        SetCameraActive(mainCamera);
    }

    // Fungsi untuk mengatur semua kamera di awal
    private void InitializeCameras()
    {
        // Nonaktifkan semua kamera terlebih dahulu
        DisableAllCameras();

        // Aktifkan hanya kamera utama (mainCamera)
        mainCamera.gameObject.SetActive(true);
    }

    // Fungsi untuk menonaktifkan semua kamera
    private void DisableAllCameras()
    {
        mercuryCamera.gameObject.SetActive(false);
        venusCamera.gameObject.SetActive(false);
        earthCamera.gameObject.SetActive(false);
        marsCamera.gameObject.SetActive(false);
        jupiterCamera.gameObject.SetActive(false);
        saturnCamera.gameObject.SetActive(false);
        uranusCamera.gameObject.SetActive(false);
        neptuneCamera.gameObject.SetActive(false);
        sunCamera.gameObject.SetActive(false);
        mainCamera.gameObject.SetActive(false);
    }

    // Fungsi untuk mengubah kamera ke planet tertentu
    public void SwitchToCamera(Camera targetCamera)
    {
        // Pastikan kamera target tidak sama dengan kamera saat ini dan tidak sedang dalam transisi
        if (!isTransitioning)
        {
            if (currentCamera != targetCamera)
            {
                Debug.Log($"Switching to {targetCamera.name}");  // Debug untuk memantau transisi
                StartCoroutine(SwitchCameraCoroutine(targetCamera));
            }
            else
            {
                Debug.Log($"Already using {targetCamera.name} camera. No transition needed.");
            }
        }
        else
        {
            Debug.Log($"Cannot switch to {targetCamera.name} - Transition in progress.");
        }
    }

    public void SwitchToMercury() => SwitchToCamera(mercuryCamera);
    public void SwitchToVenus() => SwitchToCamera(venusCamera);
    public void SwitchToEarth() => SwitchToCamera(earthCamera);
    public void SwitchToMars() => SwitchToCamera(marsCamera);
    public void SwitchToJupiter() => SwitchToCamera(jupiterCamera);
    public void SwitchToSaturn() => SwitchToCamera(saturnCamera);
    public void SwitchToUranus() => SwitchToCamera(uranusCamera);
    public void SwitchToNeptune() => SwitchToCamera(neptuneCamera);
    public void SwitchToSun() => SwitchToCamera(sunCamera);
    public void SwitchToMain() => SwitchToCamera(mainCamera);

    // Coroutine untuk menangani efek transisi dan perpindahan kamera
    private IEnumerator SwitchCameraCoroutine(Camera newCamera)
    {
        isTransitioning = true; // Tandai bahwa sedang dalam proses transisi

        // Fade out (kaburkan layar)
        yield return FadeToBlack();

        // Menonaktifkan kamera lama dan mengaktifkan kamera baru
        SetCameraActive(newCamera);

        // Fade in (tampilkan layar kembali)
        yield return FadeFromBlack();

        isTransitioning = false; // Tandai bahwa transisi selesai
    }

    // Fungsi untuk mengaktifkan kamera baru dan menonaktifkan kamera lama
    private void SetCameraActive(Camera newCamera)
    {
        if (currentCamera != null)
        {
            Debug.Log($"Disabling {currentCamera.name}");  // Debug untuk memantau kamera yang dinonaktifkan
            currentCamera.gameObject.SetActive(false);  // Nonaktifkan kamera saat ini
        }

        Debug.Log($"Enabling {newCamera.name}");  // Debug untuk memantau kamera yang diaktifkan
        newCamera.gameObject.SetActive(true);        // Aktifkan kamera baru
        currentCamera = newCamera;                   // Set kamera sekarang menjadi kamera baru
    }

    // Fungsi untuk efek transisi (fade to black)
    private IEnumerator FadeToBlack()
    {
        float elapsedTime = 0f;
        blackScreen.gameObject.SetActive(true);  // Aktifkan layar hitam

        while (elapsedTime < transitionDuration)
        {
            blackScreen.color = Color.Lerp(new Color(0, 0, 0, 0), Color.black, elapsedTime / transitionDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        blackScreen.color = Color.black;  // Pastikan layar sepenuhnya hitam
    }

    // Fungsi untuk efek transisi (fade from black)
    private IEnumerator FadeFromBlack()
    {
        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            blackScreen.color = Color.Lerp(Color.black, new Color(0, 0, 0, 0), elapsedTime / transitionDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        blackScreen.color = new Color(0, 0, 0, 0);  // Pastikan layar sepenuhnya transparan
        blackScreen.gameObject.SetActive(false);  // Nonaktifkan layar hitam
    }
}
