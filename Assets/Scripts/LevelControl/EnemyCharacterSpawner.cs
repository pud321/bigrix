using System.Collections.Generic;
using UnityEngine.AI;

public class EnemyCharacterSpawner : AbstractCharacterSpawner<EnemyCharacterManager>
{
    public EnemyCharacterSpawner(SpawnerManager cs)
    {
        spawner_monobehavior = cs;
    }

    public void SpawnGroup()
    {
        List<EnemySpawnDefinition> level_spawn_data = LevelController.GetCurrentEnemySpawn();

        foreach (EnemySpawnDefinition spawn_definition in level_spawn_data)
        {
            RunCharacterSpawn(spawn_definition);
        }
    }

    private void RunCharacterSpawn(EnemySpawnDefinition details)
    {
        NavMeshHit hit;
        bool isHit = NavMesh.SamplePosition(details.spawn_position, out hit, spawn_tolerance, NavMesh.AllAreas);

        if (isHit)
        {
            EnemyCharacterManager current_spawn = InstantiateAddComponent(hit.position);
            current_spawn.SetupCharacterData(details.info);
            SaveAlertListeners(current_spawn);
        }

    }

    protected override void SetDeathResponse(EnemyCharacterManager character_manager)
    {
        character_manager.OnDeath += CleanupCharacter;
    }


}
