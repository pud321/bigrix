using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class CharacterManager : MonoBehaviour
{
    public Transform _this_transform;
    public int current_health;
    public abstract bool is_enemy { get; }

    public delegate void CharacterDamageHandler(CharacterManager sender, DamageEventArgs e);
    public event CharacterDamageHandler OnCharacterHealth;
    public event ManagerEventSender<CharacterManager> OnDeathGeneral;


    protected NavMeshAgent _navmeshagent;
    private ActionController _action_controller;
    private CharacterAnimationController _animation_controller;

    public ICharacterData character_data;
    protected bool hold_check_and_destroy;

    private float _next_execution = 0f;

    protected void Awake()
    {
        _navmeshagent = GetComponent<NavMeshAgent>();
        _animation_controller = GetComponent<CharacterAnimationController>();

        _this_transform = this.transform;
        hold_check_and_destroy = false;
    }

    public void SetupCharacterData(ICharacterData character_data)
    {
        this.character_data = character_data;
        current_health = character_data.max_health;
    }

    private void Update()
    {
        if (!hold_check_and_destroy)
        {
            CheckAndDestroyCharacter();
        }
        else
        {
            return;
        }

        if (!_navmeshagent)
        {
            return;
        }

        if (Time.time >= _next_execution)
        {
            _navmeshagent.speed = character_data.movement_speed;
            float wait_time = _action_controller.NextAction();
            _next_execution = Time.time + wait_time;
        }
    }

    public void ChangeHealth(int amount, DamageType damage_type)
    {
        current_health += amount;

        CheckAndDestroyCharacter();

        if (current_health > character_data.max_health)
        {
            current_health = character_data.max_health;
        }

        OnCharacterHealth?.Invoke(this, new DamageEventArgs(amount, damage_type));
    }

    protected abstract void CheckAndDestroyCharacter();


    protected void RunGeneralEvents()
    {
        _animation_controller.RunDiscreteAnimation("React");
        OnDeathGeneral?.Invoke(this);
    }

    public void SetTargets(List<CharacterManager> allys, List<CharacterManager> enemies)
    {
        _action_controller = new ActionController(allys, enemies, _animation_controller);
        SetActions();
    }

    protected virtual void SetActions()
    {
        _action_controller.AddMovementAction(character_data.GetMovement(_this_transform));
        _action_controller.AddBasicAction(character_data.GetBasicAttack(_this_transform));
    }

    public float health_percent 
    { 
        get 
        { 
            return (float)current_health / (float)character_data.max_health; 
        } 
    }

    public string name
    {
        get
        {
            return character_data.name;
        }
    }
}
