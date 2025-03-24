using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugPanel : MonoBehaviour
{
    [SerializeField] private Slider _timeScaleSlider;
    [SerializeField] private Button _resetTimeButton;
    [SerializeField] private Button _addCoinButton;
    [SerializeField] private Button _closeButton;
    [SerializeField] private CoinSO _coin;
    void OnEnable()
    {
        _timeScaleSlider.onValueChanged.AddListener(UpdateTimeScale);
        _resetTimeButton.onClick.AddListener(ResetTime);
        _addCoinButton.onClick.AddListener(AddCoin);
        _closeButton.onClick.AddListener(Close);
    }

    void OnDisable()
    {
        _timeScaleSlider.onValueChanged.RemoveListener(UpdateTimeScale);
        _resetTimeButton.onClick.RemoveListener(ResetTime);
        _addCoinButton.onClick.RemoveListener(AddCoin);
        _closeButton.onClick.RemoveListener(Close);
    }

    private void Close()
    {
        gameObject.SetActive(false);
    }

    private void AddCoin()
    {
        _coin.AddValue(100);
    }

    private void ResetTime()
    {
        Time.timeScale = 1;
        _timeScaleSlider.value = 1;
    }

    private void UpdateTimeScale(float value)
    {
        Time.timeScale = value;
    }
}
