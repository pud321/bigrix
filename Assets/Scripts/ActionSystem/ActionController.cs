using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ActionController
{
    public Dictionary<ActionType, IAction> default_action;
    public IAction item_action;
    public IAction[] skilled_action;

    private int _total_actions = 1;
    private int _max_queue = 5;

    private Queue<IAction> action_history;
    private IAction last_action;

    private List<AbstractCharacter> _ally_characters;
    private List<AbstractCharacter> _enemy_characters;


    public ActionController(List<AbstractCharacter> allys, List<AbstractCharacter> enemies)
    {
        skilled_action = new IAction[_total_actions];
        default_action = new Dictionary<ActionType, IAction>();
        action_history = new Queue<IAction>();
        _ally_characters = allys;
        _enemy_characters = enemies;
    }

    public void UpdateSkilledAction(IAction action, int slot)
    {
        skilled_action[slot] = action;
        _SetActionTargets(action);
    }

    public void UpdateItemAction(IAction action)
    {
        item_action = action;
        _SetActionTargets(action);
    }

    public void UpdateDefaultAction(IAction action, ActionType action_type)
    {
        default_action[action_type] = action;
        _SetActionTargets(action);
    }

    public float NextAction()
    {
        if (last_action != null)
        {
            last_action.StopAction();
            last_action = null;
        }

        IAction next_action = _FindNextAction();

        if (next_action == null)
        {
            return 0f;
        }

        if (!next_action.CanRunAction())
        {
            next_action = default_action[next_action.action_type];
        }
        else
        {
            _AddActionToQueue(next_action);
        }

        next_action.RunAction();
        last_action = next_action;
        return next_action.execution_time;
    }

    private void _AddActionToQueue(IAction action)
    {
        action_history.Enqueue(action);

        if (action_history.Count > _max_queue)
        {
            action_history.Dequeue();
        }
    }

    private IAction _FindNextAction()
    {
        return item_action;
    }

    private void _SetActionTargets(IAction action)
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
}
