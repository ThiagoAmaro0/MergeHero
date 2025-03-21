using UnityEngine;

[CreateAssetMenu(fileName = "RecipeSO", menuName = "RecipeSO", order = 0)]
public class RecipeSO : ScriptableObject
{
    [SerializeField] private ItemSO[] _craftPath;

    public ItemSO[] CraftPath { get => _craftPath; }
}