using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayCharacterData : MonoBehaviour
{
    public PlayerCharacterData data;
    private ICharacterDataDisplay[] display_objects;
    private ICharacterDataTracker[] data_tracker;

    public void Awake()
    {
        display_objects = GetComponentsInChildren<ICharacterDataDisplay>();
        data_tracker = GetComponentsInChildren<ICharacterDataTracker>();
        data = null;
    }

    public void Start()
    {
        this.gameObject.SetActive(false);
    }

    public void SetData(PlayerCharacterData data)
    {
        this.data = data;

        foreach (ICharacterDataDisplay display_object in display_objects)
        {
            display_object.UpdateDisplay(data);
        }

        foreach (ICharacterDataTracker tracker in data_tracker)
        {
            tracker.SetPlayerData(data);
        }
    }
}
