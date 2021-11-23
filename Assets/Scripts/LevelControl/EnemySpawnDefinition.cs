using UnityEngine;

public struct EnemySpawnDefinition : ISpawnDefinition
{
    public EnemyCharacterData info { get; set; }
    public Vector3 spawn_position { get; set; }
    public bool is_enemy { get; set; }
}
