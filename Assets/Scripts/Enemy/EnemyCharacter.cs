using UnityEngine;
using System;

public class EnemyCharacter : Character
{
    [SerializeField] private EnemyType _enemyType;
    private ExperienceGiver _experienceGiver;
    private MoneyGiver _moneyGiver;
    public static event Action<EnemyType> OnEnemyDeathEvent;
    private EnemyMeleeAttacker _attacker;

    public EnemyType EnemyType { get { return _enemyType; } }

    protected override void Start()
    {
        base.Start();
        _experienceGiver = GetComponent<ExperienceGiver>();
        _moneyGiver = GetComponent<MoneyGiver>();
        _attacker = GetComponent<EnemyMeleeAttacker>();
        _attacker.OnAtackEvent += Attacker_OnAtackEvent;
    }

    private void OnDestroy()
    {
        _attacker.OnAtackEvent -= Attacker_OnAtackEvent;
    }

    protected override void Attacker_OnAtackEvent()
    {
        base.Attacker_OnAtackEvent();
        if (_attacker.TargetCharacter != null)
        {
            Vector3 directionTowardsTarget = (_attacker.TargetCharacter.transform.position - transform.position).normalized;
            _charRenderer.RenderCharacter(directionTowardsTarget);
        }
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        if(!_attacker.IsInBattle)
        {
            _attacker.StartFight();
            _attacker.SetTimeToNextAttack(1f);
        }
    }

    public void MoveInDirection(Vector2 movementVector)
    {
        _charMovement.MoveCharacter(movementVector);
        _charRenderer.RenderCharacter(movementVector);
    }

    public void SetAnimationTrigger(string trigger)
    {
        _charAnimations.SetAnimationTrigger(trigger);
    }

    public void StopFight()
    {
        _attacker.StopFight();
    }

    public void StopMovement()
    {
        _charMovement.StopCharacter();
    }

    protected override void Health_OnDeathEvent()
    {
        base.Health_OnDeathEvent();
        _experienceGiver?.GiveExpirience();
        _moneyGiver?.GiveMoney();
        OnEnemyDeathEvent?.Invoke(_enemyType);
    }
}
