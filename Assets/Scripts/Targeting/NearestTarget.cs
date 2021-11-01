using System.Collections.Generic;
using UnityEngine;

public class NearestTarget : ITargeting
{

    private List<AbstractCharacter> _enemies;
    private Transform _self_transform;

    private float _expiration_period;
    private float _next_expriation;

    public Transform _currentTarget;

    public NearestTarget(Transform _self_transform, List<AbstractCharacter> _enemies) : this(_self_transform, _enemies, 0f) { }

    public NearestTarget(Transform _self_transform, List<AbstractCharacter> _enemies, float _expiration_period)
    {
        this._enemies = _enemies;
        this._self_transform = _self_transform;
        this._expiration_period = _expiration_period;
        this._next_expriation = -1;
    }


    public Transform GetCurrentTarget()
    {
        if (_IsExpired())
        {
            _SetCurrentTarget();
        }
        return _currentTarget;
    }

    private void _SetCurrentTarget()
    {
        float current_distance = float.MaxValue;
        float _distance;
        AbstractCharacter selected_target = null;

        foreach (AbstractCharacter ac in _enemies)
        {
            _distance = Vector3.Distance(_self_transform.position, ac._this_transform.position);

            if (_distance < current_distance)
            {
                current_distance = _distance;
                selected_target = ac;
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
