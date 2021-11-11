using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TargetingPlayTest
{
    //[UnityTest]
    //public IEnumerator TestSingleTargetNearest()
    //{
    //    List<CharacterManager> targets = new List<CharacterManager>();

    //    GameObject target_object = new GameObject();
    //    targets.Add(target_object.AddComponent<CharacterManager>());

    //    GameObject character_object = new GameObject();

    //    ITargeting new_targeting = new NearestTarget(character_object.transform, targets);

    //    Transform current_target = new_targeting.GetCurrentTarget();

    //    yield return null;
    //    Assert.AreSame(current_target, target_object.transform);

    //}

    //[UnityTest]
    //public IEnumerator TestNoTargetNearest()
    //{
    //    List<CharacterManager> targets = new List<CharacterManager>();    
    //    GameObject character_object = new GameObject();

    //    ITargeting new_targeting = new NearestTarget(character_object.transform, targets);

    //    Transform current_target = new_targeting.GetCurrentTarget();

    //    yield return null;
    //    Assert.AreSame(current_target, character_object.transform);
    //}

    //[UnityTest]
    //public IEnumerator TestTimerUpdatesTarget()
    //{

    //    float period_time = 0.15f;

    //    List<CharacterManager> targets = new List<CharacterManager>();

    //    GameObject first_target_object = new GameObject();
    //    GameObject second_target_object = new GameObject();
    //    targets.Add(second_target_object.AddComponent<CharacterManager>());
    //    targets.Add(first_target_object.AddComponent<CharacterManager>());

    //    first_target_object.transform.position = new Vector3(1, 0);
    //    second_target_object.transform.position = new Vector3(2, 0);

    //    GameObject character_object = new GameObject();
    //    character_object.transform.position = new Vector3(0, 0);

    //    ITargeting new_targeting = new NearestTarget(character_object.transform, targets, period_time);

    //    Transform current_target = new_targeting.GetCurrentTarget();

    //    yield return null;
    //    Assert.AreSame(current_target, first_target_object.transform);


    //    yield return new WaitForSeconds(period_time);
    //    current_target = new_targeting.GetCurrentTarget();
    //    Assert.AreSame(current_target, first_target_object.transform);

    //    second_target_object.transform.position = new Vector3(0.5f, 0);

    //    yield return new WaitForSeconds(period_time);
    //    current_target = new_targeting.GetCurrentTarget();
    //    Assert.AreSame(current_target, second_target_object.transform);
    //}

}
