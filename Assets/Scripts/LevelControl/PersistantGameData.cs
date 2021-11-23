using System;

[Serializable]
public class PersistantGameData
{
    public int current_money;
    public LevelEnums[] levels_unlocked;
    public ItemInventoryController inventory;

    private string filename;

    public PersistantGameData()
    {
        filename = "pd_" + GameStats.save_slot_number.ToString();
    }

    public void Save()
    {
        RetreivePersistantData();
        DataHandler.saveData(this, filename);
    }

    public void Load()
    {
        PersistantGameData data = DataHandler.loadData<PersistantGameData>(filename);

        if (data != null)
        {
            data.RestorePersistantData();
        }
        else
        {
            SetDefaultPersistantData();
        }
    }

    private void RetreivePersistantData()
    {
        current_money = MoneyData.total_money;
        levels_unlocked = LevelController.unlocked_levels;
        inventory = AllInventoryLookup.all;
    }

    private void RestorePersistantData()
    {
        MoneyData.total_money = current_money;
        LevelController.unlocked_levels = levels_unlocked;
        AllInventoryLookup.InitializeFromData(inventory);
    }

    private void SetDefaultPersistantData()
    {
        MoneyData.total_money = 0;
        LevelController.unlocked_levels = new LevelEnums[] { LevelEnums.Forest };
        AllInventoryLookup.InitializeAsDefault();
    }
}
