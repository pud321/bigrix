using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{

    public delegate void CharacterEventHandler(AbstractCharacter sender);
    public event CharacterEventHandler OnCharacterSpawn;

    private CharacterFactory character_factory;

    private List<SpawnDefinition> level_characters = new List<SpawnDefinition>();
    private List<SpawnDefinition> level_enemy = new List<SpawnDefinition>();

    private List<AbstractCharacter> current_character = new List<AbstractCharacter>();
    private List<AbstractCharacter> current_enemy = new List<AbstractCharacter>();

    void Awake()
    {
        character_factory = GetComponent<CharacterFactory>();
        level_characters.Add(new SpawnDefinition(CharacterEnums.Fighter, new Vector3(3.15f, 1f, -4.22f), false));
        level_characters.Add(new SpawnDefinition(CharacterEnums.Mage, new Vector3(0f, 1f, -1.49f), false));
        level_enemy.Add(new SpawnDefinition(CharacterEnums.Enemy, new Vector3(0f, 0.6f, 2f), true));
        level_enemy.Add(new SpawnDefinition(CharacterEnums.Enemy, new Vector3(1f, 0.6f, 3.3f), true));
    }

    public void StartLevel()
    {
        Spawn(level_characters, current_character);
        Spawn(level_enemy, current_enemy);
        _SetTargets(current_enemy, current_character);
        _SetTargets(current_character, current_enemy);
    }

    protected void Spawn(List<SpawnDefinition> this_list, List<AbstractCharacter> save_list)
    {
        foreach (SpawnDefinition to_spawn in this_list)
        {
            GameObject g = character_factory.SpawnCharacter(to_spawn.spawn_name, to_spawn.spawn_position);

            if (g != null)
            {
                AbstractCharacter current_spawn = g.GetComponent<AbstractCharacter>();
                save_list.Add(current_spawn);
                OnCharacterSpawn?.Invoke(current_spawn);
            }
        }
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

    public void ListenToCharacterDamage(AbstractCharacter.CharacterDamageHandler subscription_function)
    {
        foreach (AbstractCharacter ac in current_character)
        {
            ac.OnCharacterHealth += subscription_function;
        }

        foreach (AbstractCharacter ac in current_enemy)
        {
            ac.OnCharacterHealth += subscription_function;
        }
    }

    public void ListenToCharacterDeath(AbstractCharacter.CharacterEventHandler subscription_function)
    {
        foreach (AbstractCharacter ac in current_character)
        {
            ac.OnCharacterDeath += subscription_function;
        }

        foreach (AbstractCharacter ac in current_enemy)
        {
            ac.OnCharacterDeath += subscription_function;
        }
    }
}
