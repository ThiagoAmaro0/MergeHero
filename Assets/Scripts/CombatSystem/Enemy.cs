using System;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _damage;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private Image _healthImage;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private CoinSO _coin;
    [SerializeField] private int _reward;
    public Action onDie;

    private float _health;
    private float _attackTime;

    public Action<float> onAttack { get; set; }

    void Awake()
    {
        _health = _maxHealth;
        _attackTime = Time.time + (1 / _attackSpeed / 2);
    }

    void Update()
    {
        if (Time.time > _attackTime)
        {
            _attackTime = Time.time + (1 / _attackSpeed);
            onAttack?.Invoke(_damage);
        }
    }

    public bool Hit(float damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            _healthImage.fillAmount = 0;
            onDie?.Invoke();
            _renderer.enabled = false;
            _coin.AddValue(_reward);
            Destroy(gameObject, 1f);
            return true;
        }
        _healthImage.fillAmount = _health / _maxHealth;
        return false;
    }
}