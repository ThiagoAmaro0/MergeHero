using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ParallaxConfigSO", menuName = "ParallaxConfigSO", order = 0)]
public class ParallaxConfigSO : ScriptableObject
{
    [SerializeField] private float _timeScale;
    [SerializeField] private Material[] _materials;
    public float TimeScale { get => _timeScale; set => SetTimeScale(value); }

    void OnEnable()
    {
        Debug.Log("Awake");
        foreach (Material material in _materials)
        {
            material.SetFloat("_TimeScale", _timeScale);

        }
    }

    private void SetTimeScale(float value)
    {
        if (_timeScale >= 0)
        {
            _timeScale = value;
            foreach (Material material in _materials)
            {
                material.SetFloat("_TimeScale", _timeScale);
            }
        }
    }
}