using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class QuestLogUI : MonoBehaviour
{
    //[SerializeField] private Quester _questList;
    [SerializeField] private Transform QuestLogPrefab;
    [SerializeField] private Transform QuestLogContainer;
    [SerializeField] private GameObject QuestLogPanelUI_GO;
    [SerializeField] private GameObject _noAvailableQuestsUI_GO;

    private QuestList _questList;

    [Inject]
    private void Construct(QuestList questList)
    {
        _questList = questList;
        _questList.OnQueslListChanged += OnQueslListChanged;
    }

    private void PlayerInput_OnToggleQuestLogEvent()
    {
        ToggleQuestLogUI();
    }

    private void OnDestroy()
    {
        _questList.OnQueslListChanged -= OnQueslListChanged;
    }

    private void OnQueslListChanged(List<Quest> questList)
    {
        ClearPanel();
        int questCount = 0;
        foreach (Quest quest in questList)
        {
            if(quest.State != QuestState.InProgress && quest.State != QuestState.WaitingForReward)
            {
                continue;
            }
            questCount++;
            Transform questLogTR = Instantiate(QuestLogPrefab, QuestLogContainer);
            QuestUI questLog = questLogTR.GetComponent<QuestUI>();
            questLog.SetQuest(quest);
        }
        _noAvailableQuestsUI_GO.SetActive(questCount == 0);

    }

    private void ClearPanel()
    {
        foreach (Transform child in QuestLogContainer)
        {
            Destroy(child.gameObject);
        }
    }

    public void ToggleQuestLogUI()
    {
        QuestLogPanelUI_GO.SetActive(!QuestLogPanelUI_GO.activeSelf);
    }

}
