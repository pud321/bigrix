public class EnemyCharacterData : CharacterCurrentData
{

    public DropGenerator drop_generator;
    public uint reward_experience;

    public EnemyCharacterData(uint reward_experience, DropGenerator drops) : base()
    {
        this.reward_experience = reward_experience;
        this.drop_generator = drops;
    }

    public int GetDropMoney()
    {
        return drop_generator.GetMoney();
    }
}
