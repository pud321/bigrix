using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void SkillRanEventHandler();

public interface ISkill : IAction
{
    string name { get; }
    float percent_time_remaining { get; }
    event SkillRanEventHandler OnSkillRan;
}
