using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public static class GameStats
{
    public static float elapsed_time { get { return GetTime(); } }
    public static int player_characters_count;
    public static int enemy_characters_count;
    public static int current_level;

    private static long time_checkpoint;
    private static long _elapsed_time;
    private static bool isTimerRunning;

    public static void Initialize()
    {
        _elapsed_time = 0;
        isTimerRunning = false;
        player_characters_count = 0;
        enemy_characters_count = 0;
        current_level = 1;
    }

    public static void StartTime()
    {
        if (!isTimerRunning)
        {
            time_checkpoint = DateTime.Now.Ticks;
            isTimerRunning = true;
        }
    }

    public static void ChangeCharacterCount(CharacterManager this_character, int amount)
    {
        if (this_character.is_enemy)
        {
            enemy_characters_count += amount;
        }
        else
        {
            player_characters_count += amount;
        }

    }

    public static void ChangeCharacterCount(EnemyCharacterManager this_character, int amount)
    {
        enemy_characters_count += amount;
    }
    
    public static void ChangeCharacterCount(PlayerCharacterManager this_character, int amount)
    {
        player_characters_count += amount;
    }

    public static void StopTime()
    {
        if (isTimerRunning)
        {
            _elapsed_time = DateTime.Now.Ticks - time_checkpoint;
            isTimerRunning = false;
        }
    }

    private static float GetTime()
    {
        long temp_output; 

        if (isTimerRunning)
        {
            temp_output = DateTime.Now.Ticks - time_checkpoint;
        }
        else
        {
            temp_output = _elapsed_time;
        }

        return (float)temp_output / 10000000f;
    }
}
