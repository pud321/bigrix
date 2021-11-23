using UnityEngine;
using UnityEngine.UI;

public class DataResetter : MonoBehaviour
{
    private Button reset_button;

    public void Awake()
    {
        reset_button = GetComponent<Button>();
        reset_button.onClick.AddListener(Reset);
    }

    private void Reset()
    {
        DataHandler.deleteData("pd_1");

        for (int i = 0; i < GameStats.max_characters; i++)
        {
            DataHandler.deleteData("pc_" + i.ToString());
        }
    }
}
