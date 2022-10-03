using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventCard : MonoBehaviour
{
    public TimedEventData Data;

    [SerializeField] private Image _cardImage;
    [SerializeField] private TextMeshProUGUI _cardTitle;
    [SerializeField] private TextMeshProUGUI _cardDescription;

    public void SetData(TimedEventData data)
    {
        Data = data;
    }

    public void SetDataImage()
    {
        _cardImage.sprite = Data.CardImage;
        _cardTitle.text = Data.Name;
        _cardDescription.text = Data.Description;
    }

    public void SetImage(Sprite sprite)
    {
        _cardImage.sprite = sprite;
    }

    public bool ApplyEventEffect()
    {
        switch (Data.EventType)
        {
            case TimedEventType.PickAbility:
                var abilityUI = UIControl.Instance.AbilityPickUIWindow;
                List<AbilityData> abilities = GameManager.Instance.GetRandomAbilityData(2, 1);
                if (abilities.Count >= 2)
                {
                    abilityUI.SetAbilities(abilities[0], abilities[1]);
                    abilityUI.Open();
                    return true;
                }
                return false;
            case TimedEventType.RageMonsters:
                WaveControl.Instance.EnableRagingMonsters();
                return false;
            case TimedEventType.CandyBag:
                PlayerControl.Instance.AddSugar(40);
                return false;
            case TimedEventType.PowerUp:
                PlayerControl.Instance.Power += 2;
                return false;
            case TimedEventType.SUCC:
                PlayerControl.Instance.GainAbility(new Ability(GameManager.Instance.GetRandomAbilityData(2)));
                return false;
            default:
                return false;
        }
    }
}