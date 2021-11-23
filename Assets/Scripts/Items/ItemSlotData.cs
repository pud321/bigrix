using System;

[Serializable]
public class ItemSlotData
{
    public Item item;
    public int count;
    public ItemEnum type;

    public ItemSlotData(Item item)
    {
        this.item = item;
        type = item.type;
        count = 0;
    }

    public ItemSlotData(ItemEnum type) : this(ItemFactory.GetItem(type)) { }

}
