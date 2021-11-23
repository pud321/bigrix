using UnityEngine;
using UnityEngine.UI;

public delegate void UnlockedCharacterEventHandler();

public class EmptyCharacterUI : MonoBehaviour
{
    public event UnlockedCharacterEventHandler OnCharacterAttemptUnlock;

    [SerializeField] private Text main_text;
    [SerializeField] private Text cost_text;

    private int cost;

    private void Start()
    {
        main_text.text = "Unlock New Character";
    }

    public void SetCost(int cost)
    {
        this.cost = cost;
        cost_text.text = "$" + cost.ToString();
    }

    public void AttemptUnlock()
    {
        bool unlock_attempt = MoneyData.isEnoughMoney(cost);
        if (unlock_attempt)
        {
            OnCharacterAttemptUnlock?.Invoke();
        }
    }
}
