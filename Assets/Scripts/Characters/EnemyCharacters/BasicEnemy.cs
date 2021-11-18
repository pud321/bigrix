using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemy : EnemyCharacterData
{

    public BasicEnemy() : base(100, new DropGenerator(100, 200)) 
    {
        _fixed_data = new CharacterFixedData
        {
            type = CharacterEnums.Enemy,
            name = "Enemy",
            base_movement_speed = 0.2f,
            max_health = 75,
        };

        basic_attack_data = new ActionData
        {
            frequency = 0.17f,
            damage = 15,
            range = 1f,
            action_type = ActionType.Attack,
            damage_type = DamageType.Normal,
        };

        SetupAttackGroup();
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
