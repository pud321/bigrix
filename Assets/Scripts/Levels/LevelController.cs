using System.Collections.Generic;
using UnityEngine;

public static class LevelController
{
    public static LevelEnums selected_level { get; set; }

    public static LevelEnums[] unlocked_levels;

    private static Dictionary<LevelEnums, LevelFixedData> data;

    public static void Initialize()
    {
        data = new Dictionary<LevelEnums, LevelFixedData>();

        if (unlocked_levels == null)
        {
            unlocked_levels = new LevelEnums[] { LevelEnums.Forest };
        }

        Add_Level_1();
    }

    private static void Add(LevelFixedData fixed_data)
    {
        data.Add(fixed_data.name, fixed_data);
    }

    public static void UnlockCurrentLevels()
    {
        LevelEnums[] new_levels = data[selected_level].unlocks_levels;
        UpdateUnlockedLevels(new_levels);
    }

    public static List<EnemySpawnDefinition> GetCurrentEnemySpawn()
    {
        return data[selected_level].enemy_spawn_data;
    }

    public static List<Vector3> GetRandomPlayerStarts()
    {
        return data[selected_level].GetPlayerRndStarts();
    }

    public static void UpdateUnlockedLevels(LevelEnums[] new_levels)
    {
        HashSet<LevelEnums> temp_levels = new HashSet<LevelEnums>(unlocked_levels);

        foreach (LevelEnums level in new_levels)
        {
            temp_levels.Add(level);
        }

        unlocked_levels = new LevelEnums[temp_levels.Count];
        temp_levels.CopyTo(unlocked_levels);
    }

    private static void Add_Level_1()
    {
        LevelFixedData temp_data = new LevelFixedData(LevelEnums.Forest);
        temp_data.AddStart(3.15f, 1f, -4.22f);
        temp_data.AddStart(0f, 1f, -1.49f);
        temp_data.AddStart(2.2f, 1f, -2.49f);
        temp_data.AddStart(1.5f, 1f, -3.49f);
        temp_data.AddEnemySpawn(CharacterEnums.Enemy, new Vector3(0f, 0.6f, 2f));
        temp_data.AddEnemySpawn(CharacterEnums.Enemy, new Vector3(1f, 0.6f, 3.3f));
        Add(temp_data);
    }


}
