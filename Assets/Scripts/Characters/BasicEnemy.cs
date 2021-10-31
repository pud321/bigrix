using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : AbstractCharacter
{
    private void Start()
    {
        base.base_speed = 0.1f;
        base.Start();
    }
}
