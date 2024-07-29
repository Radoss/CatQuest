using System;
using System.Collections.Generic;

public class EnemyStateMachine
{
    private IState _currentState;
    private List<Transition> Transitions = new List<Transition>();

    public void Tick()
    {
        var transition = GetTransition();
        if (transition != null)
        {
            SetState(transition.To);
        }
        _currentState?.Tick();

    }

    Transition GetTransition()
    {
        foreach (var transition in Transitions)
        {
            if (transition.Condition())
            {
                return transition;
            }
        }
        return null;
    }

    public void SetState(IState state)
    {
        if (state == _currentState)
        {
            return;
        }
        (_currentState as IStateExit)?.OnExit();
        _currentState = state;
        (_currentState as IStateEnter)?.OnEnter();
    }

    public void AddTransition(IState to, Func<bool> predicate)
    {
        Transitions.Add(new Transition(to, predicate));
    }

}
