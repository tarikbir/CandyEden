using UnityEngine;
using UnityEngine.UI;

public class TimedEvent : MonoBehaviour
{
    public TimedEventData Data;

    [SerializeField] private Image _cardImage;

    public void SetData(TimedEventData data)
    {
        Data = data;
    }
}