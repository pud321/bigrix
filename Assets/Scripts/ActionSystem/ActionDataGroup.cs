using UnityEngine;
using System.Collections.Generic;

public class ActionDataGroup : IActionData
{
    public List<IActionData> individual_action_data;
    public List<Item> item_modifiers;

    public ActionDataGroup()
    {
        individual_action_data = new List<IActionData>();
        item_modifiers = new List<Item>();
    }

    public void AddAction(IActionData data)
    {
        individual_action_data.Add(data);
    }

    public void RemoveAction(IActionData data)
    {
        individual_action_data.Remove(data);
    }

    public int damage
    {
        get
        {
            float temp = 0;

            foreach (IActionData data in individual_action_data)
            {
                temp += data.damage;
            }

            foreach (Item item in item_modifiers)
            {
                temp = item.ScaleDamage(temp);
            }

            foreach (Item item in item_modifiers)
            {
                temp = item.ShiftDamage(temp);
            }

            return Mathf.RoundToInt(temp);
        }
    }

    public float range
    {
        get
        {
            float temp = 0f;

            foreach (IActionData data in individual_action_data)
            {
                temp += data.range;
            }
            return temp;
        }
    }

    public float frequency
    {
        get
        {
            float temp = 0f;

            foreach (IActionData data in individual_action_data)
            {
                temp += data.frequency;
            }
            return temp;
        }
    }

    public DamageType damage_type
    {
        get
        {
            return individual_action_data[0].damage_type;
        }
    }

    public ActionType action_type
    {
        get
        {
            return individual_action_data[0].action_type;
        }
    }
}
