using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : AbstractCharacter
{
    public override CharacterEnums character_type { get { return CharacterEnums.Mage; } }

    private void Awake()
    {
        base_speed = 0.2f;
        base.Awake();
    }

    protected override void SetActions()
    {
        _action_controller.UpdateDefaultAction(new NavMeshMoveAction(_navmeshagent), ActionType.Attack);
        _action_controller.UpdateItemAction(new MeleeAttackAction(_this_transform));
    }

}
