public class EnemyCharacterData : CharacterCurrentData
{

    public bool drop_table;
    public uint reward_experience;

    public EnemyCharacterData(CharacterEnums type, uint reward_experience, bool drop_table) : base(type)
    {
        this.reward_experience = reward_experience;
        this.drop_table = drop_table;
    }
}
