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

    public void RunLevelUp()
    {
        ChangeMaxHealth(levelup_maxhealth);
        ChangeBaseSpeed(levelup_movement);
    }
}