using UnityEngine;

public interface ICharacterData
{
    CharacterFixedData fixed_data { get; }
    IAction GetBasicAttack(Transform this_transform);

    ActionDataGroup basic_attack_group { get; set;  }

    NavMeshMoveAction GetMovement(Transform this_transform);
    int max_health { get; }
    float movement_speed { get; }

    int base_damage { get; }

    CharacterEnums character_type { get; }

    string name { get; }

}
