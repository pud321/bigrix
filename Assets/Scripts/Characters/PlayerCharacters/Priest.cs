using UnityEngine;
using UnityEngine.AI;

public class Priest : PlayerCharacterData
{
    protected override int max_inventory_slots { get { return 2; } }

    public Priest() : base()
    {
        _fixed_data = new CharacterFixedData
        {
            type = CharacterEnums.Priest,
            name = "Priest",
            base_movement_speed = 1f,
            max_health = 50,
            levelup_damage = 10,
            levelup_maxhealth = 10
        };

        basic_attack_data = new ActionData
        {
            frequency = 0.17f,
            damage = 20,
            range = 2.5f,
            action_type = ActionType.Heal,
            damage_type = DamageType.Normal,
        };

        SetupPlayerAttackGroup();
    }

    public override IAction GetBasicAttack(Transform this_transform)
    {
        return new BasicHealAction(this_transform, basic_attack_group);
    }

    public override NavMeshMoveAction GetMovement(Transform this_transform)
    {
        return new NavMeshMoveAction(this_transform.GetComponent<NavMeshAgent>());
    }
}