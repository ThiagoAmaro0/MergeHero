using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatsSO", menuName = "PlayerStatsSO", order = 0)]
public class PlayerStatsSO : ScriptableObject
{
    [SerializeField] private float _BaseDamage = 1f;
    [SerializeField] private float _BaseAttackSpeed = .5f;
    [SerializeField] private float _BaseHealth = 10f;
    [SerializeField] private float _BaseArmor = 0f;
    [SerializeField] private float _BaseDodge = 0f;

    private ItemSO _weapon;
    private ItemSO _shield;
    private ItemSO _helmet;

    public float Damage => (_weapon ? _weapon.Power : 0) + _BaseDamage;
    public float AttackSpeed => (_weapon ? _weapon.SecondaryPower : 0) + _BaseAttackSpeed;
    public float Health => (_helmet ? _helmet.Power : 0) + _BaseHealth;
    public float Armor => (_shield ? _shield.Power : 0) + _BaseArmor;
    public float Dodge => (_shield ? _shield.SecondaryPower : 0) + _BaseDodge;

    public Action<ItemSO.ItemType> onChangeEquipament;

    void OnEnable()
    {
        _weapon = null;
        _shield = null;
        _helmet = null;
    }

    public void Equip(ItemSO item)
    {
        switch (item.Type)
        {
            case ItemSO.ItemType.SWORD:
                _weapon = item;
                onChangeEquipament?.Invoke(item.Type);
                break;
            case ItemSO.ItemType.HELMET:
                _helmet = item;
                onChangeEquipament?.Invoke(item.Type);
                break;
            case ItemSO.ItemType.SHIELD:
                _shield = item;
                onChangeEquipament?.Invoke(item.Type);
                break;
        }

    }
}