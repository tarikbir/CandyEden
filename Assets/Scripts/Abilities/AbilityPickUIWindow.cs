using UnityEngine;
using UnityEngine.UI;

public class AbilityPickUIWindow : UIWindow
{
    [SerializeField] private Button _button1;
    [SerializeField] private Button _button2;

    private Ability _ability1;
    private Ability _ability2;

    public void SetAbilities(AbilityData ability1, AbilityData ability2)
    {
        _ability1 = new Ability(ability1);
        _ability2 = new Ability(ability2);

        _button1.image.sprite = ability1.Icon;
        _button2.image.sprite = ability2.Icon;
    }

    public void GetAbility(int select)
    {
        if (select == 1)
        {
            PlayerControl.Instance.GainAbility(_ability1);
        }
        else if (select == 2)
        {
            PlayerControl.Instance.GainAbility(_ability2);
        }

        GameManager.Instance.ResumeTimers();
        Close();
    }
}
