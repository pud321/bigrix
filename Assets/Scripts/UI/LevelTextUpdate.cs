using UnityEngine;
using UnityEngine.UI;

public class LevelTextUpdate : MonoBehaviour, ICharacterTracker<PlayerCharacterManager>
{
    [SerializeField] private Text level_text;

    public void SetTracking(PlayerCharacterManager tracked_character)
    {
        tracked_character.player_character_data.OnLevelChange += UpdateLevelText;
        UpdateLevelText(tracked_character.player_character_data);
    }

    private void UpdateLevelText(PlayerCharacterData tracked_character)
    {
        level_text.text = tracked_character.level.ToString();
    }
}
