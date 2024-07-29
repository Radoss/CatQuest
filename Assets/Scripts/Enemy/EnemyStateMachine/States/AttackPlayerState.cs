using UnityEngine;
using System;

public class AttackPlayerState : IState
{
    private Attacker _attacker;
    private IDamagable _damagable;

    public AttackPlayerState(IDamagable damagable, Attacker attacker)
    {
        _attacker = attacker;
        _damagable = damagable;
}

    public void Tick()
    {
        _attacker.TryAttack(_damagable);
    }
}
