using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterManager : CharacterManager
{
    public PlayerCharacterData player_character_data;

    public event ManagerEventSender<PlayerCharacterManager> OnDeath;

    public override bool is_enemy { get { return false; } }

    public void SetupCharacterData(PlayerCharacterData character_data)
    {
        player_character_data = character_data;
        player_character_data.fixed_data.OnMaxHealthChange += MaxHealthChange;
        player_character_data.fixed_data.OnMovementSpeedChange += MovementChange;
        base.SetupCharacterData(character_data);
    }

    protected override void CheckAndDestroyCharacter()
    {
        if (current_health <= 0)
        {
            hold_check_and_destroy = true;
            player_character_data.fixed_data.OnMaxHealthChange -= MaxHealthChange;
            player_character_data.fixed_data.OnMovementSpeedChange -= MovementChange;

            OnDeath?.Invoke(this);
            RunGeneralEvents();
        }
    }

    private void MaxHealthChange(int change)
    {
        if (change > 0)
        {
            ChangeHealth(change, 0);
        }
        else
        {
            ChangeHealth(0, 0);
        }
    }

    private void MovementChange(float change)
    {
        if (change > 0)
        {
            _navmeshagent.speed = character_data.movement_speed;
        }
    }

    protected override void SetActions()
    {
        base.SetActions();
        List<SkillEnum> skills = player_character_data.GetSkills();

        _action_controller.ResetSkilledActions();

        foreach (SkillEnum skill in skills)
        {
            _action_controller.UpdateSkilledAction(SkillFactory.Get(skill, character_data.basic_attack_group, _this_transform));
        }
    }

    public List<ISkill> GetSkills()
    {
        return _action_controller.skilled_action;
    }
}
