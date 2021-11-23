using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    private ItemInventorySingle box_content;

    public bool isEmpty { get { return box_content.isEmpty; } }

    public void Start()
    {
        box_content = GetComponentInChildren<ItemInventorySingle>();
    }

    public void SetNameCount(Item this_item, int count)
    {
        if (box_content == null)
        {
            box_content = GetComponentInChildren<ItemInventorySingle>();
        }

        if (box_content != null)
        {
            box_content.SetNameCount(this_item, count);
        }
    }

    public void Clear()
    {
        if (box_content != null)
        {
            box_content.Clear();
        }
    }

    public ItemEnum BoxHoldingType()
    {
        return box_content.this_item.type;
    }


    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
        {
            return;
        }

        ItemInventorySingle return_inventory = eventData.pointerDrag.GetComponent<ItemInventorySingle>();

        if (box_content == return_inventory)
        {
            return;
        }

        // Data transfer logic.  Needs to be cleaned up.
        ItemSlotData source_data = return_inventory.GetItemSlotData();
        ItemSlotData sink_data = box_content.GetItemSlotData();

        bool data_swap = sink_data != null && source_data != null;

        bool source_addable = true;
        bool sink_addable = true;

        ItemEnum source_type = 0;
        ItemEnum sink_type = 0;
        int source_count = 0;
        int sink_count = 0;
        int full_sink_items = 0;
        int full_source_items = 0;

        if (source_data != null)
        {
            source_type = source_data.type;
            source_count = source_data.count;
            int items_to_source = box_content.ItemsAddableSwap(source_type, source_count);
            full_source_items = return_inventory.ItemsAddableSwap(source_type, source_count - items_to_source);

            source_addable = (full_source_items + items_to_source) == source_count;
        }

        if (sink_data != null)
        {
            sink_type = sink_data.type;
            sink_count = sink_data.count;
            int items_to_sink = return_inventory.ItemsAddableSwap(sink_type, sink_count);
            full_sink_items = box_content.ItemsAddableSwap(sink_type, sink_count - items_to_sink);

            sink_addable = (full_sink_items + items_to_sink) == sink_count;
        }

        if (data_swap && source_type == sink_type)
        {
            SwapIdentical(return_inventory, source_type, source_count);
        }
        else if (source_addable && sink_addable)
        {
            return_inventory.ClearSlotContent();
            box_content.ClearSlotContent();

            if (source_data != null)
            {
                box_content.AddData(source_type, source_count);
                return_inventory.AddData(source_type, full_source_items);
            }

            if (sink_data != null)
            {
                return_inventory.AddData(sink_type, sink_count);
                box_content.AddData(source_type, full_sink_items);
            }
        }
    }

    private void SwapIdentical(ItemInventorySingle return_inventory, ItemEnum type, int count)
    {
        return_inventory.ClearSlotContent();

        int can_add = box_content.ItemsAddableAdd(type, count);
        box_content.AddData(type, can_add);
        return_inventory.AddData(type, count - can_add);
    }
}
