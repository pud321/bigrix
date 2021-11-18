using UnityEngine;

public class ActionData : IActionData
{
    public float frequency { get; set; }
    public int damage
    {
        get { return _damage; }
        set { _damage = value; }
    }
    public float range { get; set; }
    public DamageType damage_type { get; set; }
    public ActionType action_type { get; set; }

    private int _damage;

    public ActionData()
    {
        frequency = 0f;
        damage = 0;
        range = 0f;
        damage_type = 0;
        action_type = 0;
    }

    public void ChangeDamage(int damage)
    {
        _damage += damage;
    }
}
