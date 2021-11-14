using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    private SpawnerManager spawner;
    private UIManager ui_manager;
    private CharacterInventory character_inventory;

    private void Awake()
    {
        spawner = GetComponent<SpawnerManager>();
        ui_manager = GetComponent<UIManager>();
        character_inventory = GetComponent<CharacterInventory>();

        GameStats.Initialize();
        CharacterDefinitions.Initialize();
        LevelDefinition.Initialize();

        spawner.AddSpawnListener(DelegatePlayerSpawn);
        spawner.AddSpawnListener(DelegateEnemySpawn);

    }

    private void Start()
    {
        GameStats.StartTime();

        bool successful_load;
        for (int i = 0; i < 3; i++)
        {
            successful_load = character_inventory.LoadCharacter(i);
            
            if (!successful_load)
            {
                CreateCharacter(i);
            }
        }

        spawner.SpawnAll(character_inventory.data);

        spawner.ListenToCharacterDamage(DelegateDamageInformation);
        spawner.ListenToCharacterDeath(DelegateEnemyDeath);
        spawner.ListenToCharacterDeath(DelegatePlayerDeath);
    }

    private void CreateCharacter(int slot)
    {
        switch (slot)
        {
            case 1:
                character_inventory.AddNew(CharacterEnums.Fighter, 1);
                break;
            case 2:
                character_inventory.AddNew(CharacterEnums.Fighter, 1);
                break;
            case 3:
                character_inventory.AddNew(CharacterEnums.Mage, 1);
                break;
            default:
                break;

        }
    }

    public void DelegatePlayerSpawn(PlayerCharacterManager manager)
    {
        character_inventory.SetCharacterContent(manager);
        GameStats.player_characters_count += 1;
    }

    public void DelegateEnemySpawn(EnemyCharacterManager manager)
    {
        ui_manager.UpdateScoreText();
        GameStats.enemy_characters_count += 1;
        ui_manager.UpdateScoreText();
    }

    private void DelegateDamageInformation(CharacterManager sender, DamageEventArgs e)
    {
    }

    private void DelegatePlayerDeath(PlayerCharacterManager sender)
    {
        DelegateDeathCommon(sender);
    }

    private void DelegateEnemyDeath(EnemyCharacterManager sender)
    {
        DelegateDeathCommon(sender);
        ui_manager.UpdateScoreText();

        character_inventory.AddExperience(sender.xp_value);
        
        PlayerCharacterData temp;

        if (GameStats.enemy_characters_count == 0)
        {
            for (int i = 0; i < character_inventory.data.Count; i++)
            {
                DataHandler.saveData(character_inventory.data[i], "pc_" + i.ToString());
            }
        }
    }

    private void DelegateDeathCommon(CharacterManager sender)
    {
        GameStats.ChangeCharacterCount(sender, -1);
    }


}
