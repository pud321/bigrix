using System.Collections.Generic;
using UnityEngine;

public class NearestTarget : ITargeting
{

    private List<AbstractCharacter> _enemies;
    private Transform _self_transform;

    public Transform _currentTarget;

    public NearestTarget(Transform _self_transform, List<AbstractCharacter> _enemies)
    {
        this._enemies = _enemies;
        this._self_transform = _self_transform;
    }

    public Transform GetCurrentTarget(bool set_target)
    {
        if (set_target)
        {
            SetCurrentTarget();
        }
        return _currentTarget;
    }

    private void SetCurrentTarget()
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
}
