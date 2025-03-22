using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "ItemSO", order = 0)]
public class ItemSO : ScriptableObject
{
    [SerializeField] private Sprite _sprite;
    [SerializeField] private Color _color;
    [SerializeField] private ItemType _type;
    [SerializeField] private float _power;
    [SerializeField] private float _secondaryPower;
    public Sprite Sprite { get => _sprite; }
    public Color Color { get => _color; }
    public ItemType Type { get => _type; }
    public float Power { get => _power; }
    public float SecondaryPower { get => _secondaryPower; set => _secondaryPower = value; }

    public enum ItemType
    {
        SWORD,
        HELMET,
        SHIELD
    }
}