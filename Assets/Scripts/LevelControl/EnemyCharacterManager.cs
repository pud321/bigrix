using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacterManager : MonoBehaviour
{
    [SerializeField]
    public List<AbstractCharacter> current_enemy = new List<AbstractCharacter>();
    public List<AbstractCharacter> current_character = new List<AbstractCharacter>();

    public void Start()
    {
        _SetTargets(current_enemy, current_character);
        _SetTargets(current_character, current_enemy);
    }

    private void CleanupCharacter(AbstractCharacter this_character)
    {
        this_character.OnCharacterDeath -= CleanupCharacter;

        current_character.Remove(this_character);
        current_enemy.Remove(this_character);
    }

    private void _SetTargets(List<AbstractCharacter> left_target, List<AbstractCharacter> right_target)
    {
        foreach (AbstractCharacter ac in left_target)
        {
            ac.SetTargets(left_target, right_target);
            ac.OnCharacterDeath += CleanupCharacter;
        }
    }

}
