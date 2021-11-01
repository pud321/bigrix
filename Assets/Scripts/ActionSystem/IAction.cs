using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAction
{
    void RunAction();
    void StopAction();
    bool CanRunAction();
    void SetTargets(List<AbstractCharacter> targets);
    Transform DesiredTarget();
    float range { get; }
    float timeRemaining { get; }

    float execution_time { get; }
    ActionType action_type { get; }
}
