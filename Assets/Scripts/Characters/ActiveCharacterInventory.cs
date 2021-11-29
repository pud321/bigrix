using UnityEngine;
using System.Collections.Generic;

public class ActiveCharacterInventory : CharacterInventory
{
    private ActionPrefabGenerator prefab_generator;

    protected void Awake()
    {
        base.Awake();
        prefab_generator = GetComponent<ActionPrefabGenerator>();
    }

    protected override PlayerCharacterData SelectCharacterObject(CharacterEnums type)
    {
        switch (type)
        {
            case CharacterEnums.Fighter:
                return new Fighter();
            case CharacterEnums.Mage:
                GameObject g = prefab_generator.InstantiateGameObject("MageBasicAttack", this.transform);
                return new Mage(g.GetComponent<ProjectileController>());
            case CharacterEnums.Priest:
                return new Priest();
            default:
                return null;
        }
    }

    public void SetSkillTracker(List<PlayerCharacterManager> current_characters)
    {
        GameObject g;

        for (int i = 0; i < current_characters.Count; i++)
        {
            List<ISkill> current_skills = current_characters[i].GetSkills();

            foreach (ISkill skill in current_skills)
            {
                character_inventory_ui_list[i].GetComponent<SkillTimerController>().CreatePrefab(skill);
            }

        }

    }
}
