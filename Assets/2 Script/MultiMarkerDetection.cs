using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class MultiMarkerDetection : MonoBehaviour
{
    public GameObject[] cardObjects;  // Objek yang akan ditampilkan untuk masing-masing marker
    public GameObject specialObject;  // Objek khusus yang akan muncul jika semua marker terdeteksi
    public GameObject pageMarkerObject; // Objek marker halaman (page marker)

    private Dictionary<string, bool> markerStates = new Dictionary<string, bool>();
    private bool allCardsDetected = false;
    private bool pageMarkerDetected = false;

    void Start()
    {
        // Inisialisasi semua marker sebagai tidak terdeteksi
        foreach (GameObject card in cardObjects)
        {
            markerStates[card.name] = false;
        }

        // Pastikan objek khusus dan page marker tidak aktif di awal
        if (specialObject != null)
        {
            specialObject.SetActive(false);
        }

        if (pageMarkerObject != null)
        {
            pageMarkerObject.SetActive(false);
        }
    }

    public void OnCardMarkerFound(string markerName)
    {
        // Jika marker kartu terdeteksi, update statusnya
        if (markerStates.ContainsKey(markerName))
        {
            markerStates[markerName] = true;
            UpdateObjects();
        }
    }

    public void OnCardMarkerLost(string markerName)
    {
        // Jika marker kartu hilang, update statusnya
        if (markerStates.ContainsKey(markerName))
        {
            markerStates[markerName] = false;
            UpdateObjects();
        }
    }

    public void OnPageMarkerFound()
    {
        // Menandakan bahwa marker halaman (page marker) terdeteksi
        pageMarkerDetected = true;
        pageMarkerObject.SetActive(true); // Menampilkan page marker
        UpdateObjects();
    }

    public void OnPageMarkerLost()
    {
        // Menandakan bahwa marker halaman hilang
        pageMarkerDetected = false;
        pageMarkerObject.SetActive(false); // Menyembunyikan page marker
        UpdateObjects();
    }

    private void UpdateObjects()
    {
        // Cek apakah semua marker kartu terdeteksi
        allCardsDetected = true;
        foreach (var state in markerStates.Values)
        {
            if (!state)
            {
                allCardsDetected = false;
                break;
            }
        }

        // Jika semua kartu terdeteksi dan halaman juga terdeteksi
        if (allCardsDetected && pageMarkerDetected)
        {
            specialObject.SetActive(true); // Aktifkan objek spesial

            // Nonaktifkan semua objek kartu
            foreach (GameObject card in cardObjects)
            {
                card.SetActive(false);
            }
        }
        else
        {
            specialObject.SetActive(false); // Nonaktifkan objek spesial

            // Aktifkan objek kartu yang sesuai dengan marker yang terdeteksi
            foreach (GameObject card in cardObjects)
            {
                card.SetActive(markerStates[card.name]);
            }
        }
    }
}
