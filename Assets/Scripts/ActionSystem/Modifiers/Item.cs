using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public string name;

    public int damage = 0;
    public float damage_percent = 0f;
    public float range = 0f;
    public float frequency_percent = 0f;

    public Item(string name)
    {
        this.name = name;
    }

    public float ShiftRange(float value)
    {
        return value + this.range;
    }

    public int ShiftDamage(int value)
    {
        return value + this.damage;
    }

    public float ScaleDamage(float value)
    {
        return (1 + damage_percent) * value;
    }

    public float ScaleFrequency(float value)
    {
        return (1 + frequency_percent) * value;
    }
}
