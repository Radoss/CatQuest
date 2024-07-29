using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMobileInput : MonoBehaviour, IInput
{
    public Vector2 MovementInputVector { get; private set; }
    [SerializeField] private GameObject _visualUI_GO;
    public event Action OnInteractEvent;
    public event Action OnToggleInventoryEvent;
    public event Action OnToggleQuestLogEvent;
    public event Action OnToggleGameMenu;

    public void ActivateMobileInput()
    {
        _visualUI_GO.SetActive(true);
    }

    public void SetMovementInput(Vector2 direction)
    {
        MovementInputVector = direction;
    }

    public void ResetInput()
    {
        MovementInputVector = Vector2.zero;
    }

    public void InteractInput()
    {
        OnInteractEvent?.Invoke();
    }

    public void InventoryInput()
    {
        OnToggleInventoryEvent?.Invoke();
    }

    public void GameMenuInput()
    {
        OnToggleGameMenu?.Invoke();
    }

    public void QuestLogInput()
    {
        OnToggleQuestLogEvent?.Invoke();
    }

}
