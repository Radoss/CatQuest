using UnityEngine;

public class FollowPlayerState : IState, IStateExit
{
    private EnemyCharacter _enemy;
    private PlayerCharacter _player;

    public FollowPlayerState(EnemyCharacter enemy, PlayerCharacter player)
    {
        _enemy = enemy;
        _player = player;
    }

    public void OnExit()
    {
        _enemy.StopMovement();
    }

    public void Tick()
    {
        Vector2 movementVector = _player.transform.position - _enemy.transform.position;
        _enemy.MoveInDirection(movementVector);
    }

}
