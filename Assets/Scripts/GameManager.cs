using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityTimer;

public class GameManager : SingletonMB<GameManager>
{
    public GameMode Mode;
    public Camera MainCamera;
    public GameSettings Settings;

    public List<AbilityData> AllAbilityList;
    public List<TimedEventData> AllEventList;

    private void Start()
    {
        Mode = GameMode.Fight;
    }

    public void PauseTimers(bool checkReveal = false)
    {
        if (checkReveal && EventRevealer.IsRevealing) return;
        Timer.PauseAllRegisteredTimers();
        Mode = GameMode.Pause;
    }

    public void ResumeTimers(bool checkReveal = false)
    {
        if (checkReveal && EventRevealer.IsRevealing) return;
        Timer.ResumeAllRegisteredTimers();
        Mode = GameMode.Fight;
    }

    public AbilityData GetRandomAbilityData(int tier = 1)
    {
        return GetRandomAbilityData(1, tier)[0];
    }

    public List<AbilityData> GetRandomAbilityData(int amount, int tier = 1)
    {
        return AllAbilityList.Where(a => a.Tier == tier).OrderBy(a => Random.Range(0, AllAbilityList.Count)).Take(amount).ToList();
    }

    public TimedEventData GetRandomEventData()
    {
        var gotEvent = AllEventList.OrderBy(e => Random.Range(0, AllEventList.Count)).Take(1).FirstOrDefault();
        if (gotEvent.IsOneOfAKind)
        {
            AllEventList.Remove(gotEvent);
        }
        return gotEvent;
    }

    public void WinGame()
    {
        SceneManager.LoadScene(3, LoadSceneMode.Single);
    }

    public void LoseGame()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }

    internal void TogglePause(bool checkReveal = false)
    {
        if (Mode == GameMode.Pause)
        {
            ResumeTimers(checkReveal);
        }
        else
        {
            PauseTimers(checkReveal);
        }
    }

    public enum GameMode
    {
        Fight,
        Pause
    }
}
