using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private SpawnerManager spawner;
    private UIManager ui_manager;
    private CharacterInventory character_inventory;

    private Queue<GameObject> cleanup_queue;

    private void Awake()
    {
        cleanup_queue = new Queue<GameObject>();
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

        character_inventory.AddNew(CharacterEnums.Fighter, 1);
        character_inventory.AddNew(CharacterEnums.Fighter, 1);
        character_inventory.AddNew(CharacterEnums.Mage, 1);

        spawner.SpawnAll(character_inventory.data);

        spawner.ListenToCharacterDamage(DelegateDamageInformation);

        spawner.ListenToCharacterDeath(DelegateEnemyDeath);
        spawner.ListenToCharacterDeath(DelegatePlayerDeath);
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


        if (GameStats.enemy_characters_count == 0)
        {
            string currentScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentScene);
        }
    }

    private void DelegateDeathCommon(CharacterManager sender)
    {
        cleanup_queue.Enqueue(sender.gameObject);
        GameStats.ChangeCharacterCount(sender, -1);
    }

    private void Update()
    {
        foreach (GameObject g in cleanup_queue)
        {
            Destroy(g, 0.1f);
        }

        cleanup_queue.Clear();
    }
}
