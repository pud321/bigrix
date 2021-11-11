using System.Collections.Generic;

public static class CharacterDefinitions
{
    private static Dictionary<CharacterEnums, CharacterFixedData> data;

    public static void Initialize()
    {
        data = new Dictionary<CharacterEnums, CharacterFixedData>();

        Add(
            new CharacterFixedData
            {
                type = CharacterEnums.Fighter,
                name = "Fighter",
                base_movement_speed = 0.5f,
                max_health = 100,
                basic_attack = new ActionData
                {
                    frequency = 6f,
                    damage = 10,
                    range = 1f,
                    action_type = ActionType.Attack,
                    damage_type = DamageType.Normal,
                }
            }
        );
        Add(
            new CharacterFixedData
            {
                type = CharacterEnums.Mage,
                name = "Mage",
                base_movement_speed = 0.2f,
                max_health = 50,
                basic_attack = new ActionData
                {
                    frequency = 6f,
                    damage = 20,
                    range = 1f,
                    action_type = ActionType.Attack,
                    damage_type = DamageType.Normal,
                }
            }
        );
        Add(
            new CharacterFixedData
            {
                type = CharacterEnums.Enemy,
                name = "Enemy",
                base_movement_speed = 0.2f,
                max_health = 75,
                basic_attack = new ActionData
                {
                    frequency = 6f,
                    damage = 15,
                    range = 1f,
                    action_type = ActionType.Attack,
                    damage_type = DamageType.Normal,
                }
            }
        );
    }

    private static void Add(CharacterFixedData fixed_data)
    {
        data.Add(fixed_data.type, fixed_data);
    }

    public static CharacterFixedData Get(CharacterEnums type)
    {
        CharacterFixedData class_in = data[type];
        return new CharacterFixedData
        {
            type = class_in.type,
            name = class_in.name,
            base_movement_speed = class_in.base_movement_speed,
            max_health = class_in.max_health,
            basic_attack = new ActionData
            {
                frequency = class_in.basic_attack.frequency,
                damage = class_in.basic_attack.damage,
                range = class_in.basic_attack.range,
                action_type = class_in.basic_attack.action_type,
                damage_type = class_in.basic_attack.damage_type,
            }
        };
    }
}
