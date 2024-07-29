using UnityEngine;
using TMPro;

public class QuestUI : MonoBehaviour
{
    private Quest _quest;
    [SerializeField] private TextMeshProUGUI _questTitleTMP;
    [SerializeField] private Transform _questGoalLogUIPrefab;

    public void SetQuest(Quest quest)
    {
        _quest = quest;
        _questTitleTMP.text = quest.Title;
        float height = transform.GetComponent<RectTransform>().sizeDelta.y;

        foreach (QuestGoalBase questGoal in quest.QuestGoalList)
        {
            Transform _questGoalLogTR = Instantiate(_questGoalLogUIPrefab, transform);
            QuestGoalLogUI questGoalLogUI = _questGoalLogTR.GetComponent<QuestGoalLogUI>();
            questGoalLogUI.SetGoal(questGoal);
            height += _questGoalLogTR.GetComponent<RectTransform>().sizeDelta.y;
        }

        transform.GetComponent<RectTransform>().sizeDelta = new Vector2(290, height);
    }
}
