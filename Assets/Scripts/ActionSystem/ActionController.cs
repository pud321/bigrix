using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActionController
{
    public NavMeshMoveAction movement_action;
    public IAction basic_action;
    public IAction[] skilled_action;

    private int _total_actions = 1;
    private int _max_queue = 5;

    private Queue<IAction> action_history;
    private IAction last_action;

    private List<CharacterManager> _ally_characters;
    private List<CharacterManager> _enemy_characters;
    private CharacterAnimationController animation_controller;

    public ActionController(List<CharacterManager> allys, List<CharacterManager> enemies, CharacterAnimationController animation_controller)
    {
        skilled_action = new IAction[_total_actions];
        action_history = new Queue<IAction>();

        _ally_characters = allys;
        _enemy_characters = enemies;
        this.animation_controller = animation_controller;
    }

    public void UpdateSkilledAction(IAction action, int slot)
    {
        skilled_action[slot] = action;
        movement_action.UpdateRange(GetMinimumRange());
        SetActionTargets(action);
    }

    public void AddBasicAction(IAction action)
    {
        basic_action = action;
        movement_action.UpdateRange(GetMinimumRange());
        SetActionTargets(action);
        basic_action.OnAnimationChangeRequest += AnimationChangeRequest;
    }

    public void AddMovementAction(NavMeshMoveAction action)
    {
        movement_action = action;
        movement_action.UpdateRange(GetMinimumRange());
        SetActionTargets(action);
    }

    public float NextAction()
    {
        if (last_action != null)
        {
            last_action.StopAction();
            last_action = null;
        }

        IAction next_action = FindNextAction();

        if (next_action == null)
        {
            return 0f;
        }

        if (!next_action.CanRunAction())
        {
            next_action = movement_action;
        }
        else
        {
            AddActionToQueue(next_action);
        }

        next_action.RunAction();
        last_action = next_action;
        return 1f;
    }

    private void AnimationChangeRequest(string name)
    {
        animation_controller.RunDiscreteAnimation(name);
    }

    private void AddActionToQueue(IAction action)
    {
        action_history.Enqueue(action);

        if (action_history.Count > _max_queue)
        {
            action_history.Dequeue();
        }
    }

    private IAction FindNextAction()
    {
        float temp_time_remaining;
        float best_time = basic_action.timeRemaining;
        IAction action_choice = basic_action;

        foreach (IAction action in skilled_action)
        {
            if (action == null || !action.CanRunAction())
            {
                continue;
            }

            temp_time_remaining = action.timeRemaining;

            if (temp_time_remaining < best_time)
            {
                best_time = temp_time_remaining;
                action_choice = action;
            }
        }
        return action_choice;
    }

    private void SetActionTargets(IAction action)
    {
        switch (action.action_type)
        {
            case ActionType.Attack: 
                action.SetTargets(_enemy_characters);
                break;
            default:
                break;
        }
    }

    private float GetMinimumRange()
    {
        float temp_minimum_range;

        if (basic_action == null)
        {
            return 0f;
        }

        float min_range = basic_action.range;

        foreach (IAction action in skilled_action)
        {
            if (action == null)
            {
                continue;
            }

            temp_minimum_range = action.range;

            if (temp_minimum_range < min_range)
            {
                min_range = temp_minimum_range;
            }
        }
        return min_range;
    }
}
