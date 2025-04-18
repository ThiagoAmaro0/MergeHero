using System;
using System.Collections;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveSystem : MonoBehaviour
{
    [SerializeField] private WaveSO[] _waves;
    [SerializeField] private PlayerCombat _playerCombat;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _combatPoint;
    [SerializeField] private float _spawnDelay = 1f;
    [SerializeField] private TMP_Text _currentWaveText;
    [SerializeField] private TMP_Text _nextWaveText;
    [SerializeField] private Image _waveProgress;
    private Enemy[] _enemies;
    private Enemy _currentEnemy;
    private int _waveIndex = -1;
    private int _enemyIndex = -1;

    void Awake()
    {
        NewWave();
    }

    void OnEnable()
    {
        _playerCombat.onDie += RestartWave;
    }

    void OnDisable()
    {
        _playerCombat.onDie -= RestartWave;
    }

    private void UpdateUI()
    {
        _currentWaveText.text = (_waveIndex + 1).ToString();
        _nextWaveText.text = (_waveIndex + 2).ToString();
        _waveProgress.fillAmount = (_enemyIndex + 1f) / _enemies.Length;
    }

    private void RestartWave()
    {
        StartCoroutine(DelayedRestart());
    }

    private IEnumerator DelayedRestart()
    {
        yield return new WaitForSeconds(1f);
        if (_currentEnemy)
        {
            Destroy(_currentEnemy.gameObject);
        }
        _playerCombat.Heal();
        _enemyIndex = -1;
        _enemies = _waves[_waveIndex].Enemies;
        NewEnemy();
        UpdateUI();
    }

    private void NewWave()
    {
        if (_waveIndex + 1 <= _waves.Length)
        {
            _waveIndex++;
        }
        _playerCombat.Heal();
        _enemyIndex = 0;
        _enemies = _waves[_waveIndex].Enemies;
        NewEnemy();

        UpdateUI();
    }

    private void NewEnemy()
    {
        if (_enemies != null)
        {
            _enemyIndex++;
            StartCoroutine(SpawnEnemy());
            UpdateUI();
        }
    }

    private IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(_spawnDelay);
        if (_currentEnemy)
            _currentEnemy.onDie -= NewEnemy;
        if (_enemyIndex < _enemies.Length)
        {
            _currentEnemy = Instantiate(_enemies[_enemyIndex], _spawnPoint.position, Quaternion.identity);
            _playerCombat.SetEnemy(_currentEnemy);
            _currentEnemy.onDie += NewEnemy;
            _currentEnemy.transform.DOMove(_combatPoint.position, .75f);
        }
        else
        {
            NewWave();
        }
    }
}