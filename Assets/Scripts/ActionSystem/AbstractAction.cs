using System.Collections.Generic;
using UnityEngine;


public abstract class AbstractAction
{
    public ActionType action_type { get { return _action_type; } }
    public float range { get { return _range; } }
    public float timeRemaining { get { return _next_action_time - Time.time; } }

    protected ActionType _action_type;
    protected Transform _this_transform;
    protected float _range;
    protected float frequency;
    protected float _next_action_time = 0f;

    protected List<CharacterManager> _targets;

    public abstract void SetTargets(List<CharacterManager> targets);

    public abstract float RunAction();
    public abstract void StopAction();
    public virtual bool CanRunAction()
    {
        return (IsActionTimerReady() && IsInRange() && IsFacingTarget());
    }

    public abstract Transform DesiredTarget();

    protected bool IsActionTimerReady()
    {
        return Time.time >= _next_action_time;
    }

    protected virtual bool IsInRange()
    {
        RaycastHit[] hits;
        Vector3 normalized_direction = (DesiredTarget().position - _this_transform.position).normalized;
        hits = Physics.RaycastAll(_this_transform.position, normalized_direction, range);

        Transform desired_target = DesiredTarget();

        foreach (RaycastHit hit in hits)
        {
            if (hit.transform == desired_target)
            {
                return true;
            }

        }
        return false;
    }

    protected bool IsFacingTarget()
    {
        Transform desired_target = DesiredTarget();
        _this_transform.LookAt(desired_target);
        return true;
    }

}
