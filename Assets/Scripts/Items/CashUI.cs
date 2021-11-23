using UnityEngine;
using UnityEngine.UI;

public class CashUI : MonoBehaviour
{
    private Text cash_text;

    private void Awake()
    {
        cash_text = GetComponent<Text>();
    }

    private void Start()
    {
        MoneyData.OnChangeMoney += SetCashChange;
        MoneyData.OnUpdateMoney += SetCash;
        MoneyData.UpdateMoneyListeners();
    }

    public void SetCash()
    {
        if (cash_text == null)
        {
            Debug.Log("CASH TEXT IS NULL");
            Debug.Log("Retrieve component: " + GetComponent<Text>());
        }
        else
        {
            cash_text.text = "Cash: " + MoneyData.total_money.ToString();
        }
    }

    public void SetCashChange(int change)
    {
        SetCash();
    }
}
