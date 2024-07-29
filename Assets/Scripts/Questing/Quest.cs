using System;
using System.Collections.Generic;
using UnityEngine;
public class Quest
{
    public event Action OnQuestCompleted;
    public string Id { get { return _questSO.Id; } }
    public string Title { get { return _questSO.Title; } }
    public string Description { get { return _questSO.Description; } }
    public string TextAfterCompletion { get { return _questSO.TextAfterCompletion; } }
    public string TextDuringProgress { get { return _questSO.TextDuringProgress; } }
    public int ExperienceReward { get { return _questSO.ExperienceReward; } }
    public int GoldReward { get { return _questSO.GoldReward; } }

    public List<QuestGoalBase> QuestGoalList { get { return _questGoalList; } }

    public QuestState State { get; private set; } = QuestState.NotStarted;

    private QuestSO _questSO;

    private List<QuestGoalBase> _questGoalList;

    public bool IsForQuestSO (QuestSO questSO)
    {
        return questSO == _questSO;
    }

    public Quest(QuestSO questSO)
    {
        _questSO = questSO;
        _questGoalList = new List<QuestGoalBase>();
        foreach (QuestGoalSO questGoalSO in _questSO.questGoalList)
        {
            QuestGoalBase goal = GetGoal(questGoalSO);
            goal.OnGoalReached += CheckProgress;
            _questGoalList.Add(goal);
        }
    }

    private QuestGoalBase GetGoal(QuestGoalSO questGoalSO)
    {
        switch(questGoalSO)
        {
            case CollectingQuestGoalSO goal:
                return new QuestGoalCollector(questGoalSO as CollectingQuestGoalSO, this);
            case KillingQuestGoalSO goal:
                return new QuestGoalKiller(questGoalSO as KillingQuestGoalSO, this);
            default: return null;
        }
    }

    public void Start()
    {
        State = QuestState.InProgress;
        foreach(QuestGoalBase goal in _questGoalList)
        {
            goal.StartProgress();
        }
    }

    public void CheckProgress()
    {
        if (State != QuestState.InProgress)
        {
            return;
        }
        foreach (QuestGoalBase questGoal in _questGoalList)
        {
            if (!questGoal.IsReached)
            {
                return;
            }
        }
        State = QuestState.WaitingForReward;
    }

    public void Complete()
    {
        State = QuestState.Completed;
        OnQuestCompleted?.Invoke();
        foreach (QuestGoalBase goal in _questGoalList)
        {
            goal.StopProgress();
        }
        Debug.Log("OnQuestCompleted?.Invoke()");
    }

    public void SetState(QuestState state)
    {
        State = state;
    }
}

