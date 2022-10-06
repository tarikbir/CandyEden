using TMPro;
using UnityEngine;

public class TooltipControl : MonoBehaviour
{
    public bool IsOn => _uiWindow.IsOpen;

    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private UIWindow _uiWindow;

    public void UpdatePosition(Vector3 position)
    {
        float pivotX = position.x / Screen.width;
        float pivotY = position.y / Screen.height;

        _rectTransform.pivot = new Vector2(pivotX, pivotY);
        transform.position = position;
    }

    public void ShowWithText(string title, string description)
    {
        if (GameManager.Instance.Settings.EnableTooltipPauseGame) GameManager.Instance.PauseTimers(true);
        _titleText.text = title;
        _descriptionText.text = description;
        _uiWindow.Open();
    }

    public void Hide()
    {
        _uiWindow.Close();
        if (GameManager.Instance.Settings.EnableTooltipPauseGame) GameManager.Instance.ResumeTimers(true);
    }
}
