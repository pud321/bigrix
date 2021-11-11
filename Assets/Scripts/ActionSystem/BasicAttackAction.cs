using System.Collections.Generic;
using UnityEngine;

public class BasicAttackAction : AbstractAction, IAction
{
    private ITargeting _targeting;
    private int damage;
    private DamageType damage_type;


    public BasicAttackAction(Transform this_tranform, ActionData data)
    {
        this._range = data.range;
        this.frequency = data.frequency;
        this._action_type = data.action_type;
        this.damage_type = data.damage_type;
        _this_transform = this_tranform;
        this.damage = -data.damage;
    }

    public override bool CanRunAction()
    {
        return (IsActionTimerReady() && IsInRange());
    }

    public override Transform DesiredTarget()
    {
        return _targeting.GetCurrentTarget();
    }

    public override void RunAction()
    {
        Transform target_transform = DesiredTarget();

        if (target_transform != _this_transform)
        {
            CharacterManager desired_target = target_transform.GetComponent<CharacterManager>();
            int temp_damage = damage;
            desired_target.ChangeHealth(temp_damage, damage_type);
            _next_action_time = Time.time + frequency;
        }
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
