using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryDisplay : AbstractInventoryDisplay
{
    private List<ItemInventorySingle> all_single_items;

    protected override void Awake()
    {
        base.Awake();
        all_single_items = new List<ItemInventorySingle>();
    }

    protected override void Start()
    {
        AllInventoryLookup.OnPlayerChange += PlayerChange;
    }

    public void SetPlayerData(PlayerCharacterData data)
    {
        AllInventoryLookup.SetPlayer(data.inventory_controller);
    }

    public void SetDragHighlight(bool canHighlight)
    {
        if (!canHighlight)
        {
            foreach (ItemInventorySingle item_box in all_single_items)
            {
                item_box.SetBoxIncompatible();
            }
        }
        else
        {
            foreach (ItemInventorySingle item_box in all_single_items)
            {
                item_box.SetBoxCompatible();
            }
        }
    }

    protected override void AddNewBox(int i)
    {
        GameObject g_container = CreateItemSlot();
        ItemInventorySingle item_single = CreateSingleItemBox(g_container, i);
        all_single_items.Add(item_single);
    }

    private void PlayerChange()
    {
        PlayerUnsubscribe();

        item_data = AllInventoryLookup.player;
        PlayerSubscribe();
        CreateBoxesForPlayer();

        for (int i = 0; i < item_data.inventory_size; i++)
        {
            all_single_items[i].controller = item_data;
        }
        item_data.UpdateInventory();
    }

    private void CreateBoxesForPlayer()
    {
        for (int i = instantiated_gameobjects.Count; i < item_data.inventory_size; i++)
        {
            AddNewBox(i);
        }

        for (int i = 0; i < item_data.inventory_size; i++)
        {
            instantiated_gameobjects[i].SetActive(true);
        }

        for (int i = item_data.inventory_size; i < instantiated_gameobjects.Count; i++)
        {
            instantiated_gameobjects[i].SetActive(false);
        }
    }


    private void PlayerSubscribe()
    {
        item_data.OnInventoryUpdate += UpdateAllDisplays;
        item_data.OnCharacterTypeCheck += SetDragHighlight;
        item_data.OnToolTipRequest += RequestToolTip;
    }

    private void PlayerUnsubscribe()
    {
        if (item_data != null)
        {
            item_data.OnInventoryUpdate -= UpdateAllDisplays;
            item_data.OnCharacterTypeCheck -= SetDragHighlight;
            item_data.OnToolTipRequest -= RequestToolTip;
        }
    }
}

