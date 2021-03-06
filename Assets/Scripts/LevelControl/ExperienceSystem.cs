using System.Collections.Generic;
using UnityEngine;

public delegate void ExperienceEventHandler();

public class ExperienceSystem
{
    public Dictionary<uint, uint> level_xp;
    private float scaling_factor;
    private float shift;

    public ExperienceSystem(float scaling_factor, float shift)
    {
        this.scaling_factor = scaling_factor;
        this.shift = shift;

        level_xp = new Dictionary<uint, uint>();

        for (uint i = 1; i < GameStats.max_level; i++)
        {
            level_xp[i] = GetXPNeeded(i);
        }
    }

    public uint GetXPNeeded(uint level)
    {
        if (level == GameStats.max_level)
        {
            return 0;
        }

        return (uint)Mathf.RoundToInt(shift * Mathf.Pow(level, scaling_factor));
    }

}
