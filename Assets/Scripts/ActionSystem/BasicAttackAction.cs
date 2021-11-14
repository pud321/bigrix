using System.Collections.Generic;
using UnityEngine;


public class BasicAttackAction : AbstractAction, IAction
{

    public event BooleanAnimationEventHandler OnAnimationChangeRequest;

    private ITargeting _targeting;
    private DamageType damage_type;
    private IActionData data;
    private string animation_name = "Attack";

    public BasicAttackAction(Transform this_tranform, IActionData data)
    {
        this.data = data;
        this._range = data.range;
        this.frequency = data.frequency;
        //this._action_type = data.action_type;
        //this.damage_type = data.damage_type;
        _this_transform = this_tranform;
    }

    public override bool CanRunAction()
    {
        return (IsActionTimerReady() && IsInRange() && IsFacingTarget());
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
            OnAnimationChangeRequest?.Invoke(animation_name);
            CharacterManager desired_target = target_transform.GetComponent<CharacterManager>();
            desired_target.ChangeHealth(-data.damage, damage_type);
            _next_action_time = Time.time + 1/frequency;
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
