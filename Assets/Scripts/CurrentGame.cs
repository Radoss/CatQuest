using System;
using UnityEngine;

public class CurrentGame
{
    public event Action OnGameFinished;
    public GameState State;
    private QuestList _questList;

    public CurrentGame(GameState state, QuestList questList) {
        State = state;
        _questList = questList;
        _questList.OnQuestListCompleted += _questList_OnQuestListCompleted;
    }
    private void _questList_OnQuestListCompleted()
    {
        State = GameState.Finished;
        OnGameFinished?.Invoke();
    }

    public void SetState(GameState state)
    {
        State = state;
    }
}

public enum GameState
{
    InProgress,
    Finished
}