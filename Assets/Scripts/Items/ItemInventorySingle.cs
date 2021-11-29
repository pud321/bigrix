using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ItemInventorySingle : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Text ItemName;
    [SerializeField] private Text ItemCount;

    public Item this_item;
    public int count;
    public int box_id;
    public ItemInventoryController controller;
    public bool isEmpty { get { return this_item == null; } }

    private CanvasGroup canvas_group;
    private RectTransform parent_transform;
    private Image item_background;

    public void Awake()
    {
        canvas_group = GetComponent<CanvasGroup>();
        parent_transform = transform.parent.GetComponent<RectTransform>();
        item_background = GetComponent<Image>();
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
            controller.ShowItemTooltip(null);

            canvas_group.blocksRaycasts = false;
            canvas_group.alpha = 0.8f;

            this.transform.SetParent(parent_transform.parent.parent.parent.transform);
            AllInventoryLookup.SetCompatibiilty(this_item);
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
        AllInventoryLookup.ResetCompatibiilty();


        canvas_group.blocksRaycasts = true;
        canvas_group.alpha = 1f;

        this.transform.SetParent(parent_transform);
        this.transform.localPosition = Vector2.zero;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        controller.ShowItemTooltip(this_item);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        controller.ShowItemTooltip(null);
    }

    public ItemSlotData GetItemSlotData()
    {
        return controller.item_data_list[box_id];
    }

    public int ItemsAddableSwap(ItemEnum type, int count)
    {
        return controller.GetItemsAddable(type, count, GetItemSlotData());
    }

    public int ItemsAddableAdd(ItemEnum type, int count)
    {
        return controller.GetItemsAddable(type, count);
    }

    public void ClearSlotContent()
    {
        controller.RemoveEntireSlot(box_id);
    }

    public void AddData(ItemEnum type, int count)
    {
        if (count > 0)
        {
            controller.AddItemSlot(type, count, box_id);
        }
    }

    public void SetBoxIncompatible()
    {
        item_background.color = Color.red;
    }

    public void SetBoxCompatible()
    {
        item_background.color = Color.white;
    }

}
