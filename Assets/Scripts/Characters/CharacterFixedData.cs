using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void CharacterDataEventHandler<T>(T change);

[System.Serializable]
public class CharacterFixedData
{
    public CharacterEnums type;
    public string name;
    public ActionData basic_attack;

    public event CharacterDataEventHandler<int> OnMaxHealthChange;
    public event CharacterDataEventHandler<float> OnMovementSpeedChange;

    public int levelup_damage = 0;
    public int levelup_maxhealth = 0;
    public float levelup_movement = 0f;
    
    private int _max_health;
    private float _base_movement_speed;

    public int max_health { 
        get { return _max_health; } 
        set { _max_health = value; } 
    }

    public float base_movement_speed
    {
        get { return _base_movement_speed; }
        set { _base_movement_speed = value; }
    }

    public void ChangeMaxHealth(int delta)
    {
        _max_health += delta;
        OnMaxHealthChange?.Invoke(delta);
    }

    public void ChangeBaseSpeed(float delta)
    {
        _base_movement_speed += delta;
        OnMovementSpeedChange?.Invoke(delta);
    }

    public CharacterFixedData GetCopy()
    {
        return new CharacterFixedData
        {
            type = this.type,
            name = this.name,
            base_movement_speed = this.base_movement_speed,
            max_health = this.max_health,
            basic_attack = new ActionData
            {
                frequency = this.basic_attack.frequency,
                damage = this.basic_attack.damage,
                range = this.basic_attack.range,
                action_type = this.basic_attack.action_type,
                damage_type = this.basic_attack.damage_type,
            },
            levelup_damage = this.levelup_damage,
            levelup_maxhealth = this.levelup_maxhealth,
            levelup_movement = this.levelup_movement,
        };
    }

    public void RunLevelUp()
    {
        ChangeMaxHealth(levelup_maxhealth);
        ChangeBaseSpeed(levelup_movement);
    }
}