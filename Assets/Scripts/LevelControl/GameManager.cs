using UnityEngine;


public class GameManager : MonoBehaviour
{
    [SerializeField] public LevelEnums level_name;

    private SpawnerManager spawner;
    private UIManager ui_manager;
    private ActiveCharacterInventory character_inventory;
    private SceneChanger scene_changer;
    private CharacterLoader character_loader;

    private void Awake()
    {
        spawner = GetComponent<SpawnerManager>();
        ui_manager = GetComponent<UIManager>();
        character_inventory = GetComponent<ActiveCharacterInventory>();
        character_loader = new CharacterLoader(character_inventory);
        scene_changer = GetComponent<SceneChanger>();

        GameStats.current_level = level_name;
        GameStats.Initialize();
        LevelController.Initialize();
        SkillFactory.Initialize(GetComponent<ActionPrefabGenerator>());

        spawner.AddSpawnListener(DelegatePlayerSpawn);
        spawner.AddSpawnListener(DelegateEnemySpawn);
    }

    private void Start()
    {
        GameStats.StartTime();
        character_loader.LoadSaved();

        spawner.SpawnAll(character_inventory.data);

        character_inventory.SetSkillTracker(spawner.GetPlayerManagers());

        spawner.ListenToCharacterDamage(DelegateDamageInformation);
        spawner.ListenToCharacterDeath(DelegateEnemyDeath);
        spawner.ListenToCharacterDeath(DelegatePlayerDeath);
    }

    public void DelegatePlayerSpawn(PlayerCharacterManager manager)
    {
        character_inventory.SetActiveContent(manager);
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

        sender.enemy_character_data.SetDropMoney();
        sender.enemy_character_data.SetItemDrop();

        if (GameStats.enemy_characters_count == 0)
        {
            character_loader.SaveAll();
            LevelController.UnlockCurrentLevels();
            GameStats.SaveAll();
        }
    }

    private void DelegateDeathCommon(CharacterManager sender)
    {
        GameStats.ChangeCharacterCount(sender, -1);
    }

    public void RunSceneChange()
    {
        character_loader.SaveAll();
        GameStats.SaveAll();
        MoneyData.ClearEvents();
        AllInventoryLookup.ClearEvents();

        scene_changer.ChangeToBase();
    }


}
