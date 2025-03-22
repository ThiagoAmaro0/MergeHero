using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotHandler : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] protected ItemSO _item;

    private ItemSO _lastItem;
    public ItemSO Item { get => _item; }

    void OnEnable()
    {
        _icon.enabled = _item;
        if (_item)
        {
            _icon.sprite = _item.Sprite;
            _icon.color = _item.Color;
        }
    }

    public void RestoreItem()
    {
        _icon.enabled = true;
        _item = _lastItem;
        _icon.sprite = _lastItem.Sprite;
        _icon.color = _lastItem.Color;
    }

    public void RemoveItem()
    {
        _item = null;
        _icon.enabled = false;
        _icon.sprite = null;
        _icon.color = Color.white;
    }

    public void PickUp()
    {
        if (!DragManager.instance.CurrentSlot && _item)
        {
            _icon.enabled = false;
            DragManager.instance.PickUp(this);
        }
    }

    public virtual void Place(ItemSO item)
    {
        if (!_item)
        {
            _item = item;
            _lastItem = _item;
            _icon.enabled = true;
            _icon.sprite = item.Sprite;
            _icon.color = item.Color;

            DragManager.instance.Place();
        }
    }

    public virtual void Swap(SlotHandler currentSlot)
    {
        ItemSO temp = _item;
        _item = currentSlot.Item;
        currentSlot._item = temp;

        RestoreItem();
        currentSlot.RestoreItem();
    }
}
