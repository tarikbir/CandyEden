using TMPro;
using UnityTimer;

public class GameTimedEventControl : SingletonMB<GameTimedEventControl>
{
    public float SecondsInterval = 10f;
    
    private Timer _eventTimer;

    void Start()
    {
        _eventTimer = Timer.Register(SecondsInterval, TimerComplete, TimerUpdate, true, true);
    }

    private void TimerUpdate(float time)
    {
        UpdateText();
    }

    private void TimerComplete()
    {
        UpdateText();

        EventControl.Instance.OnTimedEvent.Dispatch();

        GameManager.Instance.PauseTimers();
        //

        //do something
    }

    private void UpdateText(float time = 0)
    {
        UIControl.Instance.UpdateTimer(_eventTimer.GetTimeRemaining(), _eventTimer.GetRatioRemaining());
    }
}