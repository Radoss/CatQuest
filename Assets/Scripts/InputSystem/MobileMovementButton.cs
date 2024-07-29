using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class MobileMovementButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [Inject]
    private IInput _input;

    [SerializeField] private Vector2 movementDirection;
    private PlayerMobileInput _mobileInput;

    private void Start()
    {
        _mobileInput = _input as PlayerMobileInput;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _mobileInput.SetMovementInput(movementDirection);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _mobileInput.ResetInput();
    }
}
