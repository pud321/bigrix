using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubdisplayFixedCharacterData : MonoBehaviour, ICharacterDataDisplay
{
    [SerializeField] private Text name;
    [SerializeField] private Text level;

    public void UpdateDisplay(PlayerCharacterData data)
    {
        name.text = data.name;
        level.text = "Level: " + data.level.ToString();
    }

}
