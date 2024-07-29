using System;
using UnityEngine;
public class QuestGoalCollector : QuestGoalBase
{
    private ItemSO _itemToCollect;

    public QuestGoalCollector(CollectingQuestGoalSO questGoalSO, Quest quest)
    {
        InitializeGoal(questGoalSO, quest);
        _itemToCollect = questGoalSO.ItemToCollect;
    }

    ~QuestGoalCollector()
    {
        Inventory.OnItemsChanged -= Inventory_OnItemsChanged;
    }

    public override void StopProgress()
    {
        Inventory.OnItemsChanged -= Inventory_OnItemsChanged;
        Inventory.RemoveSomeItems(_itemToCollect, _currentAmount);
    }

    private void Inventory_OnItemsChanged()
    {
        UpdateProgress();
    }

    protected override void SetAmount()
    {
        _currentAmount = Math.Clamp(Inventory.GetQuantityOfItem(_itemToCollect), 0, _requiredAmount);
    }

    public override void StartProgress()
    {
        UpdateProgress();
        Inventory.OnItemsChanged += Inventory_OnItemsChanged;
    }

}
