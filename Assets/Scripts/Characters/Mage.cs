using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : AbstractCharacter
{

    private void Start()
    {
        base.base_speed = 0.2f;
        base.Start();
    }

    protected override void SetActions()
    {
        _action_controller.UpdateDefaultAction(new NavMeshMoveAction(_navmeshagent), ActionType.Attack);
    }
}
