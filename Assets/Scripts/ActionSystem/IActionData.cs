using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActionData
{
    public int damage { get; }
    public float range { get; }
    public float frequency { get; }
}
