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

    public Item(ItemEnum type, CharacterEnums[] characters)
    {
        this.type = type;
        this.name = type.ToString();
        this.characters = new HashSet<CharacterEnums>();

        foreach (CharacterEnums c in characters)
        {
            this.characters.Add(c);
        }

    }

    public float ShiftRange(float value)
    {
        return value + this.range;
    }

    public int ShiftDamage(int value)
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
