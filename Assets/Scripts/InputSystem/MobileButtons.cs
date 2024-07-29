using UnityEngine;
using Zenject;

public class MobileButtons : MonoBehaviour
{
    [Inject]
    private IInput _input;

    private PlayerMobileInput _mobileInput;

    private void Start()
    {
        _mobileInput = _input as PlayerMobileInput;
    }

    public void OnInteractClicked()
    {
        _mobileInput.InteractInput();
    }

    public void OnInventoryClicked()
    {
        _mobileInput.InventoryInput();
    }

    public void OnQuestLogClicked()
    {
        _mobileInput.QuestLogInput();
    }
}
