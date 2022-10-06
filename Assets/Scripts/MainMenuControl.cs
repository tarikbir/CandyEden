using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuControl : MonoBehaviour
{
    [SerializeField] private AudioClip ClickSound;

    [SerializeField] private GameSettings SettingsAsset;
    [SerializeField] private AudioSource MenuSounds;

    public void StartGame()
    {
        MenuSounds.PlayOneShot(ClickSound);
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void Exit()
    {
        MenuSounds.PlayOneShot(ClickSound);
        Application.Quit();
    }

    public void EnableCandyThrows(bool enable)
    {
        MenuSounds.PlayOneShot(ClickSound);
        SettingsAsset.EnableThrowingCandies = enable;
    }

    public void EnableTooltipPause(bool enable)
    {
        MenuSounds.PlayOneShot(ClickSound);
        SettingsAsset.EnableTooltipPauseGame = enable;
    }
}
