using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class AbstractCharacter : MonoBehaviour
{
    public Transform _this_transform;

    public delegate void CharacterEventHandler(AbstractCharacter sender);
    public event CharacterEventHandler OnCharacterDeath;

    protected NavMeshAgent _navmeshagent;
    protected ActionController _action_controller;
    
    protected float base_speed;
    protected int max_health = 20;
    protected int current_health;
    protected float targeting_refresh_rate = 1f;

    private float _next_execution = 0f;

    protected void Awake()
    {
        _navmeshagent = GetComponent<NavMeshAgent>();
        _this_transform = this.transform;
    }

    protected void Start()
    {
        current_health = max_health;

        if (_navmeshagent != null)
        {
            _navmeshagent.speed = base_speed;
        }
    }

    protected void Update()
    {
        CheckAndDestroyCharacter();

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

    private void CheckAndDestroyCharacter()
    {
        if (current_health <= 0)
        {
            OnCharacterDeath?.Invoke(this);
            Destroy(this.gameObject);
        }
    }

    public void ChangeHealth(int amount)
    {
        current_health += amount;
        
        if (current_health < 0)
        {
            current_health = 0;
        }

        if (current_health > max_health)
        {
            current_health = max_health;
        }

    }

    public void SetTargets(List<AbstractCharacter> allys, List<AbstractCharacter> enemies)
    {
        _action_controller = new ActionController(allys, enemies);
        SetActions();
    }

    protected abstract void SetActions();
}
