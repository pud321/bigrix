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
                temp_item.health = 10;
                return temp_item;
            case ItemEnum.Wand:
                temp_item = new Item(ItemEnum.Wand, new CharacterEnums[] { CharacterEnums.Mage, CharacterEnums.Priest });
                temp_item.damage = 2;
                return temp_item;
            case ItemEnum.DowndashTechnique:
                temp_item = new Item(ItemEnum.DowndashTechnique, new CharacterEnums[] { CharacterEnums.Fighter }, false);
                temp_item.name = DowndashSkill._name;
                temp_item.SetSkill(SkillEnum.Downdash);
                temp_item.description = DowndashSkill.description;
                return temp_item;
            default:
                return null;
        }
    }
}
