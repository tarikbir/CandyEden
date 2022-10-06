using UnityEngine;
using UnityEngine.UI;

public class AbilityPickUIWindow : UIWindow
{
    [SerializeField] private AbilityPickUIWindowAbility _button1;
    [SerializeField] private AbilityPickUIWindowAbility _button2;

    private Ability _ability1;
    private Ability _ability2;

    public void SetAbilities(AbilityData ability1, AbilityData ability2)
    {
        _ability1 = new Ability(ability1);
        _ability2 = new Ability(ability2);

        _button1.SetAbility(_ability1);
        _button2.SetAbility(_ability2);
    }

    public void GetAbility(int select)
    {
        if (select == 1)
        {
            PlayerControl.Instance.GainAbility(_ability1);
            GameManager.Instance.AllAbilityList.Remove(_ability1.Data);
        }
        else if (select == 2)
        {
            PlayerControl.Instance.GainAbility(_ability2);
            GameManager.Instance.AllAbilityList.Remove(_ability2.Data);
        }

        GameManager.Instance.ResumeTimers();
        Close();
    }
}
