using UnityEngine;

[CreateAssetMenu(fileName = "ItemCondition", menuName = "Conditions/ItemCondition")]
public class ItemsObtainedConditionSO : ConditionBaseSO
{
    public ItemSO[] items;
    public int numberOfEach = 1;
}
