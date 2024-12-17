using UnityEngine;
using UnityEngine.Events;

public class CardManager : MonoBehaviour
{
    // Total nilai CardValue dari kartu yang sedang dilacak
    public int CardValue;

    // Array untuk menyimpan CardStatus
    public CardStatus[] cardStatus;

    // Class untuk mendefinisikan kondisi kartu dan event
    [System.Serializable]
    public class CardConditionEvent
    {
        public int CardCondition; // Nilai kondisi yang harus dipenuhi
        public UnityEvent onConditionMet; // Event yang akan diinvoke
        [HideInInspector] public bool hasExecuted; // Penanda apakah event sudah dieksekusi
    }

    // Array dari CardConditionEvent
    public CardConditionEvent[] cardConditions;

    // Update CardValue dan cek kondisi setiap frame
    void Update()
    {
        // Hitung total nilai CardValue
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
    }

    // Cek kondisi CardConditionEvent
    private void CheckConditions()
    {
        foreach (var condition in cardConditions)
        {
            if (CardValue == condition.CardCondition && !condition.hasExecuted)
            {
                condition.onConditionMet?.Invoke();
                condition.hasExecuted = true; // Tandai sebagai sudah dieksekusi
            }
            else if (CardValue != condition.CardCondition)
            {
                condition.hasExecuted = false; // Reset jika kondisi tidak lagi terpenuhi
            }
        }
    }
}
