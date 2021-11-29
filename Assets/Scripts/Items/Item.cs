using System.Collections.Generic;

public class Item
{
    public string name;
    public ItemEnum type;
    public ItemRarityEnum rarity;

    public HashSet<CharacterEnums> characters;

    public int damage = 0;
    public float damage_percent = 0f;
    public float range = 0f;
    public float frequency_percent = 0f;
    public float health = 0f;

    public bool basic_attack = false;
    public SkillEnum skill;
    public bool isSkill = false;
    public string description;

    public Item(ItemEnum type, CharacterEnums[] characters) : this(type, characters, true) { }

    public Item(ItemEnum type, CharacterEnums[] characters, bool basic_attack)
    {
        this.type = type;
        this.name = type.ToString();
        this.characters = new HashSet<CharacterEnums>();
        this.basic_attack = basic_attack;

        foreach (CharacterEnums c in characters)
        {
            this.characters.Add(c);
        }

    }

    public void SetSkill(SkillEnum skill)
    {
        isSkill = true;
        this.skill = skill;
    }

    public float ShiftHealth(float value)
    {
        return value + this.health;
    }

    public float ShiftRange(float value)
    {
        return value + this.range;
    }

    public float ShiftDamage(float value)
    {
        return value + this.damage;
    }

    public float ScaleDamage(float value)
    {
        return (1 + damage_percent) * value;
    }

    public float ScaleFrequency(float value)
    {
        return (1 + frequency_percent) * value;
    }
}
