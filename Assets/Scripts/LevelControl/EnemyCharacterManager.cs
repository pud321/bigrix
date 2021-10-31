using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacterManager : MonoBehaviour
{
    [SerializeField]
    public List<AbstractCharacter> current_enemy = new List<AbstractCharacter>();
    public List<AbstractCharacter> current_character = new List<AbstractCharacter>();

    public void Start()
    {
        foreach (AbstractCharacter ac in current_character)
        {
            ac.SetEnemies(current_enemy);
        }

        foreach (AbstractCharacter ac in current_enemy)
        {
            ac.SetEnemies(current_character);
        }
    }



}
