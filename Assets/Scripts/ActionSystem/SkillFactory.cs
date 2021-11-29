using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SkillFactory
{
    private static ActionPrefabGenerator prefab_generator;

    // Downdash
    public static int downdash_bonus_damage = 20;
    public static float downdash_range = 2f;
    public static float downdash_frequency = 0.05f;

    public static void Initialize(ActionPrefabGenerator generator)
    {
        prefab_generator = generator;
    }

    public static ISkill Get(SkillEnum skill, IActionData data, Transform this_tranform)
    {
        switch (skill)
        {
            case SkillEnum.Downdash:
                GameObject g = prefab_generator.InstantiateGameObject("DowndashAttack", this_tranform);
                return new DowndashSkill(this_tranform, data, g.GetComponent<ProjectileController>());
        }

        return null;
    }
}
