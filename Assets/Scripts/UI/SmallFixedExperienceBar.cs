using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallFixedExperienceBar : GeneralBar, ICharacterDataTracker
{
    private PlayerCharacterData player_data;

    protected override void SetDefaultColors()
    {
        high_color = new Color(0.25f, 0.88f, 0.80f, 1f);
        low_color = new Color(0.25f, 0.88f, 0.80f, 1f);
    }

    protected override void SetText()
    {
        bar_text.text = "XP: " + player_data.experience.xp.ToString() + "/" + player_data.experience.NextXP().ToString();
    }

    public void SetPlayerData(PlayerCharacterData tracked_character)
    {
        player_data = tracked_character;
        SetBar(tracked_character.experience.LevelFraction());
    }
}
