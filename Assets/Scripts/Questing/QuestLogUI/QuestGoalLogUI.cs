using UnityEngine;
using TMPro;

public class QuestGoalLogUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _descriptionTMP;
    [SerializeField] private TextMeshProUGUI _progressTMP;
    private QuestGoalBase _goal;


    public void SetGoal(QuestGoalBase goal)
    {
        _goal = goal;
        _descriptionTMP.text = goal.Description;
        UpdateVisual();
        _goal.OnProgressChanged += OnProgressChanged;
    }

    private void OnProgressChanged()
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        _progressTMP.text = _goal.CurrentAmount + "/" + _goal.RequiredAmount;
    }

    private void OnDestroy()
    {
        _goal.OnProgressChanged -= OnProgressChanged;
    }
}
