using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractAction : IAction
{
    public ActionType action_type { get { return _action_type; } }
    public float range { get { return _range; } }
    public float timeRemaining { get { return _next_action_time - Time.time; } }
    public float execution_time { get { return _execution_time; } }

    protected ActionType _action_type;
    protected Transform _this_transform;
    protected float _range;
    protected float _action_period;
    protected float _next_action_time = 0f;
    protected float _execution_time = 0f;

    protected List<AbstractCharacter> _targets;

    public abstract void SetTargets(List<AbstractCharacter> targets);

    public abstract void RunAction();
    public abstract void StopAction();
    public abstract bool CanRunAction();
    public abstract Transform DesiredTarget();

    protected bool IsActionTimerReady()
    {
        return Time.time >= _next_action_time;
    }

    protected bool IsInRange()
    {
        return _range >= Vector3.Distance(DesiredTarget().position, _this_transform.position);
    }
}
