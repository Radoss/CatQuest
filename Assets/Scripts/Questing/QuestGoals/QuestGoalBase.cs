using System;
using UnityEngine;
abstract public class QuestGoalBase
{
    public bool IsReached { get { return _currentAmount >= _requiredAmount; } }
    public string Description { get { return _questGoalSO.Description;  } }
    public int RequiredAmount { get { return _questGoalSO.RequiredAmount;  } }
    public int CurrentAmount { get { return _currentAmount;  } }

    protected QuestGoalSO _questGoalSO;
    protected int _currentAmount = 0;
    protected int _requiredAmount = 0;
    public Action OnGoalReached;
    public Action OnProgressChanged;

    protected void InitializeGoal(QuestGoalSO questGoalSO, Quest quest)
    {
        _questGoalSO = questGoalSO;
        _requiredAmount = _questGoalSO.RequiredAmount;
    }

    abstract public void StartProgress();
    abstract public void StopProgress();
    protected void UpdateProgress()
    {
        if (IsReached)
        {
            return;
        }

        SetAmount();
        OnProgressChanged?.Invoke();

        if (IsReached)
        {
            OnGoalReached?.Invoke();
            return;
        }
    }

    protected virtual void SetAmount()
    {
        _currentAmount++;
    }

}
