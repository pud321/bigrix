using System;

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


    public PlayerCharacterData(CharacterEnums type, uint level) : base(type)
    {
        experience = new XPData
        {
            level = level,
            xp = 0,
            next_xp = 1
        };
        experience.OnLevelUp += AdvanceLevelListener;
        inventory_controller = new ItemController(3);
        leveling_action_data = new ActionData
        {
            frequency = 0f,
            damage = 0,
            range = 0f,
            damage_type = fixed_data.basic_attack.damage_type,
            action_type = fixed_data.basic_attack.action_type
        };
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

    public PlayerCharacterData RemakeCharacter()
    {
        PlayerCharacterData remade_character = new PlayerCharacterData(fixed_data.type, 1);

        for (int i = 1; i < experience.level; i++)
        {
            remade_character.AdvanceLevelListener();
        }

        remade_character.experience.xp = experience.xp;
        remade_character.experience.level = experience.level;

        return remade_character;
    }
}
