using System;
using UnityEngine;

public delegate void InventoryChangedEventHandler(ItemSlotData[] item_data_list);

[Serializable]
public class ItemInventoryController
{
    public int inventory_size;
    public int stack_capacity;
    public event InventoryChangedEventHandler OnInventoryUpdate;

    public ItemSlotData[] item_data_list;

    public ItemInventoryController(int inventory_size, int stack_capacity)
    {
        this.inventory_size = inventory_size;
        this.stack_capacity = stack_capacity;
        item_data_list = new ItemSlotData[inventory_size];
    }

    public int GetItemsAddable(ItemEnum type, int count)
    {
        return GetItemsAddable(type, count, null);
    }

    public int GetItemsAddable(ItemEnum type, int count, ItemSlotData ignore_data)
    {
        int free_capacity = 0;

        foreach (ItemSlotData data in item_data_list)
        {
            if (data == null || data == ignore_data)
            {
                free_capacity += stack_capacity;
            }
            else if (data.type == type)
            {
                free_capacity += (stack_capacity - data.count);
            }

            if (free_capacity >= count)
            {
                return count;
            }
        }

        return free_capacity;
    }

    public void UpdateInventory()
    {
        OnInventoryUpdate?.Invoke(item_data_list);
    }

    public void AddItem(ItemEnum type, int count)
    {
        int items_remaining = RecursiveAddItem(type, count);
        OnInventoryUpdate?.Invoke(item_data_list);
    }

    public void AddItemSlot(ItemEnum type, int count, int slot)
    {
        int items_remaining = RecursiveAddItem(type, count, slot);
        OnInventoryUpdate?.Invoke(item_data_list);
    }

    private int RecursiveAddItem(ItemEnum type, int count)
    {
        foreach (ItemSlotData data in item_data_list)
        {
            count = SpecificAddItem(type, count, data);
            if (count == 0) { return 0; }
        }

        ItemSlotData free_slot = GetFreeSlot(type);

        if (free_slot == null)
        {
            return count;
        }

        count = RecursiveAddItem(free_slot.type, count);

        return count;
    }

    private ItemSlotData GetFreeSlot(ItemEnum type)
    {
        for (int i = 0; i < inventory_size; i++)
        {
            if (item_data_list[i] == null)
            {
                item_data_list[i] = new ItemSlotData(type);
                return item_data_list[i];
            }
        }

        return null;
    }

    private int RecursiveAddItem(ItemEnum type, int count, int slot)
    {
        ItemSlotData data = item_data_list[slot];

        if (data == null)
        {
            item_data_list[slot] = new ItemSlotData(type);
        }

        int count_remaining = SpecificAddItem(type, count, data);

        if (count_remaining > 0)
        {
            count_remaining = RecursiveAddItem(type, count_remaining);
        }

        return count_remaining;
    }

    private int SpecificAddItem(ItemEnum type, int count, ItemSlotData data)
    {
        if (data == null)
        {
            return count;
        }

        if (data.type == type)
        {
            int counts_to_add = GetCountsToAdd(data.count, count);
            data.count += counts_to_add;
            count -= counts_to_add;
        }

        return count;
    }

    private int GetCountsToAdd(int slot_counts, int requested_counts)
    {
        int free_counts = stack_capacity - slot_counts;

        if (requested_counts >= free_counts)
        {
            return free_counts;
        }

        return requested_counts;
    }


    public void RemoveItem(ItemEnum type, int count)
    {
        foreach (ItemSlotData data in item_data_list)
        {
            if (data != null && data.type == type)
            {
                count = SpecificRemoveItem(data, count);
            }
        }

        OnInventoryUpdate?.Invoke(item_data_list);
    }

    public void RemoveEntireSlot(int slot)
    {
        ItemSlotData data = item_data_list[slot];

        if (data == null)
        {
            return;
        }

        int to_remove = data.count;
        item_data_list[slot] = null;

        OnInventoryUpdate?.Invoke(item_data_list);
    }

    private int SpecificRemoveItem(ItemSlotData data, int count)
    {
        int to_remove = GetCountsToRemove(data.count, count);
        data.count -= to_remove;

        if (data.count == 0)
        {
            data = null;
            return 0;
        }

        return data.count;
    }

    private int GetCountsToRemove(int slot_counts, int requested_counts)
    {
        if (requested_counts >= slot_counts)
        {
            return slot_counts;
        }

        return requested_counts;
    }

    public void RestoreInventoryObjects(ItemInventoryController temporary_inventory)
    {
        ItemSlotData data;

        for (int i = 0; i < temporary_inventory.inventory_size; i++)
        {
            data = temporary_inventory.item_data_list[i];
            if (data.count > 0)
            {
                item_data_list[i] = new ItemSlotData(data.type);
                item_data_list[i].count = data.count;
            }
        }
    }
}
