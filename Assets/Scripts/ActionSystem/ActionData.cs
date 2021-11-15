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
    public DamageType damage_type;
    public ActionType action_type;

    private int _damage;

    public void ChangeDamage(int damage)
    {
        _damage += damage;
    }
}
