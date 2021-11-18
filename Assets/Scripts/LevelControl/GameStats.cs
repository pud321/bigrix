using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public static class GameStats
{
    public static int save_slot_number = 1;
    public static LevelEnums current_level;

    public static float elapsed_time { get { return GetTime(); } }
    public static int player_characters_count;
    public static int enemy_characters_count;
    public static uint max_level = 20;

    public static int max_characters = 5;
    public static CharacterEnums[] player_character_types = new CharacterEnums[] {
        CharacterEnums.Fighter, 
        CharacterEnums.Mage,
        CharacterEnums.Priest,
    };

    // Experience system
    public static ExperienceSystem experience_system;
    public static readonly float scaling_factor = 1.5f;
    public static readonly float shift = 10f;

    // Time Variables
    private static long time_checkpoint;
    private static long _elapsed_time;
    private static bool isTimerRunning;


    private static PersistantGameData game_data;

    public static void Initialize()
    {
        _elapsed_time = 0;
        isTimerRunning = false;
        player_characters_count = 0;
        enemy_characters_count = 0;
        experience_system = new ExperienceSystem(scaling_factor, shift);
        game_data = new PersistantGameData();
        game_data.Load();
    }

    public static void SaveAll()
    {
        game_data.Save();
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
