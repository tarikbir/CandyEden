using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AbilityPickUIWindowAbility : Button, IAbilityTooltip
{
    public Ability Ability { get; set; }

    public void SetAbility(Ability ability)
    {
        Ability = ability;

        image.sprite = ability.Data.Icon;
    }
}