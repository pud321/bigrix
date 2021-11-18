using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void ProjectileHitEventHandler();

public class ProjectileController : MonoBehaviour
{
    public event ProjectileHitEventHandler OnProjectileHit;

    private GameObject projectile_gameobject;
    private Transform start_transform;
    private Transform end_transform;
    private Vector3 y_offset;

    private float speed = 3;
    private float distance_total;
    private float start_time;
    private bool is_collision;

    private void Awake()
    {
        projectile_gameobject = this.gameObject;
        projectile_gameobject.SetActive(false);
        GetComponent<Rigidbody>().isKinematic = true;
    }

    public bool RequestAttack(Transform start_transform, Transform end_transform)
    {
        if (projectile_gameobject.activeSelf)
        {
            return false;
        }

        this.start_transform = start_transform;
        this.end_transform = end_transform;

        is_collision = false;
        start_time = Time.time;
        projectile_gameobject.SetActive(true);
        distance_total = Vector3.Distance(start_transform.position, end_transform.position);
        return true;
    }

    private void Update()
    {
        if (projectile_gameobject.activeSelf)
        {
            float distance_covered = (Time.time - start_time) * speed;
            float distance_fraction = distance_covered / distance_total;
            transform.position = Vector3.Lerp(start_transform.position + y_offset, end_transform.position + y_offset, distance_fraction);

            if (distance_fraction >= 1f || is_collision)
            {
                projectile_gameobject.SetActive(false);
            }
        }
    }

    public void Set_YOffSet(float offset)
    {
        y_offset = new Vector3(0f, offset);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.transform == end_transform)
        {
            is_collision = true;
            OnProjectileHit?.Invoke();
        }
    }
}
