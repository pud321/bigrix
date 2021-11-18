using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLoader
{
    private CharacterInventory character_inventory;

    public CharacterLoader(CharacterInventory character_inventory)
    {
        this.character_inventory = character_inventory;
    }

    public void LoadSaved()
    {
        bool successful_load;
        for (int i = 0; i < GameStats.max_characters; i++)
        {
            successful_load = character_inventory.LoadCharacter(i);
        }
    }

    public void LoadSavedDebugMode()
    {
        bool successful_load;
        for (int i = 0; i < GameStats.max_characters; i++)
        {
            successful_load = character_inventory.LoadCharacter(i);

            if (!successful_load)
            {
                CreateCharacter(i);
            }
        }
    }

    public void SaveAll()
    {
        for (int i = 0; i < character_inventory.data.Count; i++)
        {
            DataHandler.saveData(character_inventory.data[i], "pc_" + i.ToString());
        }
    }

    private void CreateCharacter(int slot)
    {
        switch (slot)
        {
            case 0:
                character_inventory.AddNew(CharacterEnums.Fighter, 1);
                break;
            case 1:
                character_inventory.AddNew(CharacterEnums.Fighter, 1);
                break;
            case 2:
                character_inventory.AddNew(CharacterEnums.Mage, 1);
                break;
            default:
                break;

        }
    }

    private void CreateEmpty()
    {

    }
}
