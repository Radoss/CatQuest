using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class NewPlayerInput : MonoBehaviour, IInput
{
    public Vector2 MovementInputVector { get; private set; }
    public event Action OnInteractEvent;
    public event Action OnToggleInventoryEvent;
    public event Action OnToggleQuestLogEvent;
    public event Action OnToggleGameMenu;

    private bool _isGameInProgress = true;

    [Inject]
    private void Construct(CurrentGame currentGame)
    {
        currentGame.OnGameFinished += CurrentGame_OnGameFinished;
    }

    private void CurrentGame_OnGameFinished()
    {
        _isGameInProgress = false;
    }

    public void OnMovement(InputValue value)
    {
        if (!_isGameInProgress)
        {
            return;
        }
        MovementInputVector = value.Get<Vector2>();
    }

    public void OnInteract()
    {
        OnInteractEvent?.Invoke();
    }

    public void OnInventory()
    {
        OnToggleInventoryEvent?.Invoke();
    }

    public void OnQuestLog()
    {
        OnToggleQuestLogEvent?.Invoke();
    }

    public void OnGameMenu()
    {
        if (!_isGameInProgress)
        {
            return;
        }
        OnToggleGameMenu?.Invoke();
    }
}
