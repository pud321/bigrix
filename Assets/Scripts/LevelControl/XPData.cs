using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class XPData
{
    public uint level;
    public uint xp;
    public uint next_xp;
    public event ExperienceEventHandler OnLevelUp;

    public XPData()
    {
        level = 1;
        xp = 0;
    }

    public float LevelFraction()
    {
        if (level == GameStats.max_level)
        {
            return 1f;
        }

        return (float)xp / (float)next_xp;
    }

    public bool isLevelUp
    {
        get { return xp >= next_xp; }
    }

    public void AddXp(uint xp)
    {
        next_xp = NextXP();
        this.xp += xp;

        if (isLevelUp)
        {
            this.level += 1;
            this.xp = this.xp - this.next_xp;
            this.next_xp = GameStats.experience_system.GetXPNeeded(level);
            OnLevelUp?.Invoke();
        }
    }

    private uint NextXP()
    {
        return GameStats.experience_system.GetXPNeeded(level);
    }
}
