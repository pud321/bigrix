using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterData : CharacterCurrentData
{
    public ExperienceSystem.XP_Data experience;
    public ItemController inventory_controller;
    public uint level;


    public delegate void InventoryChangeHandler(IAction action, int slot);
    public event InventoryChangeHandler OnInventoryChange;

    public int level_health_modifier = 0;
    public int level_damage_modifier = 0;

    public PlayerCharacterData(CharacterEnums type, uint level) : base(type)
    {
        this.level = level;
        experience = new ExperienceSystem.XP_Data
        {
            level = level,
            xp = 0,
            next_xp = 1
        };
        experience.OnLevelUp += LevelChangeResponse;
        inventory_controller = new ItemController(3);
    }

    public void ReportInventoryChange(object o)
    {
        OnInventoryChange?.Invoke(null, 1);
    }

    public void AddExperience(uint experience)
    {
        this.experience.AddXp(experience);
    }

    public void AddItem(Item item, int slot)
    {
        Item removed_item = inventory_controller.AddItem(item, slot);

        if (removed_item != null)
        {
            // Report item to inventory
        }
    }

    public override int max_health
    {
        get
        {
            return fixed_data.max_health + level_health_modifier;
        }
    }

    public override int base_damage
    {
        get
        {
            return fixed_data.basic_attack.damage + level_damage_modifier;
        }
    }

    private void LevelChangeResponse()
    {
        level_health_modifier += 200;
        level_damage_modifier += 25;
    }
}
