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
        List<AbstractCharacter> targets = new List<AbstractCharacter>();

        GameObject target_object = new GameObject();
        targets.Add(target_object.AddComponent<BasicEnemy>());

        GameObject character_object = new GameObject();

        ITargeting new_targeting = new NearestTarget(character_object.transform, targets);

        Transform current_target = new_targeting.GetCurrentTarget(true);

        yield return null;
        Assert.AreSame(current_target, target_object.transform);

    }

    [UnityTest]
    public IEnumerator NoTargetNearest()
    {
        List<AbstractCharacter> targets = new List<AbstractCharacter>();    
        GameObject character_object = new GameObject();

        ITargeting new_targeting = new NearestTarget(character_object.transform, targets);

        Transform current_target = new_targeting.GetCurrentTarget(true);

        yield return null;
        Assert.AreSame(current_target, character_object.transform);

    }
}
