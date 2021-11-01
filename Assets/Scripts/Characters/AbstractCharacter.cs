using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class AbstractCharacter : MonoBehaviour
{

    protected NavMeshAgent _navmeshagent;
    protected ActionController _action_controller;

    public Transform _this_transform;
    
    protected float base_speed;
    protected float targeting_refresh_rate = 1f;

    private float _next_execution = 0f;

    protected void Awake()
    {
        _navmeshagent = GetComponent<NavMeshAgent>();
        _this_transform = this.transform;
    }

    protected void Start()
    {
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

        if (Time.time >= _next_execution)
        {
            float wait_time = _action_controller.NextAction();
            _next_execution = Time.time + wait_time;
        }
    }

    public void SetTargets(List<AbstractCharacter> allys, List<AbstractCharacter> enemies)
    {
        _action_controller = new ActionController(allys, enemies);
        SetActions();
    }

    protected abstract void SetActions();
}
