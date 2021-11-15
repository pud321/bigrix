using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public GameObject character_prefab;

    private PlayerCharacterSpawner player_spawner;
    private EnemyCharacterSpawner enemy_spawner;
    private Queue<GameObject> cleanup_queue;
    private float destroy_delay = 1.5f;

    void Awake()
    {
        player_spawner = new PlayerCharacterSpawner(this);
        enemy_spawner = new EnemyCharacterSpawner(this);
        cleanup_queue = new Queue<GameObject>();
    }

    public GameObject InstantiateCharacter(Vector3 position)
    {
        return Instantiate(character_prefab, position, Quaternion.identity);
    }

    public void SpawnAll(List<PlayerCharacterData> player_array)
    {
        player_spawner.SpawnGroup(player_array);
        enemy_spawner.SpawnGroup();

        enemy_spawner.SetTargets(player_spawner.generic_manager_list);
        player_spawner.SetTargets(enemy_spawner.generic_manager_list);

        foreach (EnemyCharacterManager ac in enemy_spawner.manager_list)
        {
            ac.OnDeath += DestoryCharacter;
        }

        foreach (PlayerCharacterManager ac in player_spawner.manager_list)
        {
            ac.OnDeath += DestoryCharacter;
        }
    }

    public void AddSpawnListener(ManagerEventSender<PlayerCharacterManager> function)
    {
        player_spawner.OnSpawn += function;
    }

    public void AddSpawnListener(ManagerEventSender<EnemyCharacterManager> function)
    {
        enemy_spawner.OnSpawn += function;
    }

    public void ListenToCharacterDamage(CharacterManager.CharacterDamageHandler subscription_function)
    {
        foreach (CharacterManager ac in enemy_spawner.generic_manager_list)
        {
            ac.OnCharacterHealth += subscription_function;
        }

        foreach (CharacterManager ac in player_spawner.generic_manager_list)
        {
            ac.OnCharacterHealth += subscription_function;
        }
    }

    public void ListenToCharacterDeath(ManagerEventSender<PlayerCharacterManager> subscription_function)
    {
        foreach (PlayerCharacterManager ac in player_spawner.manager_list)
        {
            ac.OnDeath += subscription_function;
        }
    }

    public void ListenToCharacterDeath(ManagerEventSender<EnemyCharacterManager> subscription_function)
    {
        foreach (EnemyCharacterManager ac in enemy_spawner.manager_list)
        {
            ac.OnDeath += subscription_function;
        }
    }

    private void DestoryCharacter(CharacterManager sender)
    {
        cleanup_queue.Enqueue(sender.gameObject);
    }

    private void Update()
    {
        foreach (GameObject g in cleanup_queue)
        {
            Destroy(g, destroy_delay);
        }

        cleanup_queue.Clear();
    }

}
