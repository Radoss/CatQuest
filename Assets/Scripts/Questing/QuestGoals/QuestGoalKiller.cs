public class QuestGoalKiller : QuestGoalBase
{
    private EnemyType _enemyType;

    public QuestGoalKiller(KillingQuestGoalSO questGoalSO, Quest quest)
    {
        InitializeGoal(questGoalSO, quest);
        _enemyType = questGoalSO.EnemyType;
    }

    public override void StartProgress()
    {
        EnemyCharacter.OnEnemyDeathEvent += EnemyCharacter_OnEnemyDeathEvent;
    }

    private void EnemyCharacter_OnEnemyDeathEvent(EnemyType enemyType)
    {
        if (enemyType == _enemyType)
        {
            UpdateProgress();
        }
    }

    public override void StopProgress()
    {
        EnemyCharacter.OnEnemyDeathEvent -= EnemyCharacter_OnEnemyDeathEvent;
    }
}
