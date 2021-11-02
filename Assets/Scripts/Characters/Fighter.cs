using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : AbstractCharacter
{
    private void Start()
    { 
        base.base_speed = 0.5f;
        base.Start();
    }

    protected override void SetActions()
    {
        _action_controller.UpdateDefaultAction(new NavMeshMoveAction(_navmeshagent), ActionType.Attack);
        _action_controller.UpdateItemAction(new MeleeAttackAction(_this_transform));
    }

}
