using UnityEngine;

public class EnemyMeleeAttacker : Attacker
{
    public bool IsInBattle;
    public bool IsAgressive;
    public Character TargetCharacter;
    
    public void SetTimeToNextAttack(float time)
    {
        _timeToNextAttack = time;
    }

    public void StartFight()
    {
        IsInBattle = true;
    }

    public void StopFight()
    {
        IsInBattle = false;
        TargetCharacter = null;
    }

    public bool IsTargetAttackable(IDamagable target)
    {
        return (IsInBattle || IsAgressive)
             && IsTargetInAttackRange(target);
    }

    public bool IsTargetInAttackRange(IDamagable target)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _attackRange);
        foreach (Collider2D hit in hits)
        {
            if (hit.TryGetComponent<IDamagable>(out IDamagable damagableHit))
            {
                if (damagableHit == target)
                {
                    TargetCharacter = hit.GetComponent<Character>();
                    return true;
                }
            };
        }

        return false;
    }

}
