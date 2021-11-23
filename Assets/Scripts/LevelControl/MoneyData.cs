public delegate void MoneyChangeEventHandler(int change);
public delegate void MoneyUpdateEventHandler();

[System.Serializable]
public static class MoneyData
{
    public static int total_money;
    public static event MoneyChangeEventHandler OnChangeMoney;
    public static event MoneyUpdateEventHandler OnUpdateMoney;

    public static bool isEnoughMoney(int price)
    {
        return total_money >= price;
    }

    public static bool ChangeMoney(int change)
    {
        if (!isEnoughMoney(-change))
        {
            return false;
        }

        total_money += change;
        OnChangeMoney?.Invoke(change);
        return true;
    }

    public static void UpdateMoneyListeners()
    {
        OnUpdateMoney?.Invoke();
    }

    public static void ClearEvents()
    {
        OnChangeMoney = null;
        OnUpdateMoney = null;
    }
}
