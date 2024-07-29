using System;
public interface IHealth 
{
    int CurrentHealth { get; }
    int MaxHealth { get; }
    event Action OnHealthChangedEvent;
    event Action OnDeathEvent;
    event Action OnReviveEvent;
}
