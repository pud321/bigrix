using System;
using UnityEngine;
[Serializable]
public class PlayerCharacterData : CharacterCurrentData
{
    public XPData experience;
    public ItemController inventory_controller;
    private ActionData leveling_action_data;

    public uint level { get { return experience.level; } }

    public delegate void InventoryChangeHandler(IAction action, int slot);

    public event InventoryChangeHandler OnInventoryChange;
    public event ManagerEventSender<PlayerCharacterData> OnLevelChange;


    public PlayerCharacterData() : base()
    {
        experience = new XPData();
        experience.OnLevelUp += AdvanceLevelListener;
    }

    public void SetupPlayerAttackGroup()
    {
        SetupAttackGroup();
        leveling_action_data = new ActionData();
        leveling_action_data.damage_type = basic_attack_group.damage_type;
        leveling_action_data.action_type = basic_attack_group.action_type;
        basic_attack_group.AddAction(leveling_action_data);
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
            return fixed_data.max_health;
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
    }

}
