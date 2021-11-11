using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacterManager : CharacterManager
{
    public EnemyCharacterData enemy_character_data;

    public event ManagerEventSender<EnemyCharacterManager> OnDeath;
    public new bool is_enemy = true;

    public void SetupCharacterData(EnemyCharacterData character_data)
    {
        enemy_character_data = character_data;
        base.SetupCharacterData(character_data);
    }

    public uint xp_value { get { return enemy_character_data.reward_experience; } }

    protected override void CheckAndDestroyCharacter()
    {
        if (current_health <= 0)
        {
            hold_check_and_destroy = true;
            OnDeath?.Invoke(this);
            RunGeneralEvents();
        }
    }
}
