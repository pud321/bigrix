using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string name;

    private int damage = 0;
    private float damage_percent = 0f;
    private float range = 0f;
    private float frequency_percent = 0f;

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
