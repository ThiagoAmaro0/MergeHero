using System;
using UnityEngine;

[CreateAssetMenu(fileName = "CoinSO", menuName = "CoinSO", order = 0)]
public class CoinSO : ScriptableObject
{
    [SerializeField] private int _initialAmount;
    private int _value;
    public Action<int> onChangeValue;
    public int GetValue => _value;

    void OnEnable()
    {
        _value = _initialAmount;
        onChangeValue?.Invoke(_value);
    }

    public void AddValue(int value)
    {
        _value += value;
        if (_value < 0)
            _value = 0;
        onChangeValue?.Invoke(_value);
    }

    public bool HaveEnough(int cost)
    {
        return _value >= cost;
    }
}