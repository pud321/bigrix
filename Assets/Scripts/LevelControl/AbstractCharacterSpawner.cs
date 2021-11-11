using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void ManagerEventSender<T>(T sender);

public abstract class AbstractCharacterSpawner<T>
    where T : CharacterManager
{
    public List<CharacterManager> generic_manager_list = new List<CharacterManager>();
    public List<T> manager_list = new List<T>();

    public event ManagerEventSender<T> OnSpawn;

    protected SpawnerManager spawner_monobehavior;
    protected float spawn_tolerance = 1f;


    public void CleanupCharacter(T manager)
    {
        manager_list.Remove(manager);
        generic_manager_list.Remove(manager);
    }

    public void SaveAlertListeners(T current_spawn)
    {
        manager_list.Add(current_spawn);
        generic_manager_list.Add(current_spawn);
        SetDeathResponse(current_spawn);
        OnSpawn?.Invoke(current_spawn);
    }

    public void SetTargets(List<CharacterManager> player_targets)
    {
        foreach (T ac in manager_list)
        {
            ac.SetTargets(generic_manager_list, player_targets);
        }
    }

    protected abstract void SetDeathResponse(T character_manager);

    protected T InstantiateAddComponent(Vector3 spawn_position)
    {
        GameObject g = spawner_monobehavior.InstantiateCharacter(spawn_position);
        T current_spawn = g.AddComponent<T>();
        return current_spawn;
    }

}
