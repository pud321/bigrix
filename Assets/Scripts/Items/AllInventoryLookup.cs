using UnityEngine;
using System.Collections.Generic;

public delegate void PlayerChangeEventHandler();

public static class AllInventoryLookup
{
    public static ItemInventoryController all;
    public static ItemInventoryController player;
    public static event PlayerChangeEventHandler OnPlayerChange;


    public static void InitializeFromData(ItemInventoryController inventory)
    {
        all = new ItemInventoryController(GameStats.inventory_slots, GameStats.inventory_stack_size);
        all.RestoreInventoryObjects(inventory);
    }

    public static void InitializeAsDefault()
    {
        all = new ItemInventoryController(GameStats.inventory_slots, GameStats.inventory_stack_size);
        all.AddItem(ItemEnum.Dagger, 2);
        all.AddItem(ItemEnum.Wand, 1);
    }

    public static void SetPlayer(ItemInventoryController player)
    {
        AllInventoryLookup.player = player;
        OnPlayerChange?.Invoke();
    }

    public static void ClearEvents()
    {
        OnPlayerChange = null;
    }

    public static void SetCompatibiilty(Item item_data)
    {
        if (player != null)
        {
            player.SetItemCompatibility(item_data.characters);
        }
    }

    public static void ResetCompatibiilty()
    {
        if (player != null)
        {
            player.SetItemCompatibility(true);
        }
    }

    public static void IsCompatible(Item item_data)
    {

    }
}
