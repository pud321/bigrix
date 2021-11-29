using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemTooltip : MonoBehaviour
{
    [SerializeField] private Text name_field;
    [SerializeField] private Text type_field;
    [SerializeField] private Text rarity_field;
    [SerializeField] private Text data;
    [SerializeField] private Text skill_text;


    private RectTransform rt;

    private void Awake()
    {
        rt = GetComponent<RectTransform>();
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void SetItem(Item item_data)
    {
        if (item_data == null)
        {
            gameObject.SetActive(false);
            return;
        }

        gameObject.SetActive(true);
        SetPosition();

        name_field.text = item_data.name;

        string base_string = "Classes: ";
        foreach (CharacterEnums c in item_data.characters)
        {
            base_string += c.ToString().Substring(0, 2);
        }
        type_field.text = base_string;
        rarity_field.text = item_data.rarity.ToString();
        SetData(item_data);
        SetTechnique(item_data);
    }

    private void SetPosition()
    {
        int shift_x = Input.mousePosition.x > Screen.width / 2f ? -1 : 1;
        int shift_y = Input.mousePosition.y > Screen.height / 2f ? -1 : 1;
        rt.position = new Vector2(Input.mousePosition.x + rt.sizeDelta.x * shift_x / 2f, Input.mousePosition.y + rt.sizeDelta.y * shift_y / 2f);
    }

    private void SetData(Item item_data)
    {
        string data_string = "";

        if (item_data.damage > 0)
        {
            data_string += "damage: " + item_data.damage.ToString() + "\n";
        }

        if (item_data.health > 0)
        {
            data_string += "health: " + item_data.health.ToString("0") + "\n";
        } 

        data.text = data_string;
    }

    private void SetTechnique(Item item_data)
    {
        if (item_data.isSkill)
        {
            skill_text.text = "Skill: " + item_data.description;
            return;
        }

        skill_text.text = "";
    }
}
 