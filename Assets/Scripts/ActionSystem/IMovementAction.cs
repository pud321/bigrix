using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovementAction : IAction
{
    void UpdateRange(float updated_range);
}
