using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelBaseButton : MonoBehaviour
{
    private Text button_text;
    private Button button_fxn;
    private LevelEnums level;

    public event LevelEnumEventHandler OnClick;

    public void Awake()
    {
        button_text = GetComponentInChildren<Text>();
        button_fxn = GetComponent<Button>();
    }

    public void SetButton(LevelEnums level)
    {
        this.level = level;
        button_text.text = level.ToString();
        button_fxn.onClick.AddListener(ButtonListener);
    }

    private void ButtonListener()
    {
        OnClick?.Invoke(level);
    }
}
