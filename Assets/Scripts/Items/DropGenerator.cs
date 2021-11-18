using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropGenerator
{
    private int money_min;
    private int money_max;
    private DropTable drop_table;

    public DropGenerator() : this(0, 0) { }

    public DropGenerator(int money_min, int money_max) : this(money_min, money_max, null) { }

    public DropGenerator(int money_min, int money_max, DropTable drops)
    {
        this.money_min = money_min;
        this.money_max = money_max;
        drop_table = drops;
    }

    public int GetMoney()
    {
        System.Random rnd = new System.Random();
        return rnd.Next(money_min, money_max);
    }

    public Item GetItem()
    {
        if (drop_table == null) { return null; }

        return null;
    }
}
