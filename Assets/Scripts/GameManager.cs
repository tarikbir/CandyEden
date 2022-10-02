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

    private void Start()
    {
        Mode = GameMode.Fight;
    }

    public void PauseTimers()
    {
        Timer.PauseAllRegisteredTimers();
        Mode = GameMode.Pause;
    }

    public void ResumeTimers()
    {
        Timer.ResumeAllRegisteredTimers();
        Mode = GameMode.Fight;
    }

    public AbilityData GetRandomAbilityData(int tier = 0)
    {
        return GetRandomAbilityData(1, tier).FirstOrDefault();
    }

    public IEnumerable<AbilityData> GetRandomAbilityData(int amount, int tier = 0)
    {
        return AllAbilityList.Where(a => a.Tier <= tier).OrderBy(a => Random.Range(0, AllAbilityList.Count)).Take(amount);
    }

    public void WinGame()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void LoseGame()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public enum GameMode
    {
        Fight,
        Pause
    }
}
