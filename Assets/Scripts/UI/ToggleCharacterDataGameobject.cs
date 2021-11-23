using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleCharacterDataGameobject : ToggleGameobject, ICharacterDataTracker
{
    public DisplayCharacterData display_data;
    private PlayerCharacterData this_character_data;

    protected override void Start()
    {
        base.Start();
        if (toggle_object == null) { return; }

        display_data = toggle_object.GetComponent<DisplayCharacterData>();
    }

    public override void Toggle()
    {
        if (display_data.data == null)
        {
            InitiateNewCharacter();
        }
        else if (display_data.data == this_character_data)
        {
            toggle_object.SetActive(!toggle_object.activeSelf);
        }
        else
        {
            InitiateNewCharacter();
        }
    }

    public void SetPlayerData(PlayerCharacterData data)
    {
        this_character_data = data;
    }

    public void InitiateNewCharacter()
    {
        toggle_object.SetActive(true);
        display_data.SetData(this_character_data);
    }
}
