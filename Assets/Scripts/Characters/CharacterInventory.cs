using System.Collections.Generic;
using UnityEngine;

public class CharacterInventory : MonoBehaviour
{
    [SerializeField] public GameObject character_inventory_ui;
    [SerializeField] public GameObject character_inventory_prefab;
    [SerializeField] public GameObject selected_character_details;

    public List<PlayerCharacterData> data;
    public List<ICharacterData> generic_data;

    protected List<GameObject> character_inventory_ui_list;

    protected void Awake()
    {
        data = new List<PlayerCharacterData>();
        generic_data = new List<ICharacterData>();
        character_inventory_ui_list = new List<GameObject>();
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
        SetPassiveContent(temp_data);
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

    public void SetActiveContent(PlayerCharacterManager current_character)
    {
        GameObject character_ui_item = CreateCharacterMenuItem();

        foreach (ICharacterTracker<CharacterManager> tracker in character_ui_item.GetComponents<ICharacterTracker<CharacterManager>>())
        {
            tracker.SetTracking(current_character);
        }

        foreach (ICharacterTracker<PlayerCharacterManager> tracker in character_ui_item.GetComponents<ICharacterTracker<PlayerCharacterManager>>())
        {
            tracker.SetTracking(current_character);
        }
    }

    public void SetPassiveContent(PlayerCharacterData data)
    {
        GameObject character_ui_item = CreateCharacterMenuItem();

        foreach (ICharacterDataTracker tracker in character_ui_item.GetComponents<ICharacterDataTracker>())
        {
            tracker.SetPlayerData(data);
        }
    }

    private GameObject CreateCharacterMenuItem()
    {
        GameObject character_ui_item = Instantiate(character_inventory_prefab, character_inventory_ui.transform);
        character_ui_item.GetComponent<ToggleGameobject>().toggle_object = selected_character_details;
        character_inventory_ui_list.Add(character_ui_item);
        return character_ui_item;
    }
}