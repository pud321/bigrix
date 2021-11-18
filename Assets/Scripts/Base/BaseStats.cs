using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BaseStats
{
    private static float new_character_cost_base = 10f;
    private static float new_character_cost_scaling = 1.5f;

    public static void Initialize()
    {

        }

    public static int GetNextCost(int next_character_slot)
    {
        var temp_cost = Mathf.Pow(new_character_cost_base * next_character_slot, new_character_cost_scaling);
        return Mathf.RoundToInt(temp_cost);
    }
}
