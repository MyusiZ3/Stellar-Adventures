using UnityEngine;
using UnityEngine.UI;

public class SoundSlider : MonoBehaviour
{
    [SerializeField] private Slider soundSlider; // Reference to the slider
    [SerializeField] private AudioSource backgroundMusic; // Reference to the AudioSource for background music

    private const string VolumePrefKey = "BackgroundVolume"; // Key to save volume settings

    void Start()
    {
        // Load saved volume or set to default (1.0)
        float savedVolume = PlayerPrefs.GetFloat(VolumePrefKey, 1.0f);
        soundSlider.value = savedVolume;
        backgroundMusic.volume = savedVolume;

        // Add listener to handle slider value changes
        soundSlider.onValueChanged.AddListener(SetVolume);
    }

    void SetVolume(float volume)
    {
        // Set AudioSource volume and save preference
        backgroundMusic.volume = volume;
        PlayerPrefs.SetFloat(VolumePrefKey, volume);
        PlayerPrefs.Save();
    }

    void OnDestroy()
    {
        // Remove listener to prevent memory leaks
        soundSlider.onValueChanged.RemoveListener(SetVolume);
    }
}
