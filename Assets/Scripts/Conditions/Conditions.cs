using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Conditions : MonoBehaviour
{
    [SerializeField] private ConditionListSO _conditionListSO;
    [SerializeField] private string _messageIfFailed;
    private List<ConditionBase> _conditionList;
    private QuestList _questList;

    [Inject]
    private void Construct(QuestList questList)
    {
        _questList = questList;
    }


    private void Start()
    {
        _conditionList = new List<ConditionBase>();
        foreach(var conditions in _conditionListSO.conditions) {
            if (conditions is QuestsStateConditionSO)
            {
                AddQuestsCompletedCondition(conditions as QuestsStateConditionSO);
            }
            if (conditions is ItemsObtainedConditionSO)
            {
                AddItemsObtainedCondition(conditions as ItemsObtainedConditionSO);
            }
        }
    }

    private void AddQuestsCompletedCondition(QuestsStateConditionSO conditionSO)
    {
        QuestsStateCondition questCondition = new QuestsStateCondition(conditionSO.quests, _questList, conditionSO.requieredState);
        _conditionList.Add(questCondition);
    }

    private void AddItemsObtainedCondition(ItemsObtainedConditionSO conditionSO)
    {
        ItemsObtainedCondition itemsObtained = new ItemsObtainedCondition(conditionSO.items, conditionSO.numberOfEach);
        _conditionList.Add(itemsObtained);
    }

    public ConditionCheckStruct ConditionsCheck()
    {
        ConditionCheckStruct allConditionsCheck = new ConditionCheckStruct();
        allConditionsCheck.IsMet = true;
        foreach (ConditionBase condition in _conditionList)
        {
            ConditionCheckStruct conditionCheck = condition.ConditionCheck();
            if (!conditionCheck.IsMet)
            {
                allConditionsCheck.IsMet = false;
                allConditionsCheck.Message += conditionCheck.Message;
            }
        }
        if (_messageIfFailed.Trim() != "")
        {
            allConditionsCheck.Message = _messageIfFailed;
        }
        return allConditionsCheck;
    }
}
