using System.Collections.Generic;
using UnityEngine;
using System;

public class Quester : MonoBehaviour
{
    public event Action<List<Quest>> OnQueslListChanged; 
    private List<Quest> _questList;

    public void AddQuestToList(Quest quest)
    {
        _questList.Add(quest);
    }

    private void Awake()
    {
        _questList = new List<Quest>();
    }

    private void Start()
    {
        QuestGiver.OnQuestGiven += QuestGiver_OnQuestGiven;
    }

    private void OnDestroy()
    {
        QuestGiver.OnQuestGiven -= QuestGiver_OnQuestGiven;
    }

    private void QuestGiver_OnQuestGiven(Quest quest)
    {
        AcceptQuest(quest);
    }

    public void AcceptQuest(Quest quest)
    {
        quest.Start();
        //_questList.Add(quest);
        quest.OnQuestCompleted += Quest_OnQuestCompleted;
        OnQueslListChanged?.Invoke(_questList);
    }

    private void Quest_OnQuestCompleted()
    {
        OnQueslListChanged?.Invoke(_questList);
    }
}
