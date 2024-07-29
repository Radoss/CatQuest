using UnityEngine;
using System;

public class Attacker : MonoBehaviour
{
    public event Action OnAtackEvent;
    [SerializeField] protected int _strength = 1;
    [SerializeField] protected float _timeToNextAttack = 1;
    [SerializeField] protected float _attackSpeed = 2;
    [SerializeField] protected float _attackRange;

    public virtual void TryAttack(IDamagable target)
    {
        if (target.IsDead || _timeToNextAttack > 0)
        {
            return;
        }

        PerformAttack(target);
        OnAtackEvent?.Invoke();
        _timeToNextAttack = _attackSpeed;
    }

    protected virtual void PerformAttack(IDamagable target)
    {
        target.TakeDamage(_strength);
    }

    private void Update()
    {
        if (_timeToNextAttack <= 0)
        {
            return;
        }
        _timeToNextAttack -= Time.fixedDeltaTime;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, _attackRange);
    }
}
