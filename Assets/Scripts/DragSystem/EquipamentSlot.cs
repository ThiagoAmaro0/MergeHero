using UnityEngine;

public class EquipamentSlot : SlotHandler
{
    [SerializeField] private ItemSO.ItemType _type;
    [SerializeField] private PlayerStatsSO _playerStats;

    public override void Place(ItemSO item)
    {
        if (item.Type == _type)
            base.Place(item);

        if (_item)
        {
            _playerStats.Equip(_item);
        }
    }

    public override void Swap(SlotHandler currentSlot)
    {
        if (currentSlot.Item.Type == _type)
            base.Swap(currentSlot);

        if (_item)
        {
            _playerStats.Equip(_item);
        }
    }
}