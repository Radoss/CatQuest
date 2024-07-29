using System;
using UnityEngine;

public class PlayerDesktopInput_Old : MonoBehaviour, IInput
{
    public Vector2 MovementInputVector { get; private set; }
    public event Action OnInteractEvent;
    public event Action OnToggleInventoryEvent;
    public event Action OnToggleQuestLogEvent;
    public event Action OnToggleGameMenu;

    private void Update()
    {
        GetInteractInput();
        GetMovementInput();
        GetToggleInventoryInput();
        GetToggleQuestLogInput();
    }

    private void GetToggleQuestLogInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            OnToggleQuestLogEvent?.Invoke();
        }
    }

    private void GetToggleGameMenuInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnToggleGameMenu?.Invoke();
        }
    }

    private void GetMovementInput()
    {
        MovementInputVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        MovementInputVector.Normalize();
    }

    private void GetInteractInput()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            OnInteractEvent?.Invoke();
        }
    }

    private void GetToggleInventoryInput()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            OnToggleInventoryEvent?.Invoke();
        }
    }
}
