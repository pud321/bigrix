using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractEnemy : MonoBehaviour
{
    protected Transform _enemy_transform;

    public Transform enemy_transform
    {
        get { return _enemy_transform; }
    }

    private void Awake()
    {
        _enemy_transform = this.transform;
    }
}
