using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public delegate void ExperienceEventHandler();

public class ExperienceSystem
{
    public Dictionary<uint, uint> level_xp;
    private float scaling_factor = 1.5f;
    private float shift = 10f;

    public class XP_Data
    {
        public uint level;
        public uint xp;
        public uint next_xp;
        public event ExperienceEventHandler OnLevelUp;

        public XP_Data()
        {
            level = 1;
            xp = 0;
            next_xp = NextXP();
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

    public ExperienceSystem(float scaling_factor, float shift)
    {
        this.scaling_factor = scaling_factor;
        this.shift = shift;

        level_xp = new Dictionary<uint, uint>();

        for (uint i=1; i < GameStats.max_level; i++)
        {
            level_xp[i] = GetXPNeeded(i);
        }
    }


    private uint GetXPNeeded(uint level)
    {
        if (level == GameStats.max_level)
        {
            return 0;
        }

        return (uint)Mathf.RoundToInt(shift * Mathf.Pow(level, scaling_factor));
    }

}
