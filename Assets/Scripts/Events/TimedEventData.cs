﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


[CreateAssetMenu(fileName = "Timed Event", menuName = "Timed Event", order = 1)]
public class TimedEventData : ScriptableObject
{
    public string Name;
    [TextArea]
    public string Description;

    public Sprite CardImage;
    public TimedEventType EventType;

    public bool IsOneOfAKind;
}

public enum TimedEventType
{
    PickAbility,
    RageMonsters,
    CandyBag,
    PowerUp,
    SUCC
}