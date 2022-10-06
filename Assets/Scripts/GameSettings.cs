using UnityEngine;


[CreateAssetMenu(fileName = "Game Settings", menuName = "Settings", order = 1)]
public class GameSettings : ScriptableObject
{
    public bool EnableThrowingCandies;
    public bool EnableTooltipPauseGame;
}