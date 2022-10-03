using UnityEngine;
using UnityEngine.EventSystems;

public class AbilityTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private IAbilityTooltip _abilityComponent;

    private void Awake()
    {
        _abilityComponent = GetComponent<IAbilityTooltip>();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIControl.Instance.Tooltip.Hide();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_abilityComponent.Ability != null)
        {
            UIControl.Instance.Tooltip.ShowWithText(_abilityComponent.Ability.Name, _abilityComponent.Ability.Description);
        }
    }
}

public interface IAbilityTooltip
{
    public Ability Ability { get; }
}