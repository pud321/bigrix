using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ItemInventorySingle : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private Text ItemName;
    [SerializeField] private Text ItemCount;

    public Item this_item;
    public int count;
    public int box_id;
    public ItemInventoryController item_data;
    public bool isEmpty { get { return this_item == null; } }

    private CanvasGroup canvas_group;
    private Vector2 saved_position;
    private RectTransform parent_transform;

    public void Awake()
    {
        canvas_group = GetComponent<CanvasGroup>();
        parent_transform = transform.parent.GetComponent<RectTransform>();
        Clear();
    }
    public void SetNameCount(Item this_item, int count)
    {
        if (count == 0)
        {
            Clear();
        }
        else
        {
            this.this_item = this_item;
            this.count = count;
            ItemName.text = this_item.name;
            ItemCount.text = (count > 1) ? count.ToString() : "";
        }
    }

    public void Clear()
    {
        count = 0;
        this_item = null;
        ItemName.text = "";
        ItemCount.text = "";
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (this_item != null)
        {
            canvas_group.blocksRaycasts = false;
            canvas_group.alpha = 0.8f;

            saved_position = this.transform.position;
            this.transform.SetParent(parent_transform.parent.parent.parent.transform);
        }

    }

    public void OnDrag(PointerEventData eventData)
    {
        if (this_item != null)
        {
            this.transform.position = Input.mousePosition;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        canvas_group.blocksRaycasts = true;
        canvas_group.alpha = 1f;

        this.transform.SetParent(parent_transform);
        this.transform.position = saved_position;
    }

    public ItemSlotData GetItemSlotData()
    {
        return item_data.item_data_list[box_id];
    }

    public int ItemsAddableSwap(ItemEnum type, int count)
    {
        return item_data.GetItemsAddable(type, count, GetItemSlotData());
    }

    public int ItemsAddableAdd(ItemEnum type, int count)
    {
        return item_data.GetItemsAddable(type, count);
    }

    public void ClearSlotContent()
    {
        item_data.RemoveEntireSlot(box_id);
    }

    public void AddData(ItemEnum type, int count)
    {
        if (count > 0)
        {
            item_data.AddItemSlot(type, count, box_id);
        }
    }

}
