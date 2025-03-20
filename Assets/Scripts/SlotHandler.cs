using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotHandler : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private ItemSO _item;

    public ItemSO Item { get => _item; }

    void OnEnable()
    {
        _icon.enabled = _item;
        if (_item)
        {
            _icon.sprite = _item.Sprite;
        }
    }

    public void RestoreItem()
    {
        _icon.enabled = true;
    }

    public void RemoveItem()
    {
        _item = null;
        _icon.enabled = false;
        _icon.sprite = null;
    }

    public void PickUp()
    {
        if (!DragManager.instance.CurrentSlot && _item)
        {
            _icon.enabled = false;
            DragManager.instance.PickUp(this);
        }
    }

    public void Place(ItemSO item)
    {
        if (DragManager.instance.CurrentSlot && !_item)
        {
            _item = item;
            _icon.enabled = true;
            _icon.sprite = item.Sprite;

            DragManager.instance.Place();
        }
    }
}
