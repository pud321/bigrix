using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseUIManager : MonoBehaviour
{
    [SerializeField] private GameObject level_button_prefab;
    [SerializeField] private Transform level_button_container;

    public event LevelEnumEventHandler OnLevelEvent;

    public void CreateUnlockedLevelButtons()
    {
        GameObject g;
        LevelBaseButton temp_button;

        foreach (LevelEnums level in LevelController.unlocked_levels)
        {
            g = Instantiate(level_button_prefab, level_button_container);
            temp_button = g.GetComponent<LevelBaseButton>();
            temp_button.SetButton(level);
            temp_button.OnClick += ButtonSceneChangeRequest;
        }
    }

    public void ButtonSceneChangeRequest(LevelEnums level)
    {
        OnLevelEvent?.Invoke(level);
    }
}
