using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[System.Serializable]
public class CharacterCurrentData : ICharacterData
{
    public CharacterFixedData fixed_data { get { return _fixed_data; } }
    public CharacterFixedData _fixed_data;
    public ActionDataGroup basic_attack_group { get; set; }

    public CharacterCurrentData(CharacterEnums type)
    {
        _fixed_data = CharacterDefinitions.Get(type);
        basic_attack_group = new ActionDataGroup();
        basic_attack_group.AddAction(fixed_data.basic_attack);
    }

    public IAction GetBasicAttack(Transform this_transform)
    {
        return new BasicAttackAction(this_transform, basic_attack_group);
    }

    public NavMeshMoveAction GetMovement(Transform this_transform)
    {
        return new NavMeshMoveAction(this_transform.GetComponent<NavMeshAgent>());
    }

    public virtual int max_health
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

    public virtual int base_damage
    {
        get
        {
            return basic_attack_group.damage;
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
