using System;

public class DamageEventArgs : EventArgs
{
    public int damage_value { get; set; }
    public DamageType damage_type { get; set; }

    public DamageEventArgs(int damage_value, DamageType damage_type)
    {
        this.damage_value = damage_value;
        this.damage_type = damage_type;
    }

}
