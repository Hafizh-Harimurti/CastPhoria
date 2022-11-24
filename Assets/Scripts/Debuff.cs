using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Debuff
{
    Burn,
    Slow,
    Stun
}

public class DebuffInfo
{
    public Debuff debuff;
    public float duration;
    public float strength;

    public DebuffInfo(Debuff debuff, float duration, float strength)
    {
        this.debuff = debuff;
        this.duration = duration;
        this.strength = strength;
    }
}
