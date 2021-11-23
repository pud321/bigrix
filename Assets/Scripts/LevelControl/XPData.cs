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
        next_xp = 0;
    }

    public float LevelFraction()
    {
        if (level == GameStats.max_level)
        {
            return 1f;
        }

        if (next_xp == 0)
        {
            next_xp = NextXP();
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
            OnLevelUp?.Invoke();
            this.xp = this.xp - this.next_xp;
            this.next_xp = GameStats.experience_system.GetXPNeeded(level);
            AddXp(0);
        }
    }

    public uint NextXP()
    {
        return GameStats.experience_system.GetXPNeeded(level);
    }
}
