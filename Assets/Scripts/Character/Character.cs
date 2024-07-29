using UnityEngine;

public abstract class Character : MonoBehaviour, IDamagable
{
    protected Health _health;
    protected CharacterRenderer _charRenderer;
    protected CharacterAnimations _charAnimations;
    protected CharacterMovement _charMovement;
    public bool IsDead { get; set; }

    protected virtual void Start()
    {
        _health = GetComponent<Health>();
        _health.OnDeathEvent += Health_OnDeathEvent;
        _health.OnReviveEvent += _health_OnReviveEvent; ;
        _charRenderer = GetComponent<CharacterRenderer>();
        _charAnimations = GetComponent<CharacterAnimations>();
        _charMovement = GetComponent<CharacterMovement>();
    }

    private void _health_OnReviveEvent()
    {
        IsDead = false;
        _charAnimations.SetAnimationTrigger(CharacterAnimations.REVIVE);
        _charMovement.TurnRBOn();
    }

    private void OnDestroy()
    {
        _health.OnDeathEvent -= Health_OnDeathEvent;
    }

    protected virtual void Health_OnDeathEvent()
    {
        IsDead = true;
        _charAnimations.SetAnimationTrigger(CharacterAnimations.DIETRIGGER);
        _charMovement.StopCharacter();
        _charMovement.TurnRBOff();
    }

    protected virtual void Attacker_OnAtackEvent()
    {
        _charAnimations.SetAnimationTrigger(CharacterAnimations.ATTACKTRIGGER);
    }

    public virtual void TakeDamage(int damage)
    {
        if (IsDead)
        {
            return;
        }
        _health.TakeDamage(damage);
        _charRenderer.FlashRed();
        //Play OnHitSound
        //_audioSource.Play();
    }
}

