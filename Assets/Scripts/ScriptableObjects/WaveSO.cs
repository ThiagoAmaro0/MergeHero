using UnityEngine;

[CreateAssetMenu(fileName = "WaveSO", menuName = "WaveSO", order = 0)]
public class WaveSO : ScriptableObject
{
    [SerializeField] private Enemy[] _enemies;

    public Enemy[] Enemies { get => _enemies; }
}