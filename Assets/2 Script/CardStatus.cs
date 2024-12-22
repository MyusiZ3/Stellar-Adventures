using UnityEngine;
using UnityEngine.Events;

public class CardStatus : MonoBehaviour
{
    // Public variables
    public int CardNumber; // Nomor kartu untuk mengidentifikasi kartu
    public bool isTracked; // Status untuk menunjukkan apakah kartu sedang dilacak

    // UnityEvent array untuk custom behavior
    public UnityEvent[] cardBehaviors; // Event yang bisa dipanggil ketika status berubah

    // Fungsi dipanggil ketika target ditemukan
    public void TargetFound()
    {
        isTracked = true;
        Debug.Log($"Card {CardNumber} is now tracked.");
        InvokeBehavior(0);  // Misalnya panggil behavior pertama saat kartu ditemukan
    }

    // Fungsi dipanggil ketika target hilang
    public void TargetLost()
    {
        isTracked = false;
        Debug.Log($"Card {CardNumber} is no longer tracked.");
        InvokeBehavior(1);  // Misalnya panggil behavior kedua saat kartu hilang
    }

    // Fungsi untuk memanggil behavior dari UnityEvent array
    public void InvokeBehavior(int index)
    {
        if (index >= 0 && index < cardBehaviors.Length)
        {
            cardBehaviors[index]?.Invoke();
        }
        else
        {
            Debug.LogWarning($"Invalid index {index}. No behavior invoked.");
        }
    }
}
