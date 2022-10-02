using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AbilityPickerSlot : MonoBehaviour, IDropHandler
{
    public static bool CaughtDroppedAbility;
    public ControlType ControlType;
    public int SlotIndex;

    [SerializeField] private Image _image;

    private Sprite _defaultSprite;

    private void Awake()
    {
        _defaultSprite = _image.sprite;
    }

    public void SetAbilityIcon(Ability ability)
    {
        if (ability != null)
        {
            _image.sprite = ability.Data.Icon;
        }
        else
        {
            _image.sprite = _defaultSprite;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            if (AbilityPickerItem.CurrentlyDraggingAbilityPicker != null)
            {
                var ability = AbilityPickerItem.CurrentlyDraggingAbilityPicker.PickedAbility;

                if (ability != null)
                {
                    PlayerControl.Instance.EquipAbility(ability, SlotIndex);
                }
            }
        }
    }
}