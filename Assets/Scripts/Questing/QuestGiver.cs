using System;
using UnityEngine;
using Zenject;

public class QuestGiver : MonoBehaviour, IInteractable
{
    public static event Action<Quest> OnQuestGiven;

    [SerializeField] private QuestSO _questSO;
    [SerializeField] private string _defaultText;

    private Quest _quest;
    private QuestList _questList;
    private QuestDialog _questDialog;
    private ExperienceGiver _experienceGiver;
    private MoneyGiver _moneyGiver;
    private SoundPlayer _soundPlayer;

    [Inject]
    private void Construct(QuestDialog questDialog, QuestList questList)
    {
        _questDialog = questDialog;
        _questList = questList;
    }

    private void Start()
    {
        _quest = _questList.GetQuestForSO(_questSO);
        //_quester.AddQuestToList(_quest);
        _experienceGiver = GetComponent<ExperienceGiver>();
        _moneyGiver = GetComponent<MoneyGiver>();
        _soundPlayer = GetComponent<SoundPlayer>();
    }

    private void CompleteQuest()
    {
        _quest.Complete();
        _experienceGiver.GiveExpirience(_quest.ExperienceReward);
        _moneyGiver.GiveMoney(_quest.GoldReward);
    }

    public static void GiveQuest(Quest quest)
    {
        OnQuestGiven?.Invoke(quest);
    }

    public void Interact()
    {
        if (_soundPlayer != null)
        {
            _soundPlayer.PlaySound();
        }
        _questDialog.ShowQuestDialogForQuest(_quest, _defaultText);
        if (_quest.State == QuestState.WaitingForReward)
        {
            CompleteQuest();
        }
    }
}
