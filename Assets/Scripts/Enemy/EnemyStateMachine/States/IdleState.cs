using UnityEngine;

public class IdleState : IState
{
    private EnemyCharacter _enemy;

    public IdleState(EnemyCharacter enemy)
    {
        _enemy = enemy;
    }

    public void Tick()
    {
        Vector2 movementVector = new Vector2(0, 0);
        _enemy.MoveInDirection(movementVector);
    }

}
