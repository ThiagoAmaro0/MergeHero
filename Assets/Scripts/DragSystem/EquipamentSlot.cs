using UnityEngine;

public class EquipamentSlot : SlotHandler
{
    [SerializeField] private ItemSO.ItemType _type;

    public override void Place(ItemSO item)
    {
        if (item.Type == _type)
            base.Place(item);
    }

    public override void Swap(SlotHandler currentSlot)
    {
        if (currentSlot.Item.Type == _type)
            base.Swap(currentSlot);
    }
}