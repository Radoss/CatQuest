using System;
using Zenject;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float _aggroRange;
    private EnemyStateMachine _stateMachine;
    private Vector3 _spawnPosition;
    private IState _idleState;
    private IState _followState;
    private IState _attackState;
    private IState _goHomeState;
    private IState _deadState;
    private PlayerCharacter _player;

    private EnemyCharacter _enemy;
    private EnemyMeleeAttacker _attacker;

    [Inject]
    private void Construct(PlayerCharacter player)
    {
        _player = player;
    }

    private void Start()
    {
        _attacker = GetComponent<EnemyMeleeAttacker>();
        _enemy = GetComponent<EnemyCharacter>();
        _spawnPosition = transform.position;

        _stateMachine = new EnemyStateMachine();

        _idleState = new IdleState(_enemy);
        _followState = new FollowPlayerState(_enemy, _player);
        _attackState = new AttackPlayerState(_player, _attacker);
        _goHomeState = new GoHomeState(_enemy, transform.position);
        _deadState = new DeadState(_enemy);

        _stateMachine.SetState(_idleState);
        _stateMachine.AddTransition(to: _followState, predicate: IsTargetTargetFollowable());
        _stateMachine.AddTransition(to: _attackState, predicate: IsTargetAttackable());
        _stateMachine.AddTransition(to: _goHomeState, predicate: NeedToGoHome());
        _stateMachine.AddTransition(to: _deadState, predicate: () => _enemy.IsDead);
        _stateMachine.AddTransition(to: _idleState, predicate: () => !_attacker.IsInBattle && IsInSpawnPlace());
    }

    private Func<bool> IsTargetTargetFollowable() =>
    () => !_enemy.IsDead
            && (_attacker.IsInBattle || _attacker.IsAgressive)
            && IsTargetInAggroRange(_player)
            && !_attacker.IsTargetInAttackRange(_player);

    private Func<bool> IsTargetAttackable() =>
     () => !_enemy.IsDead
             && _attacker.IsTargetAttackable(_player);

    private Func<bool> NeedToGoHome() =>
     () => !_enemy.IsDead
             && (!IsTargetInAggroRange(_player) || !_attacker.IsInBattle)
             && !IsInSpawnPlace();

    private bool IsTargetInAggroRange(IDamagable target)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(_spawnPosition, _aggroRange);
        foreach (Collider2D hit in hits)
        {
            if (hit.TryGetComponent<IDamagable>(out IDamagable damagableHit))
            {
                if (damagableHit == target)
                {
                    return true;
                }
            };
        }

        return false;
    }

    private bool IsInSpawnPlace()
    {
        float distance = Vector2.Distance(transform.position, _spawnPosition);
        return distance < 0.1f;
    }

    private void FixedUpdate()
    {
        _stateMachine.Tick();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, _aggroRange);
    }

}
