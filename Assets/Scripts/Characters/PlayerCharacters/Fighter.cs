using UnityEngine;
using UnityEngine.AI;

public class Fighter : PlayerCharacterData
{
    public Fighter() : base()
    {
        _fixed_data = new CharacterFixedData
        {
            type = CharacterEnums.Fighter,
            name = "Fighter",
            base_movement_speed = 0.5f,
            max_health = 100,
            levelup_damage = 5,
            levelup_maxhealth = 20,
            levelup_movement = 0.1f
        };

        basic_attack_data = new ActionData
        {
            frequency = 0.17f,
            damage = 10,
            range = 1f,
            action_type = ActionType.Attack,
            damage_type = DamageType.Normal,
        };

        SetupPlayerAttackGroup();
        SetupInventory();
    }

    public override IAction GetBasicAttack(Transform this_transform)
    {
        return new BasicAttackAction(this_transform, basic_attack_group);
    }

    public override NavMeshMoveAction GetMovement(Transform this_transform)
    {
        return new NavMeshMoveAction(this_transform.GetComponent<NavMeshAgent>());
    }
}
