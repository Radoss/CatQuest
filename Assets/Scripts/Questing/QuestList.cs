using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QuestList : MonoBehaviour, IDataSaver, IDataLoader
{
    [SerializeField] private QuestlistSO _questlistSO;
    public event Action<List<Quest>> OnQueslListChanged;
    public event Action OnQuestListCompleted;
    private List<Quest> _questList;

    public Quest GetQuestForSO(QuestSO questSO) 
    {
        foreach (Quest quest in _questList)
        {
            if (quest.IsForQuestSO(questSO))
            {
                return quest;
            }
        }
        return null;
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
        if (CheckIfAllquestCompleted())
        {
            OnQuestListCompleted?.Invoke();
        }
    }

    private bool CheckIfAllquestCompleted()
    {
        bool isAllCompleted = true;
        foreach(Quest quest in _questList)
        {
            if(quest.State < QuestState.Completed)
            {
                isAllCompleted = false;
            }
        }
        return isAllCompleted;
    }

    public void LoadData(GameData gameData)
    {
        Dictionary<string, int> questIdToStateMap = gameData.QuestIdToStateMap;
        _questList = new List<Quest>();
        foreach (QuestSO questSO in _questlistSO.quests)
        {
            Quest quest = new Quest(questSO);
            _questList.Add(quest);
            if (questIdToStateMap.ContainsKey(questSO.Id))
            {
                QuestState state = (QuestState)questIdToStateMap[questSO.Id];
                if(state == QuestState.InProgress || state == QuestState.WaitingForReward)
                {
                    AcceptQuest(quest);
                }
                quest.SetState(state);
            }
        }
    }

    public void SaveData(GameData gameData)
    {
        Dictionary<string, int> questIdToStateMap = new Dictionary<string, int>();
        foreach (Quest quest in _questList)
        {
            questIdToStateMap.Add(quest.Id, (int)quest.State);
        }
        gameData.QuestIdToStateMap = questIdToStateMap;
    }
}
