using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshMoveAction : AbstractAction
{
    private NavMeshAgent _navmeshagent;
    private ITargeting _targeting;

    public NavMeshMoveAction(NavMeshAgent _navmeshagent)
    {
        this._navmeshagent = _navmeshagent;
        _this_transform = _navmeshagent.transform;
        this._range = 0f;
        this._action_period = 0f;
        this._execution_time = 0.5f;
        this._action_type = ActionType.Attack;
    }

    public override bool CanRunAction()
    {
        return true;
    }

    public override Transform DesiredTarget()
    {
        return _targeting.GetCurrentTarget();
    }

    public override void RunAction()
    {
        Transform desired_target = DesiredTarget();
        _navmeshagent.destination = desired_target.position;
    }

    public override void StopAction()
    {
        _navmeshagent.SetDestination(_this_transform.position);
    }
    public override void SetTargets(List<AbstractCharacter> targets)
    {
        _targets = targets;
        _targeting = new NearestTarget(_this_transform, _targets, 1f);
    }
}
