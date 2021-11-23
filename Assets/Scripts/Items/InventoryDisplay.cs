public class InventoryDisplay : AbstractInventoryDisplay
{
    protected override void Awake()
    {
        base.Awake();
        item_data = AllInventoryLookup.all;
    }

    protected override void Start()
    {
        base.Start();
        item_data.UpdateInventory();
    }

    public void UpdateItem(ItemInventorySingle single_item)
    {
        UpdateItem(single_item.this_item, 0, single_item.count, -1);
    }

    public void UpdateItem(Item item_to_update, int counts, int counts_change, int slot)
    {
        bool box_updated = false;

        if (slot >= 0)
        {
            all_current_boxes[slot].SetNameCount(item_to_update, counts);
            return;
        }

        foreach (ItemSlot box in all_current_boxes)
        {
            if (!box.isEmpty && box.BoxHoldingType() == item_to_update.type)
            {
                box.SetNameCount(item_to_update, counts);
                box_updated = true;
                break;
            }
        }

        if (counts > 0 && !box_updated)
        {
            bool box_filled = false;
            foreach (ItemSlot box in all_current_boxes)
            {
                if (box.isEmpty)
                {
                    box.SetNameCount(item_to_update, counts);
                    box_filled = true;
                    break;
                }
            }
        }
    }
}
