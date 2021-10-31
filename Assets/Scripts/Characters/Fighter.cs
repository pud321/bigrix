using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : AbstractCharacter
{
    private void Start()
    {
        base.base_speed = 5f;
        base.Start();
    }
}
