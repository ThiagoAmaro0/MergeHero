using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragManager : MonoBehaviour
{
    [SerializeField] private Image _dragingImage;
    private SlotHandler _currentSlot;

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
                    else
                    {
                        // Merge
                    }
                }
            }
        }

        if (_currentSlot)
        {
            if (Input.GetMouseButtonUp(0))
                StartCoroutine(CheckMissPlace());
        }
        _dragingImage.rectTransform.anchoredPosition = Input.mousePosition;
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
        Debug.Log("PickUp", slot);

        // item.CurrentSlot.Item = null;

        _currentSlot = slot;

        _dragingImage.enabled = true;
        _dragingImage.sprite = _currentSlot.Item.Sprite;
    }

    public void Place()
    {
        _dragingImage.enabled = false;
        _dragingImage.sprite = null;
        _currentSlot = null;
    }
}