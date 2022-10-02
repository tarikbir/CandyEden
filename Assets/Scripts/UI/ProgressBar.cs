using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class ProgressBar : MonoBehaviour
{
    public float Minimum;
    public float Maximum;
    public float Current;

    [SerializeField] private Image _mask;
    [SerializeField] private TextMeshProUGUI _text;

    public void Setup(float maximum)
    {
        Maximum = maximum;
        UpdateText();
    }

    public void Setup(float current, float maximum)
    {
        Current = current;
        Setup(maximum);
    }

    private void Update()
    {
        GetCurrentFill();
    }

    private void GetCurrentFill()
    {
        float currentOffset = Current - Minimum;
        float maximumOffset = Maximum - Minimum;
        float fillAmount = currentOffset / maximumOffset;
        _mask.fillAmount = fillAmount;
        UpdateText();
    }

    private void UpdateText()
    {
        if (_text != null)
        {
            _text.text = $"{Current:F0}/{Maximum:F0}";
        }
    }
}