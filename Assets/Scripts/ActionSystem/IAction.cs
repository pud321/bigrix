using System.Collections.Generic;
using UnityEngine;

public delegate void BooleanAnimationEventHandler(string s);


public interface IAction
{
    float RunAction();
    void StopAction();
    bool CanRunAction();
    void SetTargets(List<CharacterManager> targets);
    Transform DesiredTarget();
    float range { get; }
    float timeRemaining { get; }
    ActionType action_type { get; }

    event BooleanAnimationEventHandler OnAnimationChangeRequest;

}
