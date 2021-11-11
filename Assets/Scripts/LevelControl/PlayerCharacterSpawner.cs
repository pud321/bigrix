using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerCharacterSpawner : AbstractCharacterSpawner<PlayerCharacterManager>
{
    public PlayerCharacterSpawner(SpawnerManager cs)
    {
        spawner_monobehavior = cs;
    }

    public void SpawnGroup(List<PlayerCharacterData> player_array)
    {
        List<Vector3> level_starts = LevelDefinition.data[GameStats.current_level].GetPlayerRndStarts();

        for (int i = 0; i < player_array.Count; i++)
        {
            SpawnSingleCharacter(player_array[i], level_starts[i]);
        }
    }


    private void SpawnSingleCharacter(PlayerCharacterData player_data, Vector3 spawn_position)
    {
        if (player_data == null)
        {
            return;
        }

        PlayerSpawnDefinition temp_spawn_definition = new PlayerSpawnDefinition
        {
            info = player_data,
            spawn_position = spawn_position,
            is_enemy = false
        };

        PlayerCharacterManager current_spawn = RunCharacterSpawn(temp_spawn_definition);

        if (current_spawn != null)
        {
            SaveAlertListeners(current_spawn);
        }
    }

    private PlayerCharacterManager RunCharacterSpawn(PlayerSpawnDefinition details)
    {
        NavMeshHit hit;
        bool isHit = NavMesh.SamplePosition(details.spawn_position, out hit, spawn_tolerance, NavMesh.AllAreas);

        if (isHit)
        {
            PlayerCharacterManager current_spawn = InstantiateAddComponent(hit.position);
            current_spawn.SetupCharacterData(details.info);
            return current_spawn;
        }

        return null;
    }

    protected override void SetDeathResponse(PlayerCharacterManager character_manager)
    {
        character_manager.OnDeath += CleanupCharacter;
    }
}
