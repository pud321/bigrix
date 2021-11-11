using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInventory : MonoBehaviour
{
    [SerializeField] public GameObject character_inventory_ui;
    [SerializeField] public GameObject character_inventory_prefab;

    public List<PlayerCharacterData> data;
    public List<ICharacterData> generic_data;

    private void Awake()
    {
        data = new List<PlayerCharacterData>();
        generic_data = new List<ICharacterData>();
    }
    public void SetCharacterContent(PlayerCharacterManager current_character)
    {
        GameObject character_ui_item = Instantiate(character_inventory_prefab, character_inventory_ui.transform);

        foreach (ICharacterTracker tracker in character_ui_item.GetComponents<ICharacterTracker>())
        {
            tracker.SetTracking(current_character);
        }
    }

    public void AddNew(CharacterEnums type, int level)
    {
        PlayerCharacterData temp_data = new PlayerCharacterData(type, 1);
        data.Add(temp_data);
        generic_data.Add(temp_data);
    }

    public void AddExperience(uint experience)
    {
        foreach (PlayerCharacterData c in data)
        {
            c.AddExperience(experience);
        }
    }
}