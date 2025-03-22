using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private PlayerStatsSO _playerStats;
    [SerializeField] private Image _healthBar;
    private Enemy _enemy;
    private float _damage;
    private float _attackSpeed;
    private float _armor;
    private float _maxHealth;
    private float _dodge;
    private float _nextAttackTime;

    private float _currentHealth;
    public Action onDie;

    private void Awake()
    {
        UpdateStats();
        _currentHealth = _maxHealth;
    }

    void OnEnable()
    {
        _playerStats.onChangeEquipament += UpdateStats;
    }

    void OnDisable()
    {
        _playerStats.onChangeEquipament -= UpdateStats;
    }

    void Update()
    {
        if (_enemy)
        {
            if (_nextAttackTime < Time.time)
            {
                Attack();
            }
        }
    }

    private void Attack()
    {
        _nextAttackTime = Time.time + 1 / _attackSpeed;
        if (_enemy.Hit(_damage))
        {
            _enemy.onAttack -= Hit;
            _enemy = null;
        }
    }

    private void UpdateStats(ItemSO.ItemType type)
    {
        switch (type)
        {
            case ItemSO.ItemType.SWORD:
                _damage = _playerStats.Damage;
                _attackSpeed = _playerStats.AttackSpeed;
                _nextAttackTime = Time.time + 1 / _attackSpeed;
                break;
            case ItemSO.ItemType.HELMET:
                _maxHealth = _playerStats.Health;
                _healthBar.fillAmount = _currentHealth / _maxHealth;
                break;
            case ItemSO.ItemType.SHIELD:
                _armor = _playerStats.Armor;
                _dodge = _playerStats.Dodge;
                break;
        }
    }

    private void UpdateStats()
    {
        _damage = _playerStats.Damage;
        _attackSpeed = _playerStats.AttackSpeed;
        _armor = _playerStats.Armor;
        _dodge = _playerStats.Dodge;
        _maxHealth = _playerStats.Health;
        _nextAttackTime = Time.time + (1 / _attackSpeed);
    }

    public void Heal()
    {
        _currentHealth = _maxHealth;
        _healthBar.fillAmount = _currentHealth / _maxHealth;
    }

    public void SetEnemy(Enemy currentEnemy)
    {
        _enemy = currentEnemy;
        currentEnemy.onAttack += Hit;
        _nextAttackTime = Time.time + (1 / _attackSpeed);
    }

    public void Hit(float damage)
    {
        float missChance = UnityEngine.Random.Range(0f, 1f);
        if (missChance > _dodge)
        {
            float realDamage = damage - _armor;
            if (realDamage > 0)
            {
                _currentHealth -= realDamage;
                if (_currentHealth <= 0)
                {
                    _enemy.onAttack -= Hit;
                    _enemy = null;
                    _healthBar.fillAmount = 0;
                    onDie?.Invoke();
                }
                else
                {
                    _healthBar.fillAmount = _currentHealth / _maxHealth;
                }
            }
        }
        else
        {
            Debug.Log("DODGE");
        }
    }
}
