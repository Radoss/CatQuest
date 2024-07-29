using Zenject;

public class QuestDialog
{
    private QuestList _questList;
    private DiContainer _container;
    private DialogUI _dialogUI;

    public QuestDialog(DiContainer container)
    {
        _container = container;
    }

    public void ShowQuestDialogForQuest(Quest quest, string defaultText)
    {
        if (_questList == null)
        {
            _questList = _container.Resolve<QuestList>();
            _dialogUI = _container.Resolve<DialogUI>();
        }

        string txtToShow = defaultText;
        switch (quest.State)
        {
            case QuestState.NotStarted:
                txtToShow = GetQuestInfoText(quest);
                _dialogUI.ShowDialogQuest(txtToShow, () => { _questList.AcceptQuest(quest); });
                return;
            case QuestState.InProgress:
                txtToShow = GetQuestInProgressText(quest);
                break;
            case QuestState.WaitingForReward:
                txtToShow = GetQuestCompletedText(quest);
                break;
        }

        _dialogUI.ShowDialogText(txtToShow);
    }

    private string GetQuestInfoText(Quest quest)
    {
        return quest.Title +
            "<br>" + quest.Description +
            "<br>XP:" + quest.ExperienceReward +
            " / Gold:" + quest.GoldReward;
    }

    private string GetQuestInProgressText(Quest quest)
    {
        return quest.TextDuringProgress;
    }

    private string GetQuestCompletedText(Quest quest)
    {
        return quest.TextAfterCompletion;
    }


}
