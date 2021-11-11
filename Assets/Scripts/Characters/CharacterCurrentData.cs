using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterCurrentData : ICharacterData
{
    public CharacterFixedData fixed_data { get; set; }

    public CharacterCurrentData(CharacterEnums type)
    {
        fixed_data = CharacterDefinitions.Get(type);
    }

    public IAction GetBasicAttack(Transform this_transform)
    {
        return new BasicAttackAction(this_transform, fixed_data.basic_attack);
    }

    public NavMeshMoveAction GetMovement(Transform this_transform)
    {
        return new NavMeshMoveAction(this_transform.GetComponent<NavMeshAgent>());
    }

    public int max_health
    {
        get
        {
            return fixed_data.max_health;
        }
    }

    public CharacterEnums character_type
    {
        get
        {
            return fixed_data.type;
        }

    }

    public int base_damage
    {
        get
        {
            return fixed_data.basic_attack.damage;
        }
    }

    public float movement_speed
    {
        get
        {
            return fixed_data.base_movement_speed;
        }
    }

    public string name
    {
        get
        {
            return fixed_data.name;
        }
    }
}
