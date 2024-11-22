using UnityEngine;
using UnityEngine.UI;

public class ImageSlider : MonoBehaviour
{
    public Button nextButton;       // Tombol untuk next image
    public Button previousButton;   // Tombol untuk previous image
    public Image targetImage;       // UI Image di mana gambar akan ditampilkan
    public Sprite[] images;         // Array untuk menyimpan urutan gambar

    private int currentIndex = 0;   // Indeks gambar saat ini

    void Start()
    {
        // Debugging untuk memeriksa apakah komponen terhubung
        if (nextButton == null)
            Debug.LogError("Next Button is not assigned!");

        if (previousButton == null)
            Debug.LogError("Previous Button is not assigned!");

        if (targetImage == null)
            Debug.LogError("Target Image is not assigned!");

        if (images.Length == 0)
            Debug.LogError("Images array is empty!");

        // Set gambar pertama saat memulai
        UpdateImage();

        // Pasang event listener untuk tombol next dan previous
        nextButton.onClick.AddListener(NextImage);
        previousButton.onClick.AddListener(PreviousImage);

        // Periksa status tombol awal
        UpdateButtonState();
    }

    // Fungsi untuk gambar berikutnya
    void NextImage()
    {
        if (currentIndex < images.Length - 1)
        {
            currentIndex++;
            UpdateImage();
        }
        UpdateButtonState();
    }

    // Fungsi untuk gambar sebelumnya
    void PreviousImage()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            UpdateImage();
        }
        UpdateButtonState();
    }

    // Fungsi untuk mengupdate gambar di UI Image
    void UpdateImage()
    {
        if (images.Length > 0 && targetImage != null)
        {
            targetImage.sprite = images[currentIndex];
        }
    }

    // Fungsi untuk update status tombol (enable/disable)
    void UpdateButtonState()
    {
        previousButton.interactable = (currentIndex > 0);
        nextButton.interactable = (currentIndex < images.Length - 1);
    }
}
