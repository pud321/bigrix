public static class ItemFactory
{
    public static Item GetItem(ItemEnum name)
    {
        Item temp_item;

        switch (name)
        {
            case ItemEnum.Dagger:
                temp_item = new Item(ItemEnum.Dagger, new CharacterEnums[] { CharacterEnums.Fighter });
                temp_item.damage = 1;
                return temp_item;
            case ItemEnum.Wand:
                temp_item = new Item(ItemEnum.Wand, new CharacterEnums[] { CharacterEnums.Mage, CharacterEnums.Priest });
                temp_item.damage = 2;
                return temp_item;
            default:
                return null;
        }
    }
}
