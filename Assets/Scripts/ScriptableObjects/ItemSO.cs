using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "ItemSO", order = 0)]
public class ItemSO : ScriptableObject
{
    [SerializeField] private Sprite _sprite;
    [SerializeField] private Color _color;
    [SerializeField] private ItemType _type;
    public Sprite Sprite { get => _sprite; }
    public Color Color { get => _color; }
    public ItemType Type { get => _type; set => _type = value; }

    public enum ItemType
    {
        SWORD,
        ARMOR,
        SHIELD
    }
}