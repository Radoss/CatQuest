using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "Quest/New Quest")]
public class QuestSO : ScriptableObject
{
    public bool IsActive;
    public string Id;
    public string Title;
    public string Description;
    public string TextDuringProgress;
    public string TextAfterCompletion;
    public int ExperienceReward;
    public int GoldReward;
    public QuestGoalSO[] questGoalList;
}
