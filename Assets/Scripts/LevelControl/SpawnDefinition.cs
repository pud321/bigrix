using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SpawnDefinition
{
    public CharacterEnums spawn_name { get; }
    public Vector3 spawn_position { get; }
    public bool is_enemy { get; }

    public SpawnDefinition(CharacterEnums spawn_name, Vector3 spawn_position, bool is_enemy)
    {
        this.spawn_name = spawn_name;
        this.spawn_position = spawn_position;
        this.is_enemy = is_enemy;
    }

}
