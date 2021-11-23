using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractInventoryDisplay : MonoBehaviour
{

    [SerializeField] protected GameObject InventoryBoxEmptyPrefab;
    [SerializeField] protected GameObject InventoryBoxContentPrefab;
    [SerializeField] protected GameObject GridTransform;

    protected List<ItemSlot> all_current_boxes;
    protected List<GameObject> instantiated_gameobjects;

    protected ItemInventoryController item_data;

    protected virtual void Awake()
    {
        all_current_boxes = new List<ItemSlot>();
        instantiated_gameobjects = new List<GameObject>();
    }

    protected virtual void Start()
    {
        item_data.OnInventoryUpdate += UpdateAllDisplays;

        for (int i = 0; i < item_data.inventory_size; i++)
        {
            AddNewBox(i);
        }
    }

    protected virtual void AddNewBox(int i)
    {
        GameObject g_container = CreateItemSlot();
        CreateSingleItemBox(g_container, i);
    }

    protected GameObject CreateItemSlot()
    {
        GameObject g_container = Instantiate(InventoryBoxEmptyPrefab, GridTransform.transform);

        instantiated_gameobjects.Add(g_container);

        ItemSlot new_box = g_container.GetComponent<ItemSlot>();
        all_current_boxes.Add(new_box);
        return g_container;
    }

    protected ItemInventorySingle CreateSingleItemBox(GameObject g_container, int i)
    {
        GameObject g_content = Instantiate(InventoryBoxContentPrefab, g_container.transform);
        ItemInventorySingle item_single = g_content.GetComponent<ItemInventorySingle>();
        item_single.item_data = item_data;
        item_single.box_id = i;
        return item_single;
    }

    public void UpdateAllDisplays(ItemSlotData[] item_data_list)
    {
        for (int i = 0; i < item_data.inventory_size; i++)
        {
            UpdateDisplay(all_current_boxes[i], item_data_list[i]);
        }
    }

    private void UpdateDisplay(ItemSlot item_box, ItemSlotData data)
    {
        if (data != null)
        {
            item_box.SetNameCount(data.item, data.count);
        }
        else
        {
            item_box.Clear();
        }
    }

}
