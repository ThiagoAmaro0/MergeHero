using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private int _itemPrice;
    [SerializeField] private CoinSO _coin;
    [SerializeField] private TMP_Text _coinText;
    [SerializeField] private Button _buyButton;
    [SerializeField] private ItemSO[] _items;
    [SerializeField] private string _coinSpriteTag = "<sprite=\"coin\" index=0>";

    void OnEnable()
    {
        _buyButton.onClick.AddListener(Buy);
        _coin.onChangeValue += UpdateText;

        _coinText.text = _coinSpriteTag + " " + _coin.GetValue.ToString();
    }
    void OnDisable()
    {
        _buyButton.onClick.RemoveListener(Buy);
        _coin.onChangeValue -= UpdateText;
    }

    private void UpdateText(int value)
    {
        _coinText.text = _coinSpriteTag + " " + value.ToString();
    }

    private void Buy()
    {
        if (_coin.HaveEnough(_itemPrice))
            if (DragManager.instance.GetEmptySlot(out SlotHandler slot))
            {
                _coin.AddValue(-_itemPrice);
                slot.Place(_items[UnityEngine.Random.Range(0, _items.Length)]);
            }
    }
}