using UnityEngine;

public delegate void LevelEnumEventHandler(LevelEnums level);

public class BaseManager : MonoBehaviour
{
    private CharacterInventory character_inventory;
    private CharacterLoader character_loader;
    private SelectNewCharacterUI character_menu_ui;
    private SceneChanger scene_changer;
    private BaseUIManager base_ui_manager;

    [SerializeField] private GameObject empty_slot_prefab;

    private GameObject empty_slot;

    private void Awake()
    {
        character_inventory = GetComponent<CharacterInventory>();
        character_menu_ui = GetComponent<SelectNewCharacterUI>();
        base_ui_manager = GetComponent<BaseUIManager>();
        scene_changer = GetComponent<SceneChanger>();

        character_loader = new CharacterLoader(character_inventory);

        GameStats.Initialize();
        BaseStats.Initialize();
    }

    private void Start()
    {
        base_ui_manager.CreateUnlockedLevelButtons();
        base_ui_manager.OnLevelEvent += RunSceneChange;

        character_menu_ui.OnCreateCharacter += AcceptCharacterUnlock;

        character_loader.LoadSaved();

        if (character_inventory.data.Count == 0 && MoneyData.total_money == 0)
        {
            MoneyData.total_money = 500;
        }

        foreach (PlayerCharacterData data in character_inventory.data)
        {
            character_inventory.SetPassiveContent(data);
        }

        CreateEmptySlot();
    }

    private void CreateEmptySlot()
    {
        if (character_inventory.data.Count < GameStats.max_characters)
        {
            empty_slot = Instantiate(empty_slot_prefab, character_inventory.character_inventory_ui.transform);
            EmptyCharacterUI empty_slot_ui = empty_slot.GetComponent<EmptyCharacterUI>();

            empty_slot_ui.OnCharacterAttemptUnlock += character_menu_ui.OpenCharacterMenu;

            int next_cost = BaseStats.GetNextCost(character_inventory.data.Count + 1);
            empty_slot_ui.SetCost(next_cost);
        }
    }

    private void AcceptCharacterUnlock(CharacterEnums type)
    {
        int next_cost = BaseStats.GetNextCost(character_inventory.data.Count + 1);
        MoneyData.ChangeMoney(-next_cost);

        Destroy(empty_slot);
        character_inventory.AddNew(type, 1);
        CreateEmptySlot();
    }

    private void RunSceneChange(LevelEnums level)
    {
        character_loader.SaveAll();
        GameStats.SaveAll();
        MoneyData.ClearEvents();
        AllInventoryLookup.ClearEvents();

        scene_changer.ChangeSceneToLevel(level);
    }


    public void RunSceneChange(int level)
    {
        RunSceneChange((LevelEnums)level);
    }
}
