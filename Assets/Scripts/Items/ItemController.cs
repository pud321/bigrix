using UnityEngine;

[System.Serializable]
public class ItemController
{
    private Item[] items;

    public ItemController(int items_quantity)
    {
        items = new Item[items_quantity];
    }

    public Item AddItem(Item item, int slot)
    {
        Item previous_item = items[slot];
        items[slot] = item;
        return previous_item;
    }

}
