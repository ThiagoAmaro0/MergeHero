using UnityEngine;

public class MergeSystem : MonoBehaviour
{
    [SerializeField] private RecipeSO _swordRecipe;
    [SerializeField] private RecipeSO _armorRecipe;
    [SerializeField] private RecipeSO _shieldRecipe;

    public ItemSO TryMerge(ItemSO item1, ItemSO item2, out ItemSO result)
    {
        result = null;
        if (item1 == item2)
        {
            switch (item1.Type)
            {
                case ItemSO.ItemType.SWORD:
                    for (int i = 0; i < _swordRecipe.CraftPath.Length; i++)
                    {
                        if (item1 == _swordRecipe.CraftPath[i] && i + 1 < _swordRecipe.CraftPath.Length)
                        {
                            result = _swordRecipe.CraftPath[i + 1];
                        }
                    }
                    break;
                case ItemSO.ItemType.ARMOR:
                    for (int i = 0; i < _armorRecipe.CraftPath.Length; i++)
                    {
                        if (item1 == _armorRecipe.CraftPath[i] && i + 1 < _armorRecipe.CraftPath.Length)
                        {
                            result = _armorRecipe.CraftPath[i + 1];
                        }
                    }
                    break;
                case ItemSO.ItemType.SHIELD:
                    for (int i = 0; i < _shieldRecipe.CraftPath.Length; i++)
                    {
                        if (item1 == _shieldRecipe.CraftPath[i] && i + 1 < _shieldRecipe.CraftPath.Length)
                        {
                            result = _shieldRecipe.CraftPath[i + 1];
                        }
                    }
                    break;
            }
        }
        return result;
    }
}