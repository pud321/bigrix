public interface IActionData
{
    int damage { get; }
    float range { get; }
    float frequency { get; }
    DamageType damage_type { get; }
    ActionType action_type { get; }

}
