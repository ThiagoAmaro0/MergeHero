using System;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _damage;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private Image _healthImage;
    [SerializeField] private Animator _animator;
    [SerializeField] private CoinSO _coin;
    [SerializeField] private int _reward;
    public Action onDie;

    private float _health;
    private float _nextAttackTime;

    public Action<float> onAttack { get; set; }

    void Awake()
    {
        _health = _maxHealth;
        _nextAttackTime = Time.time + (1 / _attackSpeed / 2);
    }

    void Update()
    {
        if (Time.time > _nextAttackTime)
        {

            _animator.SetTrigger("Attack");
            _nextAttackTime = Time.time + (1 / _attackSpeed);
            onAttack?.Invoke(_damage);
        }
    }

    public bool Hit(float damage)
    {
        _health -= damage;
        TextParticle.instance.NewText(damage.ToString(), transform.position + new Vector3(0, .5f, 0), 1f, Color.red);
        if (_health <= 0)
        {

            _animator.Play("Death");
            _healthImage.fillAmount = 0;
            onDie?.Invoke();
            _coin.AddValue(_reward);
            Destroy(gameObject, 1f);
            _nextAttackTime = Mathf.Infinity;
            return true;
        }
        _animator.SetTrigger("Hit");
        _healthImage.fillAmount = _health / _maxHealth;
        return false;
    }
}