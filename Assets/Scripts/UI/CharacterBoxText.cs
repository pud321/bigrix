using UnityEngine;
using UnityEngine.UI;

public class CharacterBoxText : MonoBehaviour, ICharacterTracker
{
    [SerializeField] public Text name_display;
    [SerializeField] public Text damage_field;

    public void SetTracking(CharacterManager tracked_character)
    {
        name_display.text = tracked_character.name;

        tracked_character.OnCharacterHealth += UpdateCharacterHealth;
        tracked_character.OnDeathGeneral += ShowCharacterDeath;
    }
    private void UpdateCharacterHealth(CharacterManager sender, DamageEventArgs e)
    {
        //damage_field.text = "Damage:" + e.damage_value.ToString();
    }

    private void ShowCharacterDeath(CharacterManager sender)
    {
        //damage_field.text = "RIP";
    }
}
