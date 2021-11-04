using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : AbstractCharacter
{
    public override CharacterEnums character_type { get { return CharacterEnums.Fighter; } }

    private void Awake()
    { 
        base_speed = 0.5f;
        base.Awake();
    }

    protected override void SetActions()
    {
        _action_controller.UpdateDefaultAction(new NavMeshMoveAction(_navmeshagent, 2f), ActionType.Attack);
        _action_controller.UpdateItemAction(new MeleeAttackAction(_this_transform));
    }

}
