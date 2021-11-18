using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SceneChanger : MonoBehaviour
{
    private Dictionary<LevelEnums, string> scene_names;
    private string base_string = "Homebase";

    public SceneChanger()
    {
        scene_names = new Dictionary<LevelEnums, string>()
        {
            { LevelEnums.Forest, "ExampleLevel" },
            { LevelEnums.Plains, "PlainsLevel" }
        };
    }

    public void ChangeToBase()
    {
        SceneManager.LoadScene(base_string);
    }

    public void ChangeSceneToLevel(LevelEnums scene_name)
    {
        string scene_string = scene_names[scene_name];
        SceneManager.LoadScene(scene_string);
    }
}
