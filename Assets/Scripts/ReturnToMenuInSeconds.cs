using UnityEngine;
using UnityEngine.SceneManagement;
using UnityTimer;

public class ReturnToMenuInSeconds : MonoBehaviour
{
    [SerializeField] private float _amountOfSecondsAfterTransition;
    [SerializeField] private int _sceneIndexToTransition;

    void Start()
    {
        Timer.Register(_amountOfSecondsAfterTransition, TimerEnd);
    }

    private void TimerEnd()
    {
        SceneManager.LoadScene(_sceneIndexToTransition);
    }
}