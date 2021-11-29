using System.Collections.Generic;
using UnityEngine;

public class RangedAttackAction : AbstractAction, IAction
{
    public event BooleanAnimationEventHandler OnAnimationChangeRequest;


    private ITargeting _targeting;
    private DamageType damage_type;
    private IActionData data;
    private string animation_name = "Attack";
    private ProjectileController projectile_control;

    private CharacterManager current_target;

    public RangedAttackAction(Transform this_tranform, IActionData data, ProjectileController projectile_control)
    {
        this.data = data;
        this._range = data.range;
        this.frequency = data.frequency;
        //this._action_type = data.action_type;
        //this.damage_type = data.damage_type;
        _this_transform = this_tranform;

        if (projectile_control != null)
        {
            this.projectile_control = projectile_control;
            this.projectile_control.OnProjectileHit += ProjectileHit;
        }

    }

    public override Transform DesiredTarget()
    {
        return _targeting.GetCurrentTarget();
    }

    public override float RunAction()
    {
        Transform target_transform = DesiredTarget();

        if (target_transform != _this_transform)
        {
            CharacterManager desired_target = target_transform.GetComponent<CharacterManager>();

            if (projectile_control == null)
            {
                return 0f;
            }


            bool attempt_attack = projectile_control.RequestAttack(_this_transform, target_transform);

            if (attempt_attack)
            {
                current_target = desired_target;
                OnAnimationChangeRequest?.Invoke(animation_name);
                _next_action_time = Time.time + 1 / frequency;
                return 1f;
            }
        }

        return 0f;
    }

    public void ProjectileHit()
    {
        current_target.ChangeHealth(-data.damage, damage_type);
    }

    public override void StopAction()
    {

    }

    public override void SetTargets(List<CharacterManager> targets)
    {
        _targets = targets;
        _targeting = new NearestTarget(_this_transform, _targets, 1f);
    }
}
