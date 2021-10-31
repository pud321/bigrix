using System.Collections.Generic;
using UnityEngine;

public class NearestTarget : ITargeting
{

    private List<AbstractEnemy> _enemies;
    private Transform _self_transform;

    public Transform _currentTarget;

    public NearestTarget(Transform _self_transform, List<AbstractEnemy> _enemies)
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
        AbstractEnemy selected_enemy = null;

        foreach (AbstractEnemy ae in _enemies)
        {
            _distance = Vector3.Distance(_self_transform.position, ae.enemy_transform.position);

            if (_distance < current_distance)
            {
                current_distance = _distance;
                selected_enemy = ae;
            }
        }

        _currentTarget = selected_enemy == null ? _self_transform : selected_enemy.enemy_transform;
    }
}
