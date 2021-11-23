using UnityEngine;
using UnityEngine.AI;

public class Mage : PlayerCharacterData
{
    private ProjectileController projectile_controller;
    protected override int max_inventory_slots { get { return 3; } }

    public Mage(ProjectileController projectile_controller) : base()
    {
        if (projectile_controller != null)
        {
            this.projectile_controller = projectile_controller;
            this.projectile_controller.Set_YOffSet(0.5f);
        }

        _fixed_data = new CharacterFixedData
        {
            type = CharacterEnums.Mage,
            name = "Mage",
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
            action_type = ActionType.Attack,
            damage_type = DamageType.Normal,
        };

        SetupPlayerAttackGroup();
    }

    public override IAction GetBasicAttack(Transform this_transform)
    {
        return new RangedAttackAction(this_transform, basic_attack_group, projectile_controller);
    }

    public override NavMeshMoveAction GetMovement(Transform this_transform)
    {
        return new NavMeshMoveAction(this_transform.GetComponent<NavMeshAgent>());
    }
}
