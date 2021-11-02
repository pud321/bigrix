using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackAction : AbstractAction
{
    private ITargeting _targeting;
    private int _damage;
    private DamageType _damage_type;
    
    public MeleeAttackAction(Transform this_tranform)
    {
        this._range = 2f;
        this._action_period = 6f;
        this._execution_time = 3f;
        this._action_type = ActionType.Attack;
        this._damage = -10;
        this._damage_type = DamageType.Normal;
        _this_transform = this_tranform;
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
            AbstractCharacter desired_target = target_transform.GetComponent<AbstractCharacter>();
            desired_target.ChangeHealth(_damage, _damage_type);
            _next_action_time = Time.time + _action_period;
        }
    }

    public override void StopAction()
    {
        
    }

    public override void SetTargets(List<AbstractCharacter> targets)
    {
        _targets = targets;
        _targeting = new NearestTarget(_this_transform, _targets, 1f);
    }

}
