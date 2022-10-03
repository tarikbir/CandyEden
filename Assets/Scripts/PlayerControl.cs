using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityTimer;

public class PlayerControl : SingletonMB<PlayerControl>
{
    [field: SerializeField] public float Health { get; private set; }
    [field: SerializeField] public float HealthMax { get; private set; }

    [field: SerializeField] public float Sugar { get; private set; }
    [field: SerializeField] public float SugarMax { get; private set; }

    public float Power = 0;
    public float Shield = 0;

    public AbilityData[] StarterAbilities;
    public Ability[] Abilities = new Ability[4];
    public HashSet<Ability> AbilityInventory = new();

    public GameObject ChompVFX;

    public bool IsSugarRush;
    private Timer _sugarRushTimer;
    private float _sugarRushDuration = 5f;
    private Camera _mainCamera;
    [SerializeField] private CameraShake _cameraShake;

    private void Start()
    {
        _mainCamera = GameManager.Instance.MainCamera;

        int i = 0;
        foreach (var abilityData in StarterAbilities)
        {
            var ability = new Ability(abilityData);
            GainAbility(ability);
            EquipAbility(ability, i++);
        }
    }

    private void Update()
    {
        if (GameManager.Instance.Mode == GameManager.GameMode.Fight)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Abilities[0] != null && Abilities[0].IsReady)
                {
                    if (Abilities[0].Data.CanActivateWithoutTarget)
                    {
                        Abilities[0].Activate();
                    }
                    else
                    {
                        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
                        RaycastHit hit;

                        if (Physics.Raycast(ray, out hit))
                        {
                            if (hit.collider.TryGetComponent<Enemy>(out Enemy enemy))
                            {
                                Abilities[0].Activate(enemy);
                            }
                        }
                    }
                }
            }
            else if (Input.GetMouseButtonDown(1))
            {
                if (Abilities[1] != null && Abilities[1].IsReady)
                {
                    if (Abilities[1].Data.CanActivateWithoutTarget)
                    {
                        Abilities[1].Activate();
                    }
                    else
                    {
                        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
                        RaycastHit hit;

                        if (Physics.Raycast(ray, out hit))
                        {
                            if (hit.collider.TryGetComponent<Enemy>(out Enemy enemy))
                            {
                                Abilities[1].Activate(enemy);
                            }
                        }
                    }
                }
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            UIControl.Instance.ToggleAbilityPanel();
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            GameManager.Instance.TogglePause();
        }
        else if (Input.GetKeyDown(KeyCode.H))
        {
            AddHealth(10);
        }
    }

    public void GainAbility(Ability ability)
    {
        AbilityInventory.Add(ability);
        EventControl.Instance.OnAbilityGained.Dispatch(ability, true);
    }

    public void EquipAbility(Ability ability, int slot)
    {
        int alreadyInSlot = -1;
        for (int i = 0; i < 4; i++)
        {
            if (Abilities[i] == ability)
            {
                alreadyInSlot = i;
            }
        }

        Ability alreadyInAbility = null;
        if (Abilities[slot] != null)
        {
            Abilities[slot].UnequipSlot();
            if (alreadyInSlot > -1)
            {
                alreadyInAbility = Abilities[slot];
            }
        }

        SlotAbility(ability, slot);

        if (alreadyInSlot > -1)
        {
            SlotAbility(alreadyInAbility, alreadyInSlot);
        }
    }

    private void SlotAbility(Ability ability, int slot)
    {
        Abilities[slot] = ability;
        ability.EquipSlot(slot == 0 ? ControlType.LeftClick : slot == 1 ? ControlType.RightClick : ControlType.Passive);
        EventControl.Instance.OnAbilityEquipped.Dispatch(ability, slot);
    }

    public void AddHealth(float value)
    {
        if (value < 0 && Shield > 0)
        {
            if (Shield >= value)
            {
                Shield -= value;
                UIControl.Instance.SetHealth(Health);
                return;
            }
            else
            {
                value -= Shield;
                Shield = 0;
            }
        }

        Health = Mathf.Min(Health + value, HealthMax);
        UIControl.Instance.SetHealth(Health);

        if (Health < 0)
        {
            GameManager.Instance.LoseGame();
        }
    }

    public void AddHealthMax(float value)
    {
        HealthMax += value;
        UIControl.Instance.SetHealth(Health, HealthMax);
    }

    public void AddSugar(float value)
    {
        if (IsSugarRush && value > 0) return;
        Sugar = Mathf.Max(0, Mathf.Min(Sugar + value, SugarMax));
        UIControl.Instance.SetSugar(Sugar);

        if (Sugar >= SugarMax && !IsSugarRush)
        {
            SugarRushStart();
        }
    }

    public void AddSugarMax(float value)
    {
        SugarMax += value;
        UIControl.Instance.SetSugar(Sugar, SugarMax);
    }

    public void SugarRushStart()
    {
        _sugarRushTimer = Timer.Register(_sugarRushDuration, SugarRushEnd, SugarRushTick);

        Power += 20;
        IsSugarRush = true;
        _decreaseAmount = 100f / _sugarRushDuration;
        StartCoroutine(_cameraShake.Shake(_sugarRushDuration, .5f));
        AudioControl.Instance.PlaySound("SugarRush");
    }

    private float _decreaseAmount;

    public void SugarRushTick(float time)
    {
        AddSugar(-_decreaseAmount * Time.deltaTime);
    }

    public void SugarRushEnd()
    {
        Power -= 20;
        IsSugarRush = false;
    }
}