using System;
using UnityEngine;

public interface IInput
{
    Vector2 MovementInputVector { get; }
    event Action OnInteractEvent;
    event Action OnToggleInventoryEvent;
    event Action OnToggleQuestLogEvent;
    event Action OnToggleGameMenu;
}