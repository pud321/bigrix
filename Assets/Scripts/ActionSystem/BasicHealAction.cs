using System.Collections.Generic;
using UnityEngine;

public class BasicHealAction : AbstractAction, IAction
{
    public event BooleanAnimationEventHandler OnAnimationChangeRequest;

    private ITargeting _targeting;
    private DamageType damage_type;
    private IActionData data;
    private string animation_name = "Attack";

    public BasicHealAction(Transform this_tranform, IActionData data)
    {
        this.data = data;
        this._range = data.range;
        this.frequency = data.frequency;
        this._action_type = data.action_type;
        this.damage_type = data.damage_type;
        _this_transform = this_tranform;
    }

    public override Transform DesiredTarget()
    {
        return _targeting.GetCurrentTarget();
    }

    public override void RunAction()
    {
        Transform target_transform = DesiredTarget();
        CharacterManager desired_target = target_transform.GetComponent<CharacterManager>();

        if (desired_target.health_percent < 1f)
        {
            OnAnimationChangeRequest?.Invoke(animation_name);
            desired_target.ChangeHealth(data.damage, damage_type);
            _next_action_time = Time.time + 1 / frequency;
        }
    }

    public override bool CanRunAction()
    {
        Transform target_transform = DesiredTarget();
        CharacterManager desired_target = target_transform.GetComponent<CharacterManager>();

        if (desired_target.health_percent >= 1f)
        {
            return false;
        }

        return base.CanRunAction();
    }

    protected override bool IsInRange()
    {
        if (DesiredTarget() == _this_transform)
        {
            return true;
        }

        return base.IsInRange();
    }

    public override void StopAction()
    {

    }

    public override void SetTargets(List<CharacterManager> targets)
    {
        _targets = targets;
        _targeting = new LowestHealthTarget(_this_transform, _targets, 1f);
    }
}
