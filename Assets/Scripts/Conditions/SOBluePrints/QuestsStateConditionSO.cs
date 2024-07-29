using UnityEngine;

[CreateAssetMenu(fileName = "QuestsCompletion", menuName = "Conditions/QuestsCompletion")]

public class QuestsStateConditionSO : ConditionBaseSO
{
    public QuestSO[] quests;
    public QuestState requieredState;
}
