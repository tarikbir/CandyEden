using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AbilitySlotController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Ability SlottedAbility;

    [SerializeField] private Image _slotImage;
    [SerializeField] private Image _cooldownImage;
    [SerializeField] private TextMeshProUGUI _cooldownText;

    private Sprite _defaultSprite;

    private void Awake()
    {
        _defaultSprite = _slotImage.sprite;
    }

    private void Update()
    {
        if (SlottedAbility != null && SlottedAbility.CountdownTimer != null && !SlottedAbility.IsReady)
        {
            _cooldownImage.fillAmount = SlottedAbility.CountdownTimer.GetRatioRemaining();
            _cooldownText.text = string.Format("{0, 0:F1}", SlottedAbility.CountdownTimer.GetTimeRemaining());
        }
        else
        {
            _cooldownImage.fillAmount = 0;
            _cooldownText.text = "";
        }
    }

    public void SetAbility(Ability ability)
    {
        SlottedAbility = ability;

        if (ability != null)
        {
            _slotImage.sprite = ability.Data.Icon;
        }
        else
        {
            _slotImage.sprite = _defaultSprite;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIControl.Instance.Tooltip.Hide();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (SlottedAbility != null)
        {
            UIControl.Instance.Tooltip.ShowWithText(SlottedAbility.Name, SlottedAbility.Description);
        }
    }
}