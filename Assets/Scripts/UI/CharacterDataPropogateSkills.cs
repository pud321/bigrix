using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterDataPropogateSkills : MonoBehaviour, ICharacterDataDisplay
{
    [SerializeField] private Transform skillbox_parent;
    [SerializeField] private GameObject skillbox_prefab;

    private List<GameObject> skillbox_prefabs;

    private void Awake()
    {
        skillbox_prefabs = new List<GameObject>();
    }

    public void UpdateDisplay(PlayerCharacterData data)
    {
        List<SkillEnum> skill_list = data.GetSkills();

        foreach (GameObject g in skillbox_prefabs)
        {
            g.SetActive(false);
        }

        for (int i = 0; i < skill_list.Count; i++)
        {
            if (i+1 > skillbox_prefabs.Count)
            {
                skillbox_prefabs.Add(Instantiate(skillbox_prefab, skillbox_parent));
            }

            skillbox_prefabs[i].SetActive(true);
            skillbox_prefabs[i].GetComponentInChildren<Text>().text = skill_list[i].ToString();
        }
    }

}
