using UnityEngine;

public class TestItemCreate : MonoBehaviour
{

    public void ButtonClick()
    {
        AllInventoryLookup.all.AddItem(ItemEnum.Wand, 1);
        AllInventoryLookup.all.AddItem(ItemEnum.Dagger, 1);
    }

    public void ButtonClickRemove()
    {
        AllInventoryLookup.all.RemoveItem(ItemEnum.Wand, 1);
        AllInventoryLookup.all.RemoveItem(ItemEnum.Dagger, 1);
    }
}
