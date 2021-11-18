using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPrefabGenerator : MonoBehaviour
{
    public Dictionary<string, GameObject> gameobject_reference;
    
    private GameObject[] all_prefabs;
    private string effects_dirname = "CombatEffects";
    public void Awake()
    {
        gameobject_reference = new Dictionary<string, GameObject>();
        all_prefabs = Resources.LoadAll<GameObject>(effects_dirname);

        foreach (GameObject prefab in all_prefabs)
        {
            gameobject_reference[prefab.name] = prefab;
        }
    }

    public GameObject InstantiateGameObject(string name, Transform location)
    {
        if (gameobject_reference.ContainsKey(name))
        {
            GameObject g = gameobject_reference[name];
            return Instantiate(g, location);
        }

        return null;
    }

}
