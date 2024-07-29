using System;
using UnityEngine;

public class Health : MonoBehaviour, IHealth
{
    public event Action OnHealthChangedEvent;
    public event Action OnDeathEvent;
    public event Action OnReviveEvent;

    [SerializeField] protected int _currentHealth;
    [SerializeField] protected int _maxHealth;
    public int CurrentHealth { get { return _currentHealth; } }
    public int MaxHealth { get { return _maxHealth; } }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        ClampHealth();
        CheckForDeath();
        OnHealthChangedEvent?.Invoke();
    }

    public void Heal(int healthPoints)
    {
        _currentHealth += healthPoints;
        ClampHealth();
        OnHealthChangedEvent?.Invoke();
    }

    public void FullHeal()
    {
        _currentHealth = _maxHealth;
        OnHealthChangedEvent?.Invoke();
    }

    private void ClampHealth()
    {
        _currentHealth = Math.Clamp(_currentHealth, 0, _maxHealth);
    }

    private void CheckForDeath()
    {
        if (_currentHealth == 0)
        {
            OnDeathEvent?.Invoke();
        }
    }

    public float GetHealthNormalized()
    {
        return (float)_currentHealth / _maxHealth;
    }

    public void Revive() {
        _currentHealth = _maxHealth;
        OnHealthChangedEvent?.Invoke();
        OnReviveEvent?.Invoke();
    }

}
