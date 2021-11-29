using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DowndashSkill : AbstractAction, IAction, ISkill
{
    public event BooleanAnimationEventHandler OnAnimationChangeRequest;

    private ITargeting _targeting;
    private DamageType damage_type;
    private IActionData data;
    
    private SkinnedMeshRenderer mesh_renderer;
    private Canvas this_canvas;

    private CharacterManager current_target;
    private ProjectileController projectile_control;

    public string name { get { return _name; } }
    public float percent_time_remaining { get { return timeRemaining / period; } }
    public float period;
    public event SkillRanEventHandler OnSkillRan;

    public static string _name = "Downdash Technique";
    public static int bonus_damage = 20;
    public static float grange = 2f;
    public static float gfrequency = 0.05f;
    public static string description = "Attack every " + (1/gfrequency).ToString("0.#") + "s with " + DowndashSkill.bonus_damage.ToString() + " bonus dmg";

    public DowndashSkill(Transform this_tranform, IActionData data, ProjectileController projectile_control)
    {
        this.data = data;
        this._range = grange;
        this.frequency = gfrequency;
        period = 1 / gfrequency;
        this._action_type = ActionType.Attack;
        this.damage_type = DamageType.Normal;

        _this_transform = this_tranform;
        mesh_renderer = _this_transform.GetComponentInChildren<SkinnedMeshRenderer>();
        this_canvas = _this_transform.GetComponentInChildren<Canvas>();

        if (projectile_control != null)
        {
            this.projectile_control = projectile_control;
            this.projectile_control.OnProjectileHit += ProjectileHit;
        }


    }

    public int damage { get { return (data.damage + bonus_damage); } }

    public override Transform DesiredTarget()
    {
        return _targeting.GetCurrentTarget();
    }

    public override float RunAction()
    {
        Transform target_transform = DesiredTarget();

        if (target_transform != _this_transform)
        {
            CharacterManager desired_target = target_transform.GetComponent<CharacterManager>();

            if (projectile_control == null)
            {
                _next_action_time = Time.time + period;
                return 0f;
            }


            bool attempt_attack = projectile_control.RequestAttack(_this_transform, target_transform);

            if (attempt_attack)
            {
                OnSkillRan?.Invoke();
                current_target = desired_target;
                _next_action_time = Time.time + period;
                mesh_renderer.enabled = false;
                this_canvas.enabled = false;
                return 2f;
            }
        }

        return 0f;
    }

    public override void StopAction()
    {
        mesh_renderer.enabled = true;
        this_canvas.enabled = true;
    }

    public void ProjectileHit()
    {
        current_target.ChangeHealth(-damage, damage_type);
        StopAction();
    }

    public override void SetTargets(List<CharacterManager> targets)
    {
        _targets = targets;
        _targeting = new NearestTarget(_this_transform, _targets, 1f);
    }

}
