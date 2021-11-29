using System.Collections.Generic;
using UnityEngine;

public class DropGenerator
{
    private int money_min;
    private int money_max;

    private float percent_zero;
    private List<DropTable> drop_table;
    private float total_probability;

    public DropGenerator() : this(0, 0) { }

    public DropGenerator(int money_min, int money_max) : this(money_min, money_max, 0f) { }

    public DropGenerator(int money_min, int money_max, float percent_zero)
    {
        this.money_min = money_min;
        this.money_max = money_max;
        this.percent_zero = percent_zero;
        total_probability = 0;

        drop_table = new List<DropTable>();
    }

    public void GetMoney()
    {
        System.Random rnd = new System.Random();
        int money_roll = rnd.Next(money_min, money_max);
        MoneyData.ChangeMoney(money_roll);
    }

    public int GetItem()
    {
        System.Random rnd = new System.Random();
        double zero_roll = rnd.NextDouble();

        if (zero_roll < percent_zero)
        {
            return -1;
        }

        float pick_roll = UnityEngine.Random.Range(0, total_probability);
        float counted_probabilities = 0f;

        foreach (DropTable drop in drop_table)
        {
            counted_probabilities += drop.probability;

            if (pick_roll < counted_probabilities)
            {
                return (int)drop.type;
            }
        }

        return -1;
    }

    public void AddItem(ItemEnum type, float probability)
    {
        drop_table.Add(new DropTable(type, probability));
        total_probability += probability;
    }
}
