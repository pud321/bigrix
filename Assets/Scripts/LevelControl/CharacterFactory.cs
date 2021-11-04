using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterFactory : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> character_prefabs;

    public Dictionary<CharacterEnums, GameObject> character_dictionary;
    private float spawn_tolerance = 1f;

    private void Awake()
    {
        character_dictionary = new Dictionary<CharacterEnums, GameObject>();

        foreach (GameObject g in character_prefabs)
        {
            character_dictionary.Add(g.GetComponent<AbstractCharacter>().character_type, g);
        }
        
    }

    public GameObject SpawnCharacter(CharacterEnums identifier, Vector3 position)
    {
        NavMeshHit hit;
        bool isHit = NavMesh.SamplePosition(position, out hit, spawn_tolerance, NavMesh.AllAreas);

        if (isHit && character_dictionary.ContainsKey(identifier))
        {
            return Instantiate(character_dictionary[identifier], hit.position, Quaternion.identity);
        }

        return null;
    }
}
