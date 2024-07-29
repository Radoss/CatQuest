public class QuestsStateCondition: ConditionBase
{
    private QuestSO[] _quests;
    private QuestList _questList;
    private QuestState _requieredState;

    public QuestsStateCondition(QuestSO[] quests, QuestList questList, QuestState requieredState)
    {
        _quests = quests;
        _questList = questList;
        _requieredState = requieredState;
    }

    public override ConditionCheckStruct ConditionCheck()
    {
        ConditionCheckStruct conditionIsMetMessage = new ConditionCheckStruct();
        conditionIsMetMessage.IsMet = true;
        foreach (QuestSO questSO in _quests)
        {
            Quest quest = _questList.GetQuestForSO(questSO);
            if (quest.State < _requieredState)
            {
                conditionIsMetMessage.IsMet = false;
                conditionIsMetMessage.Message += "<br> quest " + quest.Title + " is not completed";
            }
        }
        return conditionIsMetMessage;
    }
}
