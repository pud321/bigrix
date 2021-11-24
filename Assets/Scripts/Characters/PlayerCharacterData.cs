using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class PlayerCharacterData : CharacterCurrentData
{
    public XPData experience;
    public ItemInventoryController inventory_controller;
    private ActionData leveling_action_data;

    protected virtual int max_inventory_slots { get { return 3; } }

    public uint level { get { return experience.level; } }

    public event ManagerEventSender<PlayerCharacterData> OnLevelChange;
    private List<Item> item_modifiers;

    public PlayerCharacterData() : base()
    {
        experience = new XPData();
        item_modifiers = new List<Item>();

        experience.OnLevelUp += AdvanceLevelListener;
        inventory_controller = new ItemInventoryController(max_inventory_slots, 1);
        inventory_controller.OnInventoryUpdate += UpdateInventory;
    }

    public void SetupPlayerAttackGroup()
    {
        SetupAttackGroup();
        leveling_action_data = new ActionData();
        leveling_action_data.damage_type = basic_attack_group.damage_type;
        leveling_action_data.action_type = basic_attack_group.action_type;
        basic_attack_group.AddAction(leveling_action_data);
    }

    public void AddExperience(uint experience)
    {
        this.experience.AddXp(experience);
    }

    public override int max_health
    {
        get
        {
            float temp = fixed_data.max_health;

            foreach (Item item in item_modifiers)
            {
                temp = item.ShiftHealth(temp);
            }

            return Mathf.RoundToInt(temp);
        }
    }

    public override int base_damage
    {
        get
        {
            return leveling_action_data.damage;
        }
    }

    public void AdvanceLevelListener()
    {
        leveling_action_data.ChangeDamage(fixed_data.levelup_damage);
        fixed_data.RunLevelUp();
        OnLevelChange?.Invoke(this);
    }

    public void RemakeCharacter(PlayerCharacterData character_data)
    {
        for (int i = 1; i < character_data.experience.level; i++)
        {
            AdvanceLevelListener();
        }

        experience.xp = character_data.experience.xp;
        experience.level = character_data.experience.level;
        inventory_controller.RestoreInventoryObjects(character_data.inventory_controller);
    }

    public void UpdateInventory(ItemSlotData[] item_data_list)
    {
        List<Item> basic_item_modifiers = new List<Item>();
        item_modifiers = new List<Item>();

        foreach (ItemSlotData data in item_data_list)
        {
            if (data == null || data.item == null)
            {
                continue;
            }

            item_modifiers.Add(data.item);

            if (data.item.basic_attack)
            {
                basic_item_modifiers.Add(data.item);
            }
        }
        basic_attack_group.item_modifiers = basic_item_modifiers;
        RunDataChanged();
    }

}
