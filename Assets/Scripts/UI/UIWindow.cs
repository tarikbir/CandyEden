using UnityEngine;

public class UIWindow : MonoBehaviour
{
    public bool IsOpen = false;

    [SerializeField] private CanvasGroup _cg;

    public void Toggle()
    {
        if (IsOpen)
        {
            Close();
        }
        else
        {
            Open();
        }
    }

    public void Open()
    {
        _cg.alpha = 1f;
        _cg.interactable = true;
        _cg.blocksRaycasts = true;
        IsOpen = true;
    }

    public void Close()
    {
        _cg.alpha = 0f;
        _cg.interactable = false;
        _cg.blocksRaycasts = false;
        IsOpen = false;
    }
}