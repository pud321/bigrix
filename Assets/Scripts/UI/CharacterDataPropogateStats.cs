using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterDataPropogateStats : MonoBehaviour, ICharacterDataDisplay
{
    [SerializeField] private Text damage_text;
    [SerializeField] private Text range_text;
    [SerializeField] private Text atk_speed_text;
    [SerializeField] private Text mv_speed_text;


    public void UpdateDisplay(PlayerCharacterData data)
    {
        damage_text.text = GetMenuText(data.basic_attack_group.damage, "dmg");
        range_text.text = GetMenuText(data.basic_attack_group.range, "rng");
        atk_speed_text.text = GetMenuText(System.Math.Round(1/data.basic_attack_group.frequency, 2), "atk.spd");
        mv_speed_text.text = GetMenuText(data.movement_speed, "mv.spd");
    }

    private string GetMenuText<T>(T value, string name)
    {
        return name + ": " + value.ToString();
    }

}
