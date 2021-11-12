using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterManager : CharacterManager
{
    private PlayerCharacterData player_character_data;

    public event ManagerEventSender<PlayerCharacterManager> OnDeath;

    public override bool is_enemy { get { return false; } }

    public void SetupCharacterData(PlayerCharacterData character_data)
    {
        player_character_data = character_data;
        base.SetupCharacterData(character_data);
    }

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
