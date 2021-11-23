using System.Collections.Generic;
using UnityEngine;

public class LowestHealthTarget : ITargeting
{

    private List<CharacterManager> potential_targets;
    private Transform _self_transform;

    private float _expiration_period;
    private float _next_expriation;

    public Transform _currentTarget;

    public LowestHealthTarget(Transform _self_transform, List<CharacterManager> potential_targets) : this(_self_transform, potential_targets, 0f) { }

    public LowestHealthTarget(Transform _self_transform, List<CharacterManager> potential_targets, float _expiration_period)
    {
        this.potential_targets = potential_targets;
        this._self_transform = _self_transform;
        this._expiration_period = _expiration_period;
        this._next_expriation = -1;
    }


    public Transform GetCurrentTarget()
    {
        if (_currentTarget == null)
        {
            _next_expriation = Time.time;
        }

        if (_IsExpired())
        {
            _SetCurrentTarget();
        }

        return _currentTarget;
    }

    private void _SetCurrentTarget()
    {
        float selected_target_health = float.MaxValue;
        CharacterManager selected_target = null;

        foreach (CharacterManager target in potential_targets)
        {
            if (target.health_percent < selected_target_health)
            {
                selected_target_health = target.health_percent;
                selected_target = target;
            }
        }

        _currentTarget = selected_target == null ? _self_transform : selected_target._this_transform;
    }

    private bool _IsExpired()
    {
        bool isExpired = Time.time >= _next_expriation;

        if (isExpired)
        {
            _next_expriation = Time.time + _expiration_period;
        }

        return isExpired;
    }
}
