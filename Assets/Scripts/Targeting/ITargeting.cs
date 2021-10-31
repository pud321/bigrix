using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITargeting
{
    Transform GetCurrentTarget(bool set_target);
}
