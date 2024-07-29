
public class DeadState : IState, IStateEnter
{
    private EnemyCharacter _enemy;

    public DeadState(EnemyCharacter enemy)
    {
        _enemy = enemy;
    }

    public void OnEnter()
    {
        _enemy.SetAnimationTrigger(CharacterAnimations.DIETRIGGER);
    }

    public void Tick()
    {
        //Count to respawn
        return;
    }
}
