using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "ItemSO", order = 0)]
public class ItemSO : ScriptableObject
{
    [SerializeField] private Sprite _sprite;
    public Sprite Sprite { get => _sprite; }
}