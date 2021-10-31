using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class AbstractCharacter : MonoBehaviour
{

    private NavMeshAgent _navmeshagent;
    protected ITargeting _targeting_system;
    public Transform _this_transform;
    
    protected float base_speed;

    private int search_rate = 30;
    private int _target_search;

    protected void Awake()
    {
        _navmeshagent = GetComponent<NavMeshAgent>();
        _this_transform = this.transform;
    }

    protected void Start()
    {
        _target_search = search_rate;
        if (_navmeshagent != null)
        {
            _navmeshagent.speed = base_speed;
        }
    }

    protected void Update()
    {
        if (!_navmeshagent)
        {
            return;
        }

        bool set_new_target = _target_search == search_rate;

        if (set_new_target)
        {
            _target_search = 0;
        }
        else
        {
            _target_search += 1;
        }

        _navmeshagent.destination = _targeting_system.GetCurrentTarget(set_new_target).position;
    }

    public void SetEnemies(List<AbstractCharacter> _enemies)
    {
        _targeting_system = new NearestTarget(this.transform, _enemies);
    }
}
