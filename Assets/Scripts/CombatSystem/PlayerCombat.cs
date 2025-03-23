using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private PlayerStatsSO _playerStats;
    [SerializeField] private Image _healthBar;
    [SerializeField] private Animator _animator;
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
        _animator.SetTrigger("Attack");
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
        TextParticle.instance.NewText(_maxHealth.ToString(), transform.position + new Vector3(0, .5f, 0), 1f, Color.green);
        _animator.Play("Idle");
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
            TextParticle.instance.NewText(realDamage.ToString(), transform.position + new Vector3(0, .5f, 0), 1f, Color.red);
            if (realDamage > 0)
            {
                _currentHealth -= realDamage;
                if (_currentHealth <= 0)
                {
                    _nextAttackTime = Mathf.Infinity;
                    _animator.Play("Death");
                    _enemy.onAttack -= Hit;
                    _enemy = null;
                    _healthBar.fillAmount = 0;
                    onDie?.Invoke();
                }
                else
                {
                    _animator.SetTrigger("Hit");
                    _healthBar.fillAmount = _currentHealth / _maxHealth;
                }
            }
        }
        else
        {
            TextParticle.instance.NewText("MISS", transform.position + new Vector3(0, .5f, 0), 1f, Color.white);
            Debug.Log("DODGE");
        }
    }
}
