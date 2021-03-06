using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelFixedData
{
    public LevelEnums name;
    public List<Vector3> level_player_starts;
    public List<EnemySpawnDefinition> enemy_spawn_data;
    public LevelEnums[] unlocks_levels = new LevelEnums[] { LevelEnums.Plains };

    public List<ICharacterData> generic_enemy_data;
    public List<EnemyCharacterData> enemy;

    public LevelFixedData(LevelEnums name)
    {
        this.name = name;
        this.level_player_starts = new List<Vector3>();
        enemy_spawn_data = new List<EnemySpawnDefinition>();
        generic_enemy_data = new List<ICharacterData>();
        enemy = new List<EnemyCharacterData>();
    }

    public void AddStart(float x, float y, float z)
    {
        if (level_player_starts == null)
        {
            level_player_starts = new List<Vector3>();
        }
        level_player_starts.Add(new Vector3(x, y, z));
    }

    public void AddEnemySpawn(CharacterEnums type, Vector3 start_position)
    {
        if (enemy_spawn_data == null)
        {
            CreateEmptyLists();
        }

        EnemyCharacterData temp_enemy_data = new BasicEnemy();
        generic_enemy_data.Add(temp_enemy_data);
        enemy.Add(temp_enemy_data);

        enemy_spawn_data.Add(new EnemySpawnDefinition
        {
            info = temp_enemy_data,
            spawn_position = start_position,
            is_enemy = true
        });
    }

    private void CreateEmptyLists()
    {
        enemy_spawn_data = new List<EnemySpawnDefinition>();
        generic_enemy_data = new List<ICharacterData>();
        enemy = new List<EnemyCharacterData>();
    }
    public List<Vector3> GetPlayerRndStarts()
    {
        var rnd = new System.Random();
        return level_player_starts.OrderBy(item => rnd.Next()).ToList();
    }

}
