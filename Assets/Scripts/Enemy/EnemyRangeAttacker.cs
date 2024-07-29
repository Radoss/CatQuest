using UnityEngine;
using Zenject;

public class EnemyRangeAttacker : EnemyMeleeAttacker
{
    private ProjectilePool _projectilePool;

    [Inject]
    private void Construct(ProjectilePool projectilePool)
    {
        _projectilePool = projectilePool;
    }

    protected override void PerformAttack(IDamagable target)
    {
        if(TargetCharacter == null)
        {
            return;
        }
        Vector3 directionTowardsTarget = (TargetCharacter.transform.position - transform.position).normalized;
        //Projectile fireball = Instantiate(_projectile, transform).GetComponent<Projectile>();
        Projectile fireball = _projectilePool.GetFreeElement().GetComponent<Projectile>();
        fireball.Fire(directionTowardsTarget, this);
    }
}
