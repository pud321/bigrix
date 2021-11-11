using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlayerSpawnDefinition : ISpawnDefinition
{
    public PlayerCharacterData info { get; set; }
    public Vector3 spawn_position { get; set; }
    public bool is_enemy { get; set; }
}
