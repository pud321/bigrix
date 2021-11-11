using UnityEngine;
using UnityEngine.AI;

public class CharacterFactory : MonoBehaviour
{
    public GameObject character_prefab;

    private float spawn_tolerance = 1f;

    //public CharacterManager SpawnCharacter(SpawnDefinition details)
    //{
    //    NavMeshHit hit;
    //    bool isHit = NavMesh.SamplePosition(details.spawn_position, out hit, spawn_tolerance, NavMesh.AllAreas);

    //    if (isHit)
    //    {
    //        GameObject g = Instantiate(character_prefab, hit.position, Quaternion.identity);
    //        CharacterManager current_spawn = g.GetComponent<CharacterManager>();
    //        current_spawn.is_enemy = details.is_enemy;

    //        current_spawn.SetupCharacterData(details.info);
    //        return current_spawn;
    //    }

    //    return null;
    //}


    //public PlayerCharacterManager SpawnPlayerCharacter(SpawnDefinition<PlayerCharacterData> details)
    //{
    //    NavMeshHit hit;
    //    bool isHit = NavMesh.SamplePosition(details.spawn_position, out hit, spawn_tolerance, NavMesh.AllAreas);

    //    if (isHit)
    //    {
    //        GameObject g = Instantiate(character_prefab, hit.position, Quaternion.identity);
    //        PlayerCharacterManager current_spawn = g.AddComponent<PlayerCharacterManager>();
    //        current_spawn.SetupCharacterData(details.info);
    //        return current_spawn;
    //    }

    //    return null;
    //}

    //public EnemyCharacterManager SpawnEnemyCharacter(SpawnDefinition<EnemyCharacterData> details)
    //{
    //    NavMeshHit hit;
    //    bool isHit = NavMesh.SamplePosition(details.spawn_position, out hit, spawn_tolerance, NavMesh.AllAreas);

    //    if (isHit)
    //    {
    //        GameObject g = Instantiate(character_prefab, hit.position, Quaternion.identity);
    //        EnemyCharacterManager current_spawn = g.AddComponent<EnemyCharacterManager>();
    //        current_spawn.SetupCharacterData(details.info);
    //        return current_spawn;
    //    }

    //    return null;
    //}
}
