using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemController
{
    private Item[] items;

    public ItemController(int items_quantity)
    {
        items = new Item[items_quantity];
    }

    public Item AddItem(Item item, int slot)
    {
        Item previous_item = items[slot];
        items[slot] = item;
        return previous_item;
    }

    public int GetItemDamage(int value)
    {
        float temp_value = (float)value;

        foreach (Item item in items)
        {
            temp_value = item.ScaleDamage(temp_value);
        }

        value = Mathf.RoundToInt(temp_value);

        foreach (Item item in items)
        {
            value = item.ShiftDamage(value);
        }

        return value;
    }

    public float GetItemFrequency(float value)
    {
        foreach (Item item in items)
        {
            value = item.ScaleFrequency(value);
        }
        return value;
    }

    public float GetItemRange(float value)
    {
        foreach (Item item in items)
        {
            value = item.ShiftRange(value);
        }
        return value;
    }
}
