using UnityEngine;

public class EnemyCharacterData : CharacterCurrentData
{

    public DropGenerator drop_generator;
    public uint reward_experience;

    public EnemyCharacterData(uint reward_experience, DropGenerator drops) : base()
    {
        this.reward_experience = reward_experience;
        this.drop_generator = drops;
    }

    public void SetDropMoney()
    {
        drop_generator.GetMoney();
    }

    public void SetItemDrop()
    {
        int item_int = drop_generator.GetItem();

        if (item_int > -1)
        {
            AllInventoryLookup.all.AddItem((ItemEnum)item_int, 1);
        }
    }
}
