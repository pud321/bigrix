using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshMoveAction : AbstractAction, IMovementAction
{
    private NavMeshAgent _navmeshagent;
    private ITargeting _targeting;
    private float _retreat_wiggle;

    public event BooleanAnimationEventHandler OnAnimationChangeRequest;

    public NavMeshMoveAction(NavMeshAgent _navmeshagent)
    {
        this._navmeshagent = _navmeshagent;
        _this_transform = _navmeshagent.transform;
        this.frequency = 0f;
        this._action_type = ActionType.Attack;
        this._retreat_wiggle = 0.5f * _range;
    }

    public override bool CanRunAction()
    {
        return true;
    }

    public override Transform DesiredTarget()
    {
        return _targeting.GetCurrentTarget();
    }

    public override float RunAction()
    {
        Transform desired_target = DesiredTarget();
        float target_distance = Vector3.Distance(_this_transform.position, desired_target.position);

        if (_range == 0f || target_distance > _range)
        {
            _navmeshagent.destination = desired_target.position;
        }
        else if (1.5 * target_distance < _range)
        {
            _navmeshagent.destination = _SetRetreatPosition(desired_target.position);
        }
        else
        {
            StopAction();
        }

        return 1f;
    }

    private Vector3 _SetRetreatPosition(Vector3 target_position)
    {
        NavMeshHit hit;
        Vector3 new_position;
        bool isHit;

        new_position = 2 * _this_transform.position - target_position;
        isHit = NavMesh.SamplePosition(new_position, out hit, _retreat_wiggle, NavMesh.AllAreas);
        if (isHit)
        {
            return hit.position;
        }


        return _this_transform.position;
    }
    public override void StopAction()
    {
        if (_navmeshagent.isOnNavMesh)
        {
            _navmeshagent.SetDestination(_this_transform.position);
        }
    }

    public override void SetTargets(List<CharacterManager> targets)
    {
        _targets = targets;
        _targeting = new NearestTarget(_this_transform, _targets, 1f);
    }

    public void UpdateRange(float updated_range)
    {
        _range = updated_range;
    }
}
