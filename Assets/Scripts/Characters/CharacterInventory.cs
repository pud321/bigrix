using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInventory : MonoBehaviour
{
    [SerializeField] public GameObject character_inventory_ui;
    [SerializeField] public GameObject character_inventory_prefab;

    public List<PlayerCharacterData> data;
    public List<ICharacterData> generic_data;

    protected void Awake()
    {
        data = new List<PlayerCharacterData>();
        generic_data = new List<ICharacterData>();
    }

    public void SetCharacterContent(PlayerCharacterManager current_character)
    {
        GameObject character_ui_item = Instantiate(character_inventory_prefab, character_inventory_ui.transform);

        foreach (ICharacterTracker<CharacterManager> tracker in character_ui_item.GetComponents<ICharacterTracker<CharacterManager>>())
        {
            tracker.SetTracking(current_character);
        }

        foreach (ICharacterTracker<PlayerCharacterManager> tracker in character_ui_item.GetComponents<ICharacterTracker<PlayerCharacterManager>>())
        {
            tracker.SetTracking(current_character);
        }
    }

    public bool LoadCharacter(int slot)
    {
        PlayerCharacterData reloaded_character = DataHandler.loadData<PlayerCharacterData>("pc_" + slot.ToString());
        bool reload_success = reloaded_character != null;
        if (reload_success)
        {
            PlayerCharacterData regenerated_character = SelectCharacterObject(reloaded_character.character_type);
            regenerated_character.RemakeCharacter(reloaded_character);
            TrackNew(regenerated_character);
        }

        return reload_success;
    }

    public void AddNew(CharacterEnums type, int level)
    {
        PlayerCharacterData temp_data = SelectCharacterObject(type);
        TrackNew(temp_data);
    }

    public void AddExperience(uint experience)
    {
        foreach (PlayerCharacterData c in data)
        {
            c.AddExperience(experience);
        }
    }

    private void TrackNew(PlayerCharacterData temp_data)
    {
        data.Add(temp_data);
        generic_data.Add(temp_data);
    }

    protected virtual PlayerCharacterData SelectCharacterObject(CharacterEnums type)
    {
        switch (type)
        {
            case CharacterEnums.Fighter:
                return new Fighter();
            case CharacterEnums.Mage:
                return new Mage(null);
            case CharacterEnums.Priest:
                return new Priest();
            default:
                return null;
        }
    }
}