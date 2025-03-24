using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragManager : MonoBehaviour
{
    [SerializeField] private Image _dragingImage;
    [SerializeField] private Transform _slotsGrid;
    [SerializeField] private MergeSystem _mergeSystem;

    [SerializeField] private RectTransform canvasRect;
    private SlotHandler _currentSlot;
    private SlotHandler[] _slots;

    public SlotHandler CurrentSlot { get => _currentSlot; }
    public static DragManager instance;

    void Awake()
    {
        if (instance)
        {
            Destroy(instance);
            return;
        }
        instance = this;
        _slots = _slotsGrid.GetComponentsInChildren<SlotHandler>();
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (GetSlot(out SlotHandler slot))
            {
                slot.PickUp();
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (GetSlot(out SlotHandler slot))
            {
                if (_currentSlot)
                {
                    if (_currentSlot.Item && !slot.Item)
                    {
                        ItemSO item = _currentSlot.Item;
                        _currentSlot.RemoveItem();
                        slot.Place(item);
                    }
                    else if (slot != _currentSlot)
                    {
                        if (_mergeSystem.TryMerge(slot.Item, _currentSlot.Item, out ItemSO result))
                        {
                            TextParticle.instance.NewText("LEVEL UP", slot.transform.position, 1, Color.white);
                            _currentSlot.RemoveItem();
                            slot.RemoveItem();
                            slot.Place(result);
                        }
                        else
                        {
                            _currentSlot.Swap(slot);
                        }
                    }
                }
            }
        }

        if (_currentSlot)
        {
            if (Input.GetMouseButtonUp(0))
                StartCoroutine(CheckMissPlace());
        }
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, Input.mousePosition, Camera.main, out Vector2 localPoint))
        {
            _dragingImage.rectTransform.localPosition = localPoint;
        }
    }

    private bool GetSlot(out SlotHandler slot)
    {
        slot = null;
        PointerEventData data = new PointerEventData(EventSystem.current);
        data.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(data, results);
        for (int i = 0; i < results.Count; i++)
        {
            if (results[i].gameObject.TryGetComponent(out slot))
            {
                return slot;
            }
        }
        return false;
    }

    private IEnumerator CheckMissPlace()
    {
        yield return new WaitForEndOfFrame();
        if (_currentSlot)
        {
            _currentSlot.RestoreItem();
            Place();
        }
    }

    public void PickUp(SlotHandler slot)
    {

        _currentSlot = slot;

        _dragingImage.enabled = true;
        _dragingImage.sprite = _currentSlot.Item.Sprite;
        _dragingImage.color = _currentSlot.Item.Color;
        _dragingImage.color = _currentSlot.Item.Color;
    }

    public void Place()
    {
        _dragingImage.enabled = false;
        _dragingImage.sprite = null;
        _currentSlot = null;
        _dragingImage.color = Color.white;
    }

    public bool GetEmptySlot(out SlotHandler slot)
    {
        slot = null;
        for (int i = 0; i < _slots.Length; i++)
        {
            if (!_slots[i].Item)
            {
                slot = _slots[i];
                break;
            }
        }
        return slot;
    }
}