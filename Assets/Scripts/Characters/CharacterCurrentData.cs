using UnityEngine;
using UnityEngine.AI;


public delegate void DataChangeEventHandler();

[System.Serializable]
public abstract class CharacterCurrentData : ICharacterData
{
    public CharacterFixedData fixed_data { get { return _fixed_data; } }
    public CharacterFixedData _fixed_data;
    public ActionDataGroup basic_attack_group { get; set; }

    protected IActionData basic_attack_data;
    public event DataChangeEventHandler OnDataChanged;

    public CharacterCurrentData()
    {
        basic_attack_group = new ActionDataGroup();
    }

    public void SetupAttackGroup()
    {
        basic_attack_group.AddAction(basic_attack_data);
    }

    public virtual IAction GetBasicAttack(Transform this_transform)
    {
        return new BasicAttackAction(this_transform, basic_attack_group);
    }

    public virtual NavMeshMoveAction GetMovement(Transform this_transform)
    {
        return new NavMeshMoveAction(this_transform.GetComponent<NavMeshAgent>());
    }

    public void RunDataChanged()
    {
        OnDataChanged?.Invoke();
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
