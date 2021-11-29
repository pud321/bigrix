using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTimerController : MonoBehaviour
{
    [SerializeField] private GameObject skill_timer_prefab;
    [SerializeField] private GameObject skill_timer_box;

    public void CreatePrefab(ISkill skill)
    {
        GameObject g = Instantiate(skill_timer_prefab, skill_timer_box.transform);
        g.GetComponent<SkillTimerUI>().SetSkill(skill);
    }
}
