using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawnDefinition
{
    public Vector3 spawn_position { get; set; }
    public bool is_enemy { get; set; }
}
