using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelDefinition
{
    public static Dictionary<int, LevelFixedData> data;

    public static void Initialize()
    {
        data = new Dictionary<int, LevelFixedData>();

        LevelFixedData temp_data = new LevelFixedData
        {
            name = "Forest",
            number = 1,
        };

        temp_data.AddStart(3.15f, 1f, -4.22f);
        temp_data.AddStart(0f, 1f, -1.49f);
        temp_data.AddStart(2.2f, 1f, -2.49f);
        temp_data.AddEnemySpawn(CharacterEnums.Enemy, new Vector3(0f, 0.6f, 2f), 20);
        temp_data.AddEnemySpawn(CharacterEnums.Enemy, new Vector3(1f, 0.6f, 3.3f), 30);
        Add(temp_data);

    }

    private static void Add(LevelFixedData fixed_data)
    {
        data.Add(fixed_data.number, fixed_data);
    }
}
