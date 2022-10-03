using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIControl : SingletonMB<UIControl>
{
    [Header("Main References")]
    public Canvas MainCanvas;

    [Header("Bars")]
    public ProgressBar HealthBar;
    public ProgressBar SugarBar;

    [Header("Ability")]
    public UIWindow AbilityWindow;
    public AbilitySlotController[] AbilitySlots;
    public AbilityPickerItem AbilityPickerItemPrefab;
    public AbilityPickerSlot[] AbilityPickerSlots;
    public List<AbilityPickerItem> AbilityInventoryList = new();
    public GridLayoutGroup AbilityInventoryGrid;

    [Header("Tooltip")]
    public TooltipControl Tooltip;

    [Header("Wave")]
    public TextMeshProUGUI WaveText;

    [Header("Event")]
    public AbilityPickUIWindow AbilityPickUIWindow;
    public TextMeshProUGUI TimerText;
    public Image TimerFill;

    private void Start()
    {
        EventControl.Instance.OnAbilityEquipped.AddListener(UpdateAbilityEquipped);
        EventControl.Instance.OnAbilityGained.AddListener(UpdateAbilityInventory);
    }

    private void Update()
    {
        if (Tooltip.IsOn)
        {
            Tooltip.UpdatePosition(Input.mousePosition);
        }
    }

    public void OpenAbilityPickPanel()
    {
        AbilityPickUIWindow.Open();
    }

    public void ToggleAbilityPanel()
    {
        AbilityWindow.Toggle();
    }

    public void SetHealth(float val, float newMax = -1)
    {
        HealthBar.Current = val;
        if (newMax != -1)
        {
            HealthBar.Setup(newMax);
        }
    }

    public void SetSugar(float val, float newMax = -1)
    {
        SugarBar.Current = val;
        if (newMax != -1)
        {
            SugarBar.Setup(newMax);
        }
    }

    public void SetWaveText(int waveCount)
    {
        if (waveCount <= WaveControl.Instance.WaveTimes.Length)
        {
            WaveText.text = $"Wave {waveCount}";
        }
        else
        {
            WaveText.text = $"Final Wave";
        }
    }

    public void UpdateTimer(float remaining, float remainingRatio)
    {
        TimerText.text = string.Format("{0, 0:F1}", remaining);
        TimerFill.fillAmount = remainingRatio;
    }

    private void UpdateAbilityInventory(Ability ability, bool isAdded)
    {
        if (isAdded)
        {
            var abilityPickerItem = Instantiate(AbilityPickerItemPrefab, AbilityInventoryGrid.transform, true);
            abilityPickerItem.SetAbility(ability);
            AbilityInventoryList.Add(abilityPickerItem);
        }
        else
        {
            var abilityToBeRemoved = AbilityInventoryList.Find(a => a.PickedAbility == ability);
            AbilityInventoryList.Remove(abilityToBeRemoved);
            Destroy(abilityToBeRemoved.gameObject);
        }
    }

    private void UpdateAbilityEquipped(Ability ability, int slot)
    {
        if (ability != null)
        {
            AbilitySlots[slot].SetAbility(PlayerControl.Instance.Abilities[slot]);
            AbilityPickerSlots[slot].SetAbilityIcon(PlayerControl.Instance.Abilities[slot]);
        }
    }
}
