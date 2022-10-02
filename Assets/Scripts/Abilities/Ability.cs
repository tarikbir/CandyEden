using System.Numerics;
using UnityEngine.XR;
using UnityTimer;

public class Ability
{
	public AbilityData Data;

	public string Name;
	public string Description;

	public bool IsReady;
	public float Cooldown;
    public Timer CountdownTimer;

	public ControlType ControlType;

	private AbilityMethod _activationMethod;

	public Ability(AbilityData data)
	{
        Data = data;
		Name = data.Name;
		Description = data.Description;
		Cooldown = data.Cooldown;
		IsReady = true;
        _activationMethod = GetMethod(data.ActivationMethod);
    }

	public void Ready()
	{
		if (CountdownTimer != null)
		{
			CountdownTimer.Cancel();
		}

		IsReady = true;
		SubscribeToTriggerEvent();
    }

	public void EquipSlot(ControlType controlType)
	{
		ControlType = controlType;
		Ready();
    }

	public void UnequipSlot()
	{
        if (CountdownTimer != null)
        {
		    CountdownTimer.Cancel();
        }

		IsReady = false;

		if (ControlType == ControlType.Passive)
		{
			if (EventControl.Instance.OnGetHit.Contains(Activate))
			{
				EventControl.Instance.OnGetHit.RemoveOnce(Activate);
			}
			else if (EventControl.Instance.OnAbilityActivate.Contains(ActivateWithAbility))
			{
				EventControl.Instance.OnAbilityActivate.RemoveOnce(ActivateWithAbility);
			}
		}
    }

	public void Activate(Enemy enemy)
	{
        _activationMethod.Invoke(ControlType, enemy);
        CountdownTimer = Timer.Register(Cooldown, Ready);
        IsReady = false;
        EventControl.Instance.OnAbilityActivate.Dispatch(this);
    }

	public void Activate()
	{
		Activate(null);
	}

    private void ActivateWithAbility(Ability otherAbility)
    {
        if (otherAbility != this && IsReady)
        {
            Activate();
        }
    }

	private void SubscribeToTriggerEvent()
	{
        if (ControlType is ControlType.Passive)
        {
            switch (Data.TriggerType)
            {
                case TriggerType.OnCooldown:
                    Activate();
                    break;
                case TriggerType.OnGetHit:
                    EventControl.Instance.OnGetHit.AddOnce(Activate);
                    break;
                case TriggerType.OnAnotherAbility:
                    EventControl.Instance.OnAbilityActivate.AddOnce(ActivateWithAbility);
                    break;
                default:
                    break;
            }
        }
    }

	private AbilityMethod GetMethod(AbilityMethods method)
	{
		return method switch
		{
			AbilityMethods.Chomp => new ChompMethod(10),
            AbilityMethods.TwoHands => new TwoHandMethod(2),
            AbilityMethods.GiveItALick => new GiveItALickMethod(4),
            AbilityMethods.ExpressYourself => new ExpressYourselfMethod(2),
            AbilityMethods.SUCC => new SUCCMethod(5),
            AbilityMethods.Shield => new ShieldMethod(5),
            AbilityMethods.Eye => new EyeMethod(1),
            AbilityMethods.DrinkUp => new DrinkUpMethod(20),
            _ => null
		};
	}
}

public abstract class AbilityMethod
{
    public float Power;
    
    public AbilityMethod(float powerOverride = 10)
    {
        Power = powerOverride + PlayerControl.Instance.Power;
    }

    public abstract void Invoke(ControlType controlType, Enemy enemy = null);
}

public class ChompMethod : AbilityMethod
{
    public ChompMethod(float powerOverride = 10) : base(powerOverride) { }

    public override void Invoke(ControlType controlType, Enemy enemy = null)
    {
        if (controlType == ControlType.Passive)
        {
            enemy = WaveControl.Instance.GetRandomEnemy();
        }
        if (enemy == null) return;

        UnityEngine.GameObject.Instantiate(PlayerControl.Instance.ChompVFX, enemy.transform.position + UnityEngine.Vector3.up * 2, UnityEngine.Quaternion.identity);

        if (enemy.GetDamage(Power))
        {
            PlayerControl.Instance.AddSugar(enemy.SugarValue);
        }
    }
}

public class TwoHandMethod : AbilityMethod
{
    public TwoHandMethod(float powerOverride = 10) : base(powerOverride) { }

    public override void Invoke(ControlType controlType, Enemy enemy = null)
    {
        if (controlType == ControlType.Passive)
        {
            enemy = WaveControl.Instance.GetRandomEnemy();
        }
        if (enemy == null) return;

        //UnityEngine.GameObject.Instantiate(PlayerControl.Instance.ChompVFX, enemy.transform.position + UnityEngine.Vector3.up * 2, UnityEngine.Quaternion.identity);
        float damage = Power;
        if (controlType == ControlType.LeftClick)
        {
            damage *= 3;
        }
        else if (controlType == ControlType.RightClick)
        {
            damage *= 5;
        }

        enemy.GetDamage(damage);
    }
}

public class GiveItALickMethod : AbilityMethod
{
    public GiveItALickMethod(float powerOverride = 10) : base(powerOverride) { }

    public override void Invoke(ControlType controlType, Enemy enemy = null)
    {
        if (controlType == ControlType.Passive)
        {
            PlayerControl.Instance.AddHealth(Power * 3);
        }
        if (enemy == null) return;
        enemy.GetDamage(Power);
        PlayerControl.Instance.AddHealth(Power * 2);
    }
}

public class ExpressYourselfMethod : AbilityMethod
{
    public ExpressYourselfMethod(float powerOverride = 10) : base(powerOverride) { }

    public override void Invoke(ControlType controlType, Enemy enemy = null)
    {
        if (controlType == ControlType.Passive)
        {
            enemy = WaveControl.Instance.GetRandomEnemy();
            if (enemy == null) return;
            enemy.ShittedOn(Power);
        }
        else
        {
            if (enemy == null) return;
            enemy.ShittedOn(Power / 2);
        }

        //UnityEngine.GameObject.Instantiate(PlayerControl.Instance.ChompVFX, enemy.transform.position + UnityEngine.Vector3.up * 2, UnityEngine.Quaternion.identity);
    }
}

public class SUCCMethod : AbilityMethod
{
    public SUCCMethod(float powerOverride = 10) : base(powerOverride){ }

    public override void Invoke(ControlType controlType, Enemy enemy = null)
    {
        if (controlType == ControlType.Passive)
        {
            PlayerControl.Instance.AddHealth(Power * 4);
        }
        if (enemy == null) return;
        enemy.GetDamage(Power);
        PlayerControl.Instance.AddHealth(Power * 2);
    }
}

public class ShieldMethod : AbilityMethod
{
    public ShieldMethod(float powerOverride = 10) : base(powerOverride){ }

    public override void Invoke(ControlType controlType, Enemy enemy = null)
    {
    }
}

public class EyeMethod : AbilityMethod
{
    public EyeMethod(float powerOverride = 10) : base(powerOverride){ }

    public override void Invoke(ControlType controlType, Enemy enemy = null)
    {
    }
}

public class DrinkUpMethod : AbilityMethod
{
    public DrinkUpMethod(float powerOverride = 10) : base(powerOverride){ }

    public override void Invoke(ControlType controlType, Enemy enemy = null)
    {
    }
}

public enum ControlType
{
	LeftClick,
	RightClick,
	Passive
}