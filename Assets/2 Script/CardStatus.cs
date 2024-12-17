using UnityEngine;
using UnityEngine.Events;

public class CardStatus : MonoBehaviour
{
    // Public variables
    public int CardNumber; // Number to identify the card
    public bool isTracked; // Status to indicate if the card is tracked 

    // UnityEvent array for custom behavior
    public UnityEvent[] cardBehaviors;

    // Function called when the target is found
    public void TargetFound()
    {
        isTracked = true;
        Debug.Log($"Card {CardNumber} is now tracked.");
    }

    // Function called when the target is lost
    public void TargetLost()
    {
        isTracked = false;
        Debug.Log($"Card {CardNumber} is no longer tracked.");
    }

    // Function to invoke behavior from the UnityEvent array
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
