using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private CharacterSpawner spawner;
    public UIManager ui_manager; 

    private void Awake()
    {
        spawner = GetComponent<CharacterSpawner>();
        ui_manager = GetComponent<UIManager>();

        spawner.OnCharacterSpawn += DelegateSpawnInformation;
    }

    private void Start()
    {
        spawner.StartLevel();
        spawner.ListenToCharacterDamage(DelegateDamageInformation);
        spawner.ListenToCharacterDeath(DelegateDeathInformation);

    }

    private void DelegateDamageInformation(AbstractCharacter sender, DamageEventArgs e)
    {
    }

    private void DelegateDeathInformation(AbstractCharacter sender)
    {
        Debug.Log("Death");
        ui_manager.UpdateScoreText(-1);
    }

    private void DelegateSpawnInformation(AbstractCharacter sender)
    {
        Debug.Log("Spawn " + sender.name);
        ui_manager.UpdateScoreText(1);
    }
}
