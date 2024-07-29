using UnityEngine;

public class GoHomeState : IState, IStateEnter, IStateExit
{
    private EnemyCharacter _enemy;
    private Vector3 _homePosition;

    public GoHomeState(EnemyCharacter enemy, Vector3 homePosition)
    {
        _enemy = enemy;
        _homePosition = homePosition;
    }

    public void OnEnter()
    {
        _enemy.StopFight();
    }

    public void OnExit()
    {
        _enemy.StopMovement();
    }

    public void Tick()
    {
        Vector2 movementVector = _homePosition - _enemy.transform.position;
        _enemy.MoveInDirection(movementVector);
    }
}
