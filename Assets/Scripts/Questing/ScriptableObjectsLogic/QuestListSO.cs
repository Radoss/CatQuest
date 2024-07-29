using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "Quest/Questlist")]
public class QuestlistSO: ScriptableObject
{
    public QuestSO[] quests;
}

