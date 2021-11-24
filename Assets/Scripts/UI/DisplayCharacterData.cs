using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayCharacterData : MonoBehaviour
{
    public PlayerCharacterData data;
    private ICharacterDataDisplay[] display_objects;
    private ICharacterDataTracker[] data_tracker;
    private ItemInventoryController item_data;

    public void Awake()
    {
        display_objects = GetComponentsInChildren<ICharacterDataDisplay>();
        data_tracker = GetComponentsInChildren<ICharacterDataTracker>();
        data = null;
    }

    public void Start()
    {
        this.gameObject.SetActive(false);
        AllInventoryLookup.OnPlayerChange += TrackInventory;
    }

    public void SetData(PlayerCharacterData data)
    {
        this.data = data;
        RefreshDisplays();
    }

    private void RefreshDisplays()
    {
        foreach (ICharacterDataDisplay display_object in display_objects)
        {
            display_object.UpdateDisplay(data);
        }

        foreach (ICharacterDataTracker tracker in data_tracker)
        {
            tracker.SetPlayerData(data);
        }

        GetComponentInChildren<PlayerInventoryDisplay>().SetPlayerData(data);
    }

    private void RefreshDisplayData(ItemSlotData[] data)
    {
        Debug.Log("Update inventory");
        Debug.Log(this.data.max_health);
        foreach (ICharacterDataDisplay display_object in display_objects)
        {
            display_object.UpdateDisplay(this.data);
        }

        foreach (ICharacterDataTracker tracker in data_tracker)
        {
            tracker.SetPlayerData(this.data);
        }
    }

    private void TrackInventory()
    {
        if (item_data != null)
        {
            item_data.OnInventoryUpdate -= RefreshDisplayData;
        }

        item_data = AllInventoryLookup.player;
        item_data.OnInventoryUpdate += RefreshDisplayData;
    }
}
