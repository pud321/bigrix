using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterData : CharacterCurrentData
{
    public uint experience;
    public uint level;
    public ItemController inventory_controller;


    public delegate void InventoryChangeHandler(IAction action, int slot);
    public event InventoryChangeHandler OnInventoryChange;

    public PlayerCharacterData(CharacterEnums type, uint level) : base(type)
    {
        this.level = level;
        experience = 0;
        inventory_controller = new ItemController(3);
    }

    public void ReportInventoryChange(object o)
    {
        OnInventoryChange?.Invoke(null, 1);
    }

    public void AddExperience(uint experience)
    {
        Debug.Log(this.experience);
        this.experience += experience;
        Debug.Log(this.experience);
    }

    public void AddItem(Item item, int slot)
    {
        Item removed_item = inventory_controller.AddItem(item, slot);

        if (removed_item != null)
        {
            // Report item to inventory
        }
    }
}
