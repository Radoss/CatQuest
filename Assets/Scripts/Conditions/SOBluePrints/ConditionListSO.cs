using UnityEngine;

[CreateAssetMenu(fileName = "ListOfConditions", menuName = "Conditions/ListOfConditions")]

public class ConditionListSO : ScriptableObject
{
    public ConditionBaseSO[] conditions;
    public bool isOneTimeCondition;
}
