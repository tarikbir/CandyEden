using UnityEngine;


[CreateAssetMenu(fileName = "Ability", menuName = "Ability")]
public class AbilityData : ScriptableObject
{
    [Header("Info")]
    public string Name;
    [TextArea]
    public string Description;
    public Sprite Icon;
    public int Tier;

    [Header("Activation")]
    public TriggerType TriggerType;
    public float Cooldown;
    public bool CanActivateWithoutTarget;

    [Header("Methods")]
    public AbilityMethods ActivationMethod;
}

public enum AbilityMethods
{
    Chomp,
    GiveItALick,
    TwoHands,
    ExpressYourself,
    SUCC,
    Shield,
    Eye,
    DrinkUp
}

public enum TriggerType
{
    OnCooldown,
    OnGetHit,
    OnAnotherAbility
}