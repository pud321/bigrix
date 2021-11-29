using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DowndashProjectileController : ProjectileController
{
    Vector3 jump_shift = new Vector3(0f, 4f, 0f);

    protected override void Awake()
    {
        base.Awake();
        speed = 6;
    }

    public override bool RequestAttack(Transform start_transform, Transform end_transform)
    {
        bool response = base.RequestAttack(start_transform, end_transform);

        if (response)
        {
            transform.position = start_transform.position + y_offset + jump_shift;
        }

        return response;
    }
    protected override void Update()
    {
        if (projectile_gameobject.activeSelf)
        {
            float distance_covered = (Time.time - start_time) * speed;
            float distance_fraction = distance_covered / distance_total;
            transform.position = Vector3.Lerp(start_transform.position + y_offset + jump_shift, end_transform.position + y_offset, distance_fraction);

            if (distance_fraction >= 1f || is_collision)
            {
                projectile_gameobject.SetActive(false);
            }
        }
    }
}
