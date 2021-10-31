using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TargetingPlayTest
{
    [UnityTest]
    public IEnumerator SingleTargetNearest()
    {
        List<AbstractEnemy> enemies = new List<AbstractEnemy>();

        GameObject enemy_object = new GameObject();
        enemies.Add(enemy_object.AddComponent<BasicEnemy>());

        GameObject character_object = new GameObject();

        ITargeting new_targeting = new NearestTarget(character_object.transform, enemies);

        Transform current_target = new_targeting.GetCurrentTarget(true);

        yield return null;
        Assert.AreSame(current_target, enemy_object.transform);

    }

    [UnityTest]
    public IEnumerator NoTargetNearest()
    {
        List<AbstractEnemy> enemies = new List<AbstractEnemy>();    
        GameObject character_object = new GameObject();

        ITargeting new_targeting = new NearestTarget(character_object.transform, enemies);

        Transform current_target = new_targeting.GetCurrentTarget(true);

        yield return null;
        Assert.AreSame(current_target, character_object.transform);

    }
}
