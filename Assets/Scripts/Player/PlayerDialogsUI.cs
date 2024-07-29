using UnityEngine;
using Zenject;

public class PlayerDialogsUI : MonoBehaviour
{
    public bool IsInteractableDialogOpened { get { return _dialogUI.IsDialogActive || _storeUI.IsStoreActive; } }
    private DialogUI _dialogUI;
    private StoreUI _storeUI;
    private InventoryUI _inventoryUI;
    private QuestLogUI _questLogUI;
    private IInput _playerInput;

    [Inject]
    private void Construct(IInput playerInput, InventoryUI inventoryUI, DialogUI dialogUI, StoreUI storeUI, QuestLogUI questLogUI)
    {
        _playerInput = playerInput;
        _dialogUI = dialogUI;
        _storeUI = storeUI;
        _inventoryUI = inventoryUI;
        _questLogUI = questLogUI;
        _playerInput.OnToggleInventoryEvent += ToggleInventoryDialog;
        _playerInput.OnToggleQuestLogEvent += ToggleQuestLogDialog;
    }

    private void OnDestroy()
    {
        _playerInput.OnToggleInventoryEvent -= ToggleInventoryDialog;
        _playerInput.OnToggleQuestLogEvent -= ToggleQuestLogDialog;
    }

    public void CloseNotStationaryDialogs()
    {
        _dialogUI.ToggleDialog(false);
        _storeUI.ToggleStoreInventory(false);
    }

    public void ToggleInventoryDialog()
    {
        _inventoryUI.ToggleInventoryUI();
    }

    public void ToggleQuestLogDialog()
    {
        _questLogUI.ToggleQuestLogUI();
    }

}
