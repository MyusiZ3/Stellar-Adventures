using UnityEngine;
using UnityEngine.Events;

public class CardManager : MonoBehaviour
{
    // Total nilai CardValue dari kartu yang sedang dilacak
    public int CardValue;

    // Array untuk menyimpan CardStatus
    public CardStatus[] cardStatus;

    // Array untuk menyimpan objek 3D yang akan diaktifkan atau dinonaktifkan
    public GameObject[] objectsToDisable;  // Objek 3D yang akan diaktifkan/nonaktifkan

    // Class untuk mendefinisikan kondisi kartu dan event
    [System.Serializable]
    public class CardConditionEvent
    {
        public int CardCondition; // Nilai kondisi yang harus dipenuhi
        public UnityEvent onConditionMet; // Event yang akan diinvoke jika kondisi terpenuhi
        public UnityEvent onTargetLost; // Event yang akan diinvoke jika target hilang
        [HideInInspector] public bool hasExecuted; // Penanda apakah event sudah dieksekusi
        [HideInInspector] public bool wasTracked; // Menyimpan status pelacakan sebelumnya
    }

    // Array dari CardConditionEvent
    public CardConditionEvent[] cardConditions;

    // Update CardValue dan cek kondisi setiap frame
    void Update()
    {
        // Hitung total nilai CardValue berdasarkan status kartu yang terdeteksi
        int newCardValue = 0;
        foreach (var card in cardStatus)
        {
            if (card != null && card.isTracked)
            {
                newCardValue += card.CardNumber;
            }
        }

        // Jika CardValue berubah, update dan cek kondisi
        if (newCardValue != CardValue)
        {
            CardValue = newCardValue;
            CheckConditions();
        }

        // Cek kondisi target yang hilang
        CheckTargetLost();
    }

    // Cek kondisi CardConditionEvent
    private void CheckConditions()
    {
        foreach (var condition in cardConditions)
        {
            // Jika kondisi terpenuhi
            if (CardValue == condition.CardCondition && !condition.hasExecuted)
            {
                condition.onConditionMet?.Invoke();
                condition.hasExecuted = true; // Tandai sebagai sudah dieksekusi
                DisableObjects(true); // Nonaktifkan objek 3D ketika kondisi terpenuhi
            }
            // Reset jika kondisi tidak lagi terpenuhi
            else if (CardValue != condition.CardCondition)
            {
                condition.hasExecuted = false;
                DisableObjects(false); // Aktifkan objek 3D jika kondisi tidak terpenuhi
            }
        }
    }

    // Cek jika ada target yang hilang
    private void CheckTargetLost()
    {
        foreach (var condition in cardConditions)
        {
            foreach (var card in cardStatus)
            {
                // Jika kartu hilang dari pelacakan
                if (card != null && card.isTracked != condition.wasTracked)
                {
                    if (card.isTracked == false && !condition.hasExecuted)
                    {
                        condition.onTargetLost?.Invoke(); // Panggil event target hilang
                        condition.hasExecuted = false; // Reset event yang telah dieksekusi
                        DisableObjects(false); // Aktifkan objek 3D jika kartu hilang
                    }
                    condition.wasTracked = card.isTracked; // Update status pelacakan
                }
            }
        }
    }

    // Fungsi untuk menonaktifkan atau mengaktifkan objek 3D
    private void DisableObjects(bool disable)
    {
        foreach (var obj in objectsToDisable)
        {
            if (obj != null)
            {
                obj.SetActive(!disable); // Nonaktifkan objek jika disable = true
            }
        }
    }
}
